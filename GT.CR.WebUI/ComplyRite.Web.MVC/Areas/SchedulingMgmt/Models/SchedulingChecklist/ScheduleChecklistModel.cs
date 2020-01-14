using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingChecklist
{
    public class ScheduleChecklistModel
    {
        public int ScheduleChecklistId { get; set; }

        #region Schedule Basic Details
        public int ChecklistId { get; set; }
        public string ChecklistName { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
        public string ChecklistScheduleStatus { get; set; }
        #endregion

        #region Location Details
        public string Location { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        #endregion

        #region Time Details
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string ScheduleStartDate { get; set; }
        public string ScheduleEndDate { get; set; }
        public string ScheduleExpiryDate { get; set; }
        public string ScheduleStartTime { get; set; }
        public string ScheduleEndTime { get; set; }
        public string ScheduleExpiryTime { get; set; }
        #endregion

        #region Schedule Options
        public string ThemeColor { get; set; }
        public string IsFullDay { get; set; }
        #endregion

       
      

    }
}