using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingAudit
{
    public class ScheduleAuditModel
    {

        #region Schedule Details
        public string AuditName { get; set; }
        public int AuditId { get; set; }
        public string AssignedTo { get; set; }
        public string CreatedBy { get; set; }
        public string ScheduleStatus { get; set; }
        #endregion

        #region Location Details
        public string Location { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        #endregion

        #region DataTime Details
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public string ExpiryDate { get; set; }
        public string ExpiryTime { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        #endregion


    }
}