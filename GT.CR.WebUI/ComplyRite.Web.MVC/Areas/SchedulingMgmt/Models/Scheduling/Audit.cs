using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling
{
    public class Audit
    {
        #region Audit Basic Details
        public int AuditId { get; set; }
        public string AuditName { get; set; }
        public string AuditImage { get; set; }
        public string AuditCode { get; set; }
        #endregion

        #region Location and Time Details
        public DateTime AuditScheduledStartDateTime { get; set; }
        public DateTime AuditScheudledEndDteTime { get; set; }
        public DateTime AssignedDateTime { get; set; }
        #endregion

        #region Other Audit Details
        public string AuditedBy { get; set; }
        public string AssignedBy { get; set; }
        public string AuditStatus { get; set; }
        public string AuditType { get; set; }
        public int TotalNofChecklist { get; set; }
        public int AuditScore { get; set; }
        public int AuditMaxScore { get; set; }
        #endregion
        
        public List<AuditChecklist> AuditChecklists { get; set; }




        //#region Audit Basic Details
        //public int AuditId { get; set; }
        //public string AuditTitle { get; set; }
        //public string CreatedBy { get; set; }
        //public string CreatedOn { get; set; }
        //#endregion

        //#region Audit Location Details
        //public string Location { get; set; }
        //public string Area { get; set; }
        //public string SubArea { get; set; }
        //#endregion

        //#region Audit Checklist Details
        //public int ChecklistCount { get; set; }
        //public List<Checklist> AuditChecklists { get; set; }
        //#endregion

    }
}