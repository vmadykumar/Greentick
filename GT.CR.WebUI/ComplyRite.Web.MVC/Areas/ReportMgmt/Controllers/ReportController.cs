using ComplyRite.Web.MVC.Areas.ReportMgmt.App_LocalResources;
using ComplyRite.Web.MVC.Areas.ReportMgmt.Models;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.App_LocalResources;
using ComplyRite.Web.MVC.Facade.Report;
using ComplyRite.Web.MVC.Facade.SchedulingMgmt;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Controllers
{
    public class ReportController : Controller
    {
        #region Global Variable Declaration
        private static IReportMgmtFacade _iReportMgmtFacade = new ReportMgmtFacade();
        string userUUID = "";
        #endregion

        private static IScheduleMgmtFacade _iScheduleMgmtFacade = new ScheduleMgmtFacade();

        // GET: ReportMgmt/ReportMgmt
        public ActionResult Index()
        {
            return View();
        }
        public async Task<List<AccountLocationsDTO>> GetLocationsByUser(string uuid)
        {
        
            string lurl = ServiceUrls.GetAllLocationsbyUUID + uuid;

            return await _iReportMgmtFacade.GetAllLocationsByUUID(lurl);
        }
        public async Task<ActionResult> AnalyticsHome()
        {
            GetUserDetails();
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;
            userUUID = claims.ToList().Where(i => i.Type.Split('/').LastOrDefault() == "muid").Select(j => j.Value).ToList()[0];

            string companyName = await GetUserCompany(userUUID);

            var locations = await GetLocationsByUser(userUUID);
            //ViewBag.userLocations 
            ViewBag.userLocations = GetUserLocations(locations) ;
            ViewBag.userAccounts = Newtonsoft.Json.JsonConvert.SerializeObject( locations.Select(a=>a.AccountCode).ToList());

            ViewBag.ticket = PostDataToService("", "GreenTick");
            switch (companyName)
            {
                case "AHC":
                       return View("~/Areas/ReportMgmt/Views/Dashboard/_AnalyticsHomeHealthTech.cshtml");

                default: return View("~/Areas/ReportMgmt/Views/Dashboard/_AnalyticsHome.cshtml");
            }
            
        }

        private string GetUserLocations(List<AccountLocationsDTO> AccountLocations)
        {
            List<string> locations = new List<string>();
            foreach (AccountLocationsDTO account in AccountLocations)
                locations.AddRange(account.Locations.Select(a => a.LocationName).ToList());
            return Newtonsoft.Json.JsonConvert.SerializeObject(locations);
        }

        public async Task<string> GetUserCompany(string uuid)
        {
            string lurl = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userDetails = await _iScheduleMgmtFacade.GetUserByUUID(lurl);
            string companyName = userDetails.UserAccounts[0].LobName;

            return companyName;
        }

        //public async Task<ActionResult> GetDashboardData()
        //{
        //    GetUserDetails();
        //    string CompanyAlias = await GetUserAbbr(userUUID);
        //    string url = ScheduleServiceUrls.GetPublishedAuditCount + CompanyAlias;
        //    DashboardModel dashboardObj = await _iScheduleMgmtFacade.GetPublishedAuditCount(url);
        //    return View("~/Areas/SchedulingMgmt/Views/Dashboard/DashboardIndex.cshtml", dashboardObj);
        //}
        public void GetUserDetails()
        {
            string userId = User.Identity.GetUserId();
            string userName = User.Identity.GetUserName();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            userUUID = claims.ToList().Where(i => i.Type.Split('/').LastOrDefault() == "muid").Select(j => j.Value).ToList()[0];
            System.Web.HttpContext.Current.Request.Headers["UUID"] = userUUID;
            System.Web.HttpContext.Current.Request.Headers["UserName"] = userName;
            System.Web.HttpContext.Current.Request.Headers["UserId"] = userId;
            ViewBag.UUID = userUUID;
            ViewBag.UserName = userName;
            ViewBag.UserId = userId;
        }


        /// <summary>
        /// Get Tokens 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="targetSite_parameter"></param>
        /// <returns></returns>
        private async Task<string> PostDataToService(string user, string targetSite_parameter = null)
        {
            user = "pratian";
            try
            {
                var ticket = "";
                string targetSite = targetSite_parameter;

                // Getting the Server name where tableau is hosted from the web config file.
                string url = ConfigurationManager.AppSettings["TableauServer"];

                // Getting the target site name in the tableau server from the web config file based on the module selected.
                switch (targetSite_parameter)
                {
                    case "GreenTick":
                        targetSite = ConfigurationManager.AppSettings["TargetSiteForGreenTick"];
                        break;
                    case "EDW":
                        targetSite = ConfigurationManager.AppSettings["TargetSiteForEDW"];
                        break;
                    default:
                        break;
                }


                // Appending the username and target_site into the url.
                string uri = url + "trusted?username=" + user + "&target_site=" + targetSite_parameter;

                // Creating the object with two property (username , target_site)
                var data = new
                {
                    username = user,
                    target_site = targetSite
                };

                // Client object creation and putting under using block.
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    // Creating the reponse variable and getting reponse from the server.
                    HttpResponseMessage response = client.PostAsJsonAsync(uri, data).Result;


                    // checking if the response is successful.
                    if (response.IsSuccessStatusCode)
                    {
                        // Extracting the content from the response.
                        ticket = await response.Content.ReadAsStringAsync();
                    }

                    return ticket;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetTickets(int num)
        {
            List<string> token = new List<string>();
            for (int i = 0; i < num; i++)
            {
                token.Add(PostDataToService("", "GreenTick").Result);
            }
            return Json(token, JsonRequestBehavior.AllowGet);
        }

        #region Reports

        /// Author : Sharath K M
        /// <summary>
        /// Method to get the audit report data
        /// </summary>        
        /// <returns>The model binded report view</returns>
        public async Task<ActionResult> ReportIndex(string audId)
        {
            string auditId = Base64Decode(audId);
            string url = ServiceUrls.GetReportData + auditId;
            AuditReport report = await _iReportMgmtFacade.GetAuditReportData(auditId, url);
            report.Sections.ForEach(x => x.AuditAreas.ForEach(y => { y.PassedCheckpointPercentage = x.Round(y.PassedCheckpointPercentage); y.FailedCheckpointPercentage = x.Round(y.FailedCheckpointPercentage); }));
            report.AuditDate = TimeZoneInfo.ConvertTimeFromUtc(report.AuditDate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            return View(report);
        }

        #endregion

        #region Private Methods
        /// Author : Sharath K M
        /// <summary>
        /// Method to encode any text.
        /// </summary>      
        /// <param name="plainText"></param>
        /// <returns></returns>
        private static string Base64Encode(string plainText)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
        }

        /// Author : Sharath K M
        /// <summary>
        /// Method to decode the encoded text
        /// </summary>    
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        private static string Base64Decode(string base64EncodedData)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
        }
        #endregion
    }
}
