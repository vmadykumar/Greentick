using ComplyRite.Web.MVC.Areas.SchedulingMgmt.App_LocalResources;
using ComplyRite.Web.MVC.Facade.SchedulingMgmt;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.AppStore.Controllers
{
    public class AppUpController : Controller
    {
        #region Global Variables
        private static IScheduleMgmtFacade _iScheduleMgmtFacade = new ScheduleMgmtFacade();
        #endregion

        // GET: AppStore/AppUp
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> GetAppUpdate()
        {
            await GetUserDetails();
            return View("~/Areas/AppStore/Views/AppUp/AppUpdateIndex.cshtml");
        }

        public async Task<ActionResult> DownloadFile(string file)
        {
            string appdir = AppDomain.CurrentDomain.BaseDirectory + "Areas\\AppStore\\Content\\AppUpdate\\Android\\" + file;
            //string path = "Content\\AndroidApp\\"+file;
            if (!System.IO.File.Exists(appdir))
            {
                return HttpNotFound();
            }
            var fileBytes = System.IO.File.ReadAllBytes(appdir);
            var response = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = file
            };
            return response;

        }
        #region User Details
        /// <summary>
        /// Method to Get the User Details for the session
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetUserDetails()
        {
            string userId = User.Identity.GetUserId();
            string userName = User.Identity.GetUserName();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string uuid = claims.ToList().Where(i => i.Type.Split('/').LastOrDefault() == "muid").Select(j => j.Value).ToList()[0];
            System.Web.HttpContext.Current.Request.Headers["UUID"] = uuid;
            System.Web.HttpContext.Current.Request.Headers["UserName"] = userName;
            System.Web.HttpContext.Current.Request.Headers["UserId"] = userId;
            ViewBag.UUID = uuid;
            ViewBag.UserName = userName;
            ViewBag.UserId = userId;
            string userFullName = await GetUserFullName(uuid);
            ViewBag.UserFullName = userFullName;
            return true;
        }

        public async Task<string> GetUserFullName(string uuid)
        {
            string surl = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userdata = await _iScheduleMgmtFacade.GetUserByUUID(surl);
            string userFullName = userdata.UserBasicInfo.FirstName + " " + userdata.UserBasicInfo.LastName;
            return userFullName;
        }
        #endregion
    }
}