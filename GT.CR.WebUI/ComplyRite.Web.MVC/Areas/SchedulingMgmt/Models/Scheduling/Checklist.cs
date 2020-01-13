using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling
{
    public class Checklist
    {
        public int checklistID { get; set; }
        public string checklistCode { get; set; }
        public string checklistName { get; set; }
        public float? totalNoOfChecks { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime checklistScheduledStartDateTime { get; set; }
        public DateTime checklistScheduledEndDateTime { get; set; }

        public string areaName { get; set; }
        public string subAreaName { get; set; }
        public string subAreaCode { get; set; }
        //public int CountOfChecks { get; set; }
        public List<Checks> ChecklistChecks { get; set; }

    }
}
