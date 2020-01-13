using ComplyRite.Web.MVC.Areas.SchedulingMgmt.App_LocalResources;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling.DTO;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingAudit;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingChecklist;
using ComplyRite.Web.MVC.Facade.SchedulingMgmt;
using ComplyRite.Web.MVC.Helpers;
using MasterMgmt.CommonLayer.Models.DTO;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Controllers
{

    public class SchedulingManagementController : Controller
    {
        // Global Variables
        //string loggedInUserAbbr = "";
        string userFullName = " ";
        string userUUID = " ";

        private static IScheduleMgmtFacade _iScheduleMgmtFacade = new ScheduleMgmtFacade();
        // GET: SchedulingMgmt/SchedulingManagement
        public ActionResult Index()
        {
            return View();
        }

        #region Schedule Checklist and Audits Main and Partial Views
        public async Task<ActionResult> ScheduleAudit()
        {
            //var _controllerInstance = new DashboardController();
            //_controllerInstance.GetUserDetails();
            await GetUserDetails();
            
            ViewBag.OpenInEditMode = false;
            ViewBag.AuditID = 0;
            return View();
        }

        public async Task<ActionResult> EditScheduledAudit(string AuditID)
        {
            await GetUserDetails();
            ViewBag.OpenInEditMode = true;
            ViewBag.AuditID = AuditID;
            return View("~/Areas/SchedulingMgmt/Views/SchedulingManagement/ScheduleAudit.cshtml");
        }

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
            userFullName = await GetUserFullName(uuid);
            ViewBag.UserFullName = userFullName;
            userUUID = uuid;

            string url = ScheduleServiceUrls.GetUserDetailsByUUID + userUUID;
            UserDetailsModel userdata = await _iScheduleMgmtFacade.GetUserByUUID(url);
            UserPreference userPreference = userdata.UserPreferences[0];

            ViewBag.LobCode = userPreference.LobCode;

            return true;
        }

        public ActionResult ScheduleChecklist()
        {
            return View();
        }

        /// <summary>
        /// View Scheduled Audit for the day when clicking on a 
        /// </summary>
        /// <returns></returns>
        public async Task<PartialViewResult> GetScheduleViewAudit(DateTime StartDateTime, DateTime EndDateTime)
        {
            ViewBag.DatePassed = StartDateTime;
            //StartDateTime = StartDateTime.AddMinutes(timeZoneOffset);
            //EndDateTime = EndDateTime.AddMinutes(timeZoneOffset);;

            DateTime startTimeLocal = Convert.ToDateTime(StartDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss"));
            DateTime endTimeLocal = Convert.ToDateTime(EndDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss"));

            string startDate = StartDateTime.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss"); /*DateTime.SpecifyKind(StartDateTime, DateTimeKind.Utc);*/
            string endDate = EndDateTime.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss"); /*EndDateTime.ToUniversalTime();*/
            string url = ScheduleServiceUrls.GetScheduledAuditsList + "?StartDateTime=" + startDate + "&EndDateTime=" + endDate;// + "&EndDateTime=" + endDate;
            List<AuditListModel> dayAuditList = await _iScheduleMgmtFacade.GetPublishedAuditList(url);
            await GetUserDetails();
            string loggedInUserAbbr = await GetUserAbbr(userUUID);

            List<AuditListModel> validAudits = new List<AuditListModel>();

            //if (dayAuditList.Count > 0)
            //{
            //    foreach (var audit in dayAuditList)
            //    {
            //        DateTime auditStartTime = Convert.ToDateTime(Convert.ToDateTime(audit.auditStartDate + " 12:00:00 AM", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd'T'HH:mm:ss"));
            //        DateTime auditEndTime = Convert.ToDateTime(Convert.ToDateTime(audit.auditEndDate + " 11:59:59 PM", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd'T'HH:mm:ss"));
            //        if ((DateTime.Compare(startTimeLocal, auditStartTime) >= 0) && (DateTime.Compare(endTimeLocal, auditEndTime) <= 0))
            //        {
            //            validAudits.Add(audit);
            //        }
            //    }
            //}
            //else
            //{

            //}
            List<AuditListModel> finalAudits = dayAuditList.FindAll(audit => audit.AuditFBO == loggedInUserAbbr && audit.auditStatus == "Published");
            Session["AuditDetails"] = finalAudits;
            return PartialView("~/Areas/SchedulingMgmt/Views/SchedulingManagement/PartialViews/Audit/_ScheduleViewAudit.cshtml", finalAudits);
        }

        /// <summary>
        /// Get the view for adding the audit schedule
        /// </summary>
        /// <returns></returns>
        public PartialViewResult GetScheduleAddAudit()
        {
            return PartialView("~/Areas/SchedulingMgmt/Views/SchedulingManagement/PartialViews/Audit/_ScheduleAddAudit.cshtml");
        }

        /// <summary>
        /// View Scheduled Audit for the day when clicking on a date
        /// </summary>
        /// <returns></returns>
        public PartialViewResult GetScheduleViewChecklist()
        {
            return PartialView("~/Areas/SchedulingMgmt/Views/SchedulingManagement/PartialViews/Checklist/_ScheduleViewChecklist.cshtml");
        }

        /// <summary>
        /// Get the view for adding the audit schedule
        /// </summary>
        /// <returns></returns>
        public PartialViewResult GetScheduleAddChecklist()
        {
            return PartialView("~/Areas/SchedulingMgmt/Views/SchedulingManagement/PartialViews/Checklist/_ScheduleAddChecklist.cshtml");
        }
        #endregion 

        public ActionResult AddAudit()
        {
            return View("");
        }

        #region Scheduling Audit Methods

        /// <summary>
        /// Get all the active published schedules for the day
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetPublishedAuditScheduleList(DateTime StartDateTime, DateTime EndDateTime)
        {
            StartDateTime = StartDateTime.ToUniversalTime();
            EndDateTime = EndDateTime.ToUniversalTime();
            string url = ScheduleServiceUrls.GetScheduledAuditsList + "?=" + StartDateTime + "?=" + EndDateTime;
            List<AuditListModel> auditList = await _iScheduleMgmtFacade.GetPublishedAuditList(url);
            Session["AuditDetails"] = auditList;
            return Json(auditList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SaveAuditSchedule(UserModel auditSchedule)
        {
            string url = ScheduleServiceUrls.SaveAuditScheduled;
            var obj = await _iScheduleMgmtFacade.SaveAuditSchedule(url, auditSchedule);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        //Gives all the user details so that we can get the locations for the user
        public async Task<JsonResult> GetUserDetailsByUUID(string uuid)
        {
            string url = ScheduleServiceUrls.GetUserDetailsByUUID;
            List<UserDetailsModel> userDetails = await _iScheduleMgmtFacade.GetUserDetailsByUUID(url);
            return Json(userDetails, JsonRequestBehavior.AllowGet);
        }

        //public async Task<JsonResult> PostAuditData(string schedules)
        [HttpPost]
        //public async Task<JsonResult> PostAuditData(AuditViewModel schedule, ScheduleRepeatData repeatData)
        public async Task<JsonResult> PostAuditData(string scheduleJSON)
        {
            List<AuditViewModel> schedules = null;
            try
            {
                schedules = JsonConvert.DeserializeObject<List<AuditViewModel>>(scheduleJSON);
            }
            catch (Exception e)
            {
                throw e;
            }
            //ScheduleRepeatData repeatData = new ScheduleRepeatData();
            //List<AuditViewModel> schedules = repeatData.GetRepeatingSchedules(schedule, repeatData);
            //foreach (var schedule in schedules)
            //{
            //    schedule.AuditScheduledStartDateTime = schedule.AuditScheduledStartDateTime?.ToUniversalTime();
            //    schedule.AuditScheduledEndDateTime = schedule.AuditScheduledEndDateTime?.ToUniversalTime();
            //    schedule.AuditExpiryDateTime = schedule.AuditExpiryDateTime?.ToUniversalTime();
            //    schedule.CreatedDateTime = DateTime.UtcNow;
            //    foreach (var checklist in schedule.AuditChecklistDto)
            //    {
            //        checklist.checklistScheduledStartDateTime = (DateTime)schedule.AuditScheduledStartDateTime;
            //        checklist.checklistScheduledEndDateTime = (DateTime)schedule.AuditScheduledEndDateTime;
            //    }
            //}
            //data.AuditScheduledStartDateTime = data.AuditScheduledStartDateTime?.ToUniversalTime();
            //data.AuditScheduledEndDateTime = data.AuditScheduledEndDateTime?.ToUniversalTime();
            //data.AuditExpiryDateTime = data.AuditExpiryDateTime?.ToUniversalTime();

            //foreach (var checklist in data.AuditChecklistDto)
            //{
            //    checklist.checklistScheduledStartDateTime = (DateTime)data.AuditScheduledStartDateTime;
            //    checklist.checklistScheduledEndDateTime = (DateTime)data.AuditScheduledEndDateTime;
            //}

            //List<AuditViewModel> postData = new List<AuditViewModel>();
            //data.CreatedDateTime = DateTime.UtcNow;
            string url = ScheduleServiceUrls.SaveAuditScheduled;
            //postData.Add(data);
            //JavaScriptSerializer js = new JavaScriptSerializer();
            ////string jsonResult = js.Serialize(schedules);
            //var tt =   _iScheduleMgmtFacade.SaveAuditSchedule(url, schedules);
            //tt.Wait();
            //var s = tt.Result;


            JArray  savedAuditCodes = (JArray) await _iScheduleMgmtFacade.SaveAuditSchedule(url, schedules);
            var savedAuditCodesList = savedAuditCodes.Select(c => c.ToString()).ToList(); 
            //List<string> savedAuditCodes = await _iScheduleMgmtFacade.SaveAuditSchedule(url, schedules) as List<string>;

            return Json(savedAuditCodesList, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> PostUpdatedAuditData(string scheduleJSON)
        {
            List<AuditViewModel> schedules = null;
            try
            {
                schedules = JsonConvert.DeserializeObject<List<AuditViewModel>>(scheduleJSON);
            }
            catch (Exception e)
            {
                throw e;
            }
            string url = ScheduleServiceUrls.UpdateAuditScheduled;

            JArray savedAuditCodes = (JArray)await _iScheduleMgmtFacade.SaveAuditSchedule(url, schedules);
            var savedAuditCodesList = savedAuditCodes.Select(c => c.ToString()).ToList();

            return Json(savedAuditCodesList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> PublishAuditSchedule(List<string> auditCodes)
        {
            string auditIdsToAppend = "?AuditCodes=" + auditCodes[0];
            for (int i = 1; i < auditCodes.Count(); i++)
            {
                auditIdsToAppend = auditIdsToAppend + "&AuditCodes=" + auditCodes[i];
            }
            
            string url = ScheduleServiceUrls.PublishAuditSchedules + auditIdsToAppend;
            bool isAuditPublished = await _iScheduleMgmtFacade.PublishScheduledAudit(url, null);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonResult = js.Serialize(isAuditPublished);
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        // Method to get all the cities for the user
        public async Task<JsonResult> GetAllUserCitiesByUUID(string uuid)
        {
            string url = ScheduleServiceUrls.GetAllCitiesByUUID + "?UUID=" + uuid;
            List<City> cities = await _iScheduleMgmtFacade.GetAllCitiesByUUID(url);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        //Method to get all the location for the city for the user
        public async Task<JsonResult> GetAllLocationsByCityAndUUID(string uuid, string cityName)
        {
            string url = ScheduleServiceUrls.GetAllLocationByCityAndUUID + "?cityName=" + cityName + "&UUID=" + uuid;
            List<Location> cities = await _iScheduleMgmtFacade.GetAllLocationsByCityAndUUID(url);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all the Cities, Location and Users for a user 
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="LOBCode"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetAllLocationUserbyManager(string uuid, string LOBCode)
        {
            string url = ScheduleServiceUrls.GetAllLocationUserbyManager + "?UUID=" + uuid + "&LobCode=" + LOBCode;
            List<LocationUserbyManagerDTO> locationUsers = await _iScheduleMgmtFacade.GetAllLocationUserbyManager(url);
            return Json(locationUsers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all the areas and subareas for a location
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="locationCode"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetAllUserAreasandSubAreasbyLocation(string uuid, string locationCode)
        {
            string url = ScheduleServiceUrls.GetAllUserAreasandSubAreasbyLocation + "?UUID=" + uuid + "&LocationCode=" + locationCode;
            List<AreaDTO> areaSubareas = await _iScheduleMgmtFacade.GetAllUserAreasandSubAreasbyLocation(url);
            return Json(areaSubareas, JsonRequestBehavior.AllowGet);
        }



        public async Task<JsonResult> GetAllAuditorsByLocation(string uuid, string locationCode)
        {
            // URL to get the user preferences from the UUID
            // We need user preferences for LOBCode and ModuleCode, so that we can get all the auditors for the location.
            string surl = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userdata = await _iScheduleMgmtFacade.GetUserByUUID(surl);


            ViewBag.LOBCode = null;
            ViewBag.ModuleCode = null;
            if (userdata != null)
            {
                foreach (var data in userdata.UserPreferences)
                {
                    ViewBag.ModuleCode = data.ModuleCode;
                    ViewBag.LOBCode = data.LobCode;
                }
            }

            // Get all the auditors for the locaion. It requires Business Module  Code and The LOB Code
            string url = ScheduleServiceUrls.GetAllAuditorsByLocation + "?BMCode=" + ViewBag.ModuleCode + "&LobCode=" + ViewBag.LOBCode + "&LocationCode=" + locationCode;
            List<AuditorModel> auditors = await _iScheduleMgmtFacade.GetAllAuditorsByLocation(url);
            return Json(auditors, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetUserLocationsByUUID(string uuid)
        {
            string url = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userDetails = await _iScheduleMgmtFacade.GetUserByUUID(url);
            List<UserLocation> locations = new List<UserLocation>();
            if (userDetails != null)
            {
                foreach (var data in userDetails.UserAccounts)
                {
                    locations = data.Locations;
                }
            }
            return Json(userDetails.UserAccounts, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllUserAreasByLocation(string uuid, string locationCode)
        {
            string url = ScheduleServiceUrls.GetAllUserAreasbyLocation + "?UUID=" + uuid + "&LocationCode=" + locationCode;
            List<Area> areas = await _iScheduleMgmtFacade.GetAllUserAreasByLocation(url);
            return Json(areas, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllUserSubAreasbyLocation(string uuid, string locationCode, string areaCode)
        {
            string url = ScheduleServiceUrls.GetAllUserSubAreasbyLocation + "?UUID=" + uuid + "&LocationCode=" + locationCode + "&AreaCode=" + areaCode;
            List<SubArea> subAreas = await _iScheduleMgmtFacade.GetAllUserSubAreasbyLocation(url);
            return Json(subAreas, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllChecklistForAreaSubArea(string subAreaCode)
        {
            string url = ScheduleServiceUrls.GetAllChecklistForAreaSubArea + subAreaCode;
            List<AuditChecklist> checklists = await _iScheduleMgmtFacade.GetAllChecklistForAreaSubArea(url);
            return Json(checklists, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllChecksByChecklistID(string checklistId, string subAreaCode)
        {
            string url = ScheduleServiceUrls.GetAllChecksByChecklistID + checklistId + "&SubAreaCode=" + subAreaCode;
            List<AuditCheckViewModel> checks = await _iScheduleMgmtFacade.GetAllChecksByChecklistID(url);
            return Json(checks, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> GetUserFullName(string uuid)
        {
            string surl = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userdata = await _iScheduleMgmtFacade.GetUserByUUID(surl);
            string userFullName = userdata.UserBasicInfo.FirstName + " " + userdata.UserBasicInfo.LastName;
            return userFullName;
        }

        public async Task<string> GetUserAbbr(string uuid)
        {
            string lurl = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userDetails = await _iScheduleMgmtFacade.GetUserByUUID(lurl);
            string locationCode = userDetails.UserAccounts[0].Locations[0].LocationCode; ;

            string url = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userdata = await _iScheduleMgmtFacade.GetUserByUUID(url);
            UserPreference userPreference = userdata.UserPreferences[0];

            string surl = ScheduleServiceUrls.GetAllAuditorsByLocation + "?BMCode=" + userPreference.ModuleCode + "&LobCode=" + userPreference.LobCode + "&LocationCode=" + locationCode;
            List<AuditorModel> user = await _iScheduleMgmtFacade.GetAllAuditorsByLocation(surl);

            return user[0].AccountAbbreviation;
        }

        /// <summary>
        /// Method to get LobCode for a User
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<string> GetAuditorLobCode(string uuid)
        {
            string lobCode = "";
            string url = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userdata = await _iScheduleMgmtFacade.GetUserByUUID(url);
            if (userdata != null)
            {
                foreach (var data in userdata.UserPreferences)
                {
                    lobCode = data.LobCode; ;
                }
            }

            return lobCode;
        }
        #endregion

        #region Private Methods for Repeat

        // Test this Mehod
    
        #endregion

        #region SchedulingFunctionalitiesStubs

        List<ScheduleChecklistModel> scheduledChecklists = new List<ScheduleChecklistModel>();

        /// <summary>
        /// Method to Fetch the Schedules
        /// </summary>
        /// <returns></returns>
        public JsonResult GetScheduledChecklists()
        {


            ScheduleChecklistModel sChecklist1 = new ScheduleChecklistModel();
            sChecklist1.ChecklistName = "H/F Cleaning";
            sChecklist1.CreatedDate = "2018-12-15";
            sChecklist1.ScheduleStartDate = "2018-12-17 12:30 PM";
            sChecklist1.ScheduleStartTime = "15:30:00";
            sChecklist1.ScheduleEndTime = "18:00:00";

            ScheduleChecklistModel sChecklist2 = new ScheduleChecklistModel();
            sChecklist2.ChecklistName = "H/F Cleaning";
            sChecklist2.CreatedDate = "2018-12-15";
            sChecklist2.ScheduleStartDate = "2018-12-16";
            sChecklist2.ScheduleStartTime = "15:30:00";
            sChecklist2.ScheduleEndTime = "18:00:00";

            scheduledChecklists.Add(sChecklist1);
            scheduledChecklists.Add(sChecklist2);

            return Json(scheduledChecklists, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Save the schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public JsonResult SaveScheduleChecklist(SchedulingDTO schedule)
        {
            //scheduledChecklists.Add(schedule);
            return Json(scheduledChecklists, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllLocations(string locationId, string UUID)
        {
            //var locations = new List<SelectListItem>();
            //locations.Add(
            //    new SelectListItem()
            //    {
            //        Value = "Whitefield",
            //        Text = "Whitefield"
            //    }
            //);
            //locations.Add(
            //    new SelectListItem()
            //    {
            //        Value = "Brookfield",
            //        Text = "Brookfield"
            //    });

            var locations = new List<object>() {
                new {
                    Value = "New Location",
                    Text = "New Location"
                },
                 new {
                    Value = "old Location",
                    Text = "Old Location"
                }
            };


            return Json(locations, JsonRequestBehavior.AllowGet);
        }

        // Get JSON of all the published Audits
        public async Task<JsonResult> GetAllPublishedSchedules()
        {
            //var _controllerInstance = new DashboardController();
            //_controllerInstance.GetUserDetails();
            string url = ScheduleServiceUrls.GetScheduledAuditsList;
            List<AuditListModel> auditList = await _iScheduleMgmtFacade.GetPublishedAuditList(url);

            await GetUserDetails();
            string loggedInUserAbbr = await GetUserAbbr(userUUID);
            List<AuditListModel> finalAudits = auditList.FindAll(audit => audit.AuditFBO == loggedInUserAbbr && audit.auditStatus == "Published");
            return Json(finalAudits, JsonRequestBehavior.AllowGet);
        }

        #endregion



        #region Old Implementation with Drag & Drop
        public PartialViewResult GetAddScheduleChecklistView()
        {
            return PartialView("~/Areas/SchedulingMgmt/Views/SchedulingManagement/PartialViews/_AddScheduleChecklist.cshtml");
        }

        public PartialViewResult GetEditScheduleChecklistView()
        {
            return PartialView("~/Areas/SchedulingMgmt/Views/SchedulingManagement/PartialViews/_EditScheduleChecklist.cshtml");
        }

        public PartialViewResult GetViewScheduleChecklistView()
        {
            return PartialView("~/Areas/SchedulingMgmt/Views/SchedulingManagement/PartialViews/_ViewTilesScheduleChecklist.cshtml");
        }

        #endregion
    }


}