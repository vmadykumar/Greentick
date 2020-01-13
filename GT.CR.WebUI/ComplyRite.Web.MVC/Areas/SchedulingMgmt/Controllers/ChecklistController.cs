using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingChecklist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Controllers
{
    public class ChecklistController : Controller
    {
        // GET: SchedulingMgmt/Checklist
        public ActionResult Index()
        {
            return View();
        }

        //To get the list of saved checklist for scheduling
        public async Task<ActionResult> GetDraftedChecklist()
        {
            //List<CheckModel> checks = await _iCheckMgmtFacade.GetDraftedChecks(status);
            ScheduleChecklistModel checklist = ChecklistData();
            List<ScheduleChecklistModel> checklists = new List<ScheduleChecklistModel>();
            checklists.Add(checklist);
            checklists.Add(checklist);
            return View("~/Areas/SchedulingMgmt/Views/Checklist/_DraftedChecklist.cshtml", checklists);
        }

        public ScheduleChecklistModel ChecklistData()
        {
           
            return new ScheduleChecklistModel { Location = "Whitefield, Bangalore",Area="Kitchen", SubArea="", ChecklistName = "HF/Cleaning", AssignedTo= "Keshava", ScheduleStartDate = "12/26/2018", ScheduleEndDate = "12/26/2018", CreatedBy = "Sharda Sharma", CreatedDate = "12/19/2018",CreatedTime="03:00 PM", ChecklistScheduleStatus = "Active"};
        }


        //To get the list of saved checklist for scheduling
        public async Task<ActionResult> GetPublishedChecklist()
        {
            //List<CheckModel> checks = await _iCheckMgmtFacade.GetDraftedChecks(status);
            ScheduleChecklistModel checklist = ChecklistData();
            List<ScheduleChecklistModel> checklists = new List<ScheduleChecklistModel>();
            checklists.Add(checklist);
            checklists.Add(checklist);
            return View("~/Areas/SchedulingMgmt/Views/Checklist/_PublishedChecklist.cshtml", checklists);
        }


        public async Task<ActionResult> GetChecksForChecklistById()
        {
            //List<ChecksModel> checks = await _iCheckMgmtFacade.GetDraftedChecks(status);
            ChecksModel checks= ChecksData();
            List<ChecksModel> listOfChecks = new List<ChecksModel>();
            listOfChecks.Add(checks);
            listOfChecks.Add(checks);
            return View("~/Areas/SchedulingMgmt/Views/Checklist/_ViewChecksForChecklist.cshtml", listOfChecks);
        }
        public ChecksModel ChecksData()
        {
            return new ChecksModel { CheckId = 138, CheckTitle = "A/C Cleaning", checkDescription = "Are the air vents cleaned in the A/C?", checkAnswer = "Yes", score=2 };
        }


    }
}