using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.AuditManagement;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingAudit;
using ComplyRite.Web.MVC.Facade.SchedulingMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.App_LocalResources;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Controllers
{
    public class AuditController : Controller
    {
        #region Global Variable Declaration
        string userFullName = " ";
        string userUUID = " ";

        private static IScheduleMgmtFacade _iScheduleMgmtFacade = new ScheduleMgmtFacade();
        #endregion

        // GET: SchedulingMgmt/Audit
        public ActionResult Index()
        {
            return View();
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
            return true;
        }

        #region Helpers
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

        #endregion

        //To get the list of saved checklist for scheduling
        public async Task<ActionResult> GetDraftedAuditScheduleList()
        {
            //List<CheckModel> checks = await _iCheckMgmtFacade.GetDraftedChecks(status);
            ScheduleAuditModel checklist = ChecklistData();
            List<ScheduleAuditModel> checklists = new List<ScheduleAuditModel>();
            checklists.Add(checklist);
            return View("~/Areas/SchedulingMgmt/Views/Audit/_DraftedAuditScheduleList.cshtml", checklists);
        }

        //To get the list of published scheduled audits
        public async Task<ActionResult> GetPublishedAuditScheduleList()
        {
            //var _controllerInstance = new DashboardController();
            //_controllerInstance.GetUserDetails();
            await GetUserDetails();
            string url = ScheduleServiceUrls.GetScheduledAuditsList;
            List<AuditListModel> auditList = await _iScheduleMgmtFacade.GetPublishedAuditList(url);
            await GetUserDetails();
            string loggedInUserAbbr = await GetUserAbbr(userUUID);
            List<AuditListModel> finalAudits = auditList.FindAll(audit => audit.AuditFBO == loggedInUserAbbr);


            Session["AuditDetails"] = finalAudits;
            return View("~/Areas/SchedulingMgmt/Views/Audit/_PublishedAuditScheduleList.cshtml", finalAudits);
        }

        public ScheduleAuditModel ChecklistData()
        {
            return new ScheduleAuditModel { Location = "Whitefield, Bangalore", AuditName = "Mc'D Annual Audit", AssignedTo = "Yatin Arora", StartDate = "Dec 20,2018", StartTime="09:00 AM", EndDate = "Dec 26,2018", EndTime= "02:00 PM", ExpiryDate = "Dec 28,2018", ExpiryTime= "06:00 PM", CreatedBy = "Sharda Sharma", CreatedDate = "Dec 20,2018", CreatedTime="11:00 AM" };
        }


        //To get the list of Audits Created
        public async Task<ActionResult> GetAllAudits()
        {
            //List<CheckModel> checks = await _iCheckMgmtFacade.GetDraftedChecks(status);
            Audit2 audit = AuditData();
            List<Audit2> auditList = new List<Audit2>();
            auditList.Add(audit);
            auditList.Add(audit);
            auditList.Add(audit);
            return View("~/Areas/SchedulingMgmt/Views/Audit/_ViewAuditsCreated.cshtml", auditList);
        }
         public Audit2 AuditData()
        {
            return new Audit2 { Location = "Whitefield, Bangalore", AuditId = 1, AuditTitle = "Mc'D Annual Audit", ChecklistCount = 18, CreatedBy = "Sharda Sharma", CreatedOn = "08-12-2018"};
        }


        //To get the list of Checklist for an Audit Created
        public async Task<ActionResult> GetAuditDetailsByAuditId(int AuditID)
        {
            //var _controllerInstance = new DashboardController();
            //_controllerInstance.GetUserDetails();
            await GetUserDetails();
            string url = ScheduleServiceUrls.GetAuditDetailsbyAuditID+ AuditID;
            List<Checklist> checklist = await _iScheduleMgmtFacade.GetAuditDetailsByAuditId(url);           
           AuditListModel auditDetails = ((List<AuditListModel>)Session["AuditDetails"]).Where(x=>x.auditInfoID==AuditID).FirstOrDefault();
            auditDetails.checklistDetails = checklist;
            //Session["AuditDetails"] = null;
            return View("~/Areas/SchedulingMgmt/Views/Audit/_ViewSpecificCreatedAudit.cshtml", auditDetails);
        }

        public async Task<ActionResult> GetAuditDetailsForEditByAuditId(int AuditID)
        {
            await GetUserDetails();
            string url = ScheduleServiceUrls.GetAuditDetailsbyAuditID + AuditID;
            List<Checklist> checklist = await _iScheduleMgmtFacade.GetAuditDetailsByAuditId(url);
            AuditListModel auditDetails = ((List<AuditListModel>)Session["AuditDetails"]).Where(x => x.auditInfoID == AuditID).FirstOrDefault();
            auditDetails.checklistDetails = checklist;

            return Json(auditDetails, JsonRequestBehavior.AllowGet);
        }

        public AuditChecklistModel AuditChecklistData()
        {
            return new AuditChecklistModel {AuditId=1, ChecklistId=12, ChecklistArea="Kormangla, Bangalore",ChecklistSubArea="Kitchen", ChecklistName="Personal Hygiene",CountOfChecks=3};
        }

        //Get all checks for a Checklist by the Subarea Code and ChecklistIDs
        public async Task<ActionResult> GetCheckData(int checklistID, string subAreaCode)
        {
            string url = ScheduleServiceUrls.GetAllChecksByChecklistID + checklistID+ "&SubAreaCode="+ subAreaCode;
            List<CheckModel> checks = await _iScheduleMgmtFacade.GetCheckDataByChecklistID(url);
            return PartialView("_ViewChecksModal", checks);
        }

       
      
    }
}