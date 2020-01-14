using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling
{
    public class AuditChecklist
    {
        #region Checklist Details
        public int AuditchecklistId { get; set; }
        public int ChecklistId { get; set; }
        public string ChecklistCode { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistMasterCode { get; set; }
        public string ChecklistIcon { get; set; }
        public string ChecklistCategory { get; set; }
        public string ChecklistStatus { get; set; }
        #endregion

        #region Location and Time Details
        public string ChecklistScheduledStartDateTime { get; set; }
        public string ChecklistScheduledEndDateTime { get; set; }
        public string ChecklistArea { get; set; }
        public string ChecklistSubArea { get; set; }
        #endregion

        #region Check Related Details
        public string TotalNoOfChecks { get; set; }
        public int TotalYesResponse { get; set; }
        public int TotalNoResponse { get; set; }
        public int TotalScore { get; set; }
        //public double MaxScore { get; set; }
        #endregion

        public List<AuditCheck> AuditChecks { get; set; }

    }
}