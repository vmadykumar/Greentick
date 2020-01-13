using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling
{
    public class SchedulingDTO
    {
        #region Schedule Basic Details
        public int ScheduleId { get; set; }
        public string TypeOfSchedule { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
        public string ScheduleStatus { get; set; }
        #endregion

        #region Checklist Schedule
        public int ChecklistId { get; set; }
        public string ChecklistName { get; set; }
        #endregion

        #region Audit Schedule
        public List<Audit> Audits { get; set; }
        #endregion

        #region Location Details
        public string Location { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        #endregion

        #region DataTime Details
        public string ScheduleCreatedDate { get; set; }
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