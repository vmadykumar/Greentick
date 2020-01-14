using ComplyRite.Web.MVC.Areas.SchedulingMgmt.App_LocalResources;
using ComplyRite.Web.MVC.Facade.SchedulingMgmt;
using Microsoft.AspNet.Identity;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Controllers
{
    public class DashboardController : Controller
    {

        #region Global Variable Declaration
        private static IScheduleMgmtFacade _iScheduleMgmtFacade = new ScheduleMgmtFacade();
        string userUUID = "";
        #endregion

        // GET: SchedulingMgmt/Dashboard
        public ActionResult Index()
        {
            GetUserDetails();
            return View("~/Areas/SchedulingMgmt/Views/Dashboard/DashboardIndex.cshtml");
        }
        //To get the count of published Audits
        public async Task<ActionResult> GetDashboardData()
        {
            GetUserDetails();
            string CompanyAlias = await GetUserAbbr(userUUID);
            string url = ScheduleServiceUrls.GetPublishedAuditCount+ CompanyAlias;
            DashboardModel dashboardObj = await _iScheduleMgmtFacade.GetPublishedAuditCount(url);
            return View("~/Areas/SchedulingMgmt/Views/Dashboard/DashboardIndex.cshtml", dashboardObj);
        }
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

     
        public async Task<string> GetUserAbbr(string uuid)
        {
            string lurl = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userDetails = await _iScheduleMgmtFacade.GetUserByUUID(lurl);
            string locationCode = userDetails.UserAccounts[0].Locations[0].LocationCode;
          

            string url = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userdata = await _iScheduleMgmtFacade.GetUserByUUID(url);
            UserPreference userPreference = userdata.UserPreferences[0];

            string surl = ScheduleServiceUrls.GetAllAuditorsByLocation + "?BMCode=" + userPreference.ModuleCode + "&LobCode=" + userPreference.LobCode + "&LocationCode=" + locationCode;
            List<AuditorModel> user = await _iScheduleMgmtFacade.GetAllAuditorsByLocation(surl);

            return user[0].AccountAbbreviation;
        }

    }
}