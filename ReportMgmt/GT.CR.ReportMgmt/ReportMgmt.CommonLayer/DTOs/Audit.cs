using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportMgmt.CommonLayer.DTOs
{
    public class Audit
    {
        public int AuditID { get; set; }
       // public string UserID { get; set; }
        public List<Dictionary<string, string>> ManagerInfo { get; set; }//key: email, value: name
        public string AuditName { get; set; }
        public string AuditImage { get; set; }
       // public string AuditFBO { get; set; }
        //public string AuditLocation { get; set; }
        public string LocationCode { get; set; }
        public string LOBCode { get; set; }
        public string BMCode { get; set; }
        public string TeamName { get; set; }
       // public string AuditCity { get; set; }
        public string AuditCode { get; set; }
        public DateTime AuditScheduledStartDateTime { get; set; }
        public DateTime AuditScheduledEndDateTime { get; set; }
        public DateTime AuditStartDateTime { get; set; }
        public DateTime AuditEndDateTime { get; set; }
        public string AuditedBy { get; set; }
        public string AuditedByRole { get; set; }
        public string AssignedBy { get; set; }
        public DateTime AssignedDateTime { get; set; }
        public string AuditStatus { get; set; }
        public string AuditType { get; set; }
        public string AuditCategory { get; set; }
        public int TotalNumberOfChecklist { get; set; }
        public int TotalOpenChecklist { get; set; }
        public int TotalDraftChecklist { get; set; }
        public int TotalClosedChecklist { get; set; }
        public int TotalNoOfChecks { get; set; }
        public int TotalYesChecks { get; set; }
        public int TotalNoChecks { get; set; }
        public int AuditScore { get; set; }
        public int AuditMaxScore { get; set; }
        public double AuditCompliancePercentage { get; set; }
        public bool Submitted { get; set; }
        public DateTime SubmittedDateTime { get; set; }
        public bool Locked { get; set; }
        public bool Synced { get; set; }
        public DateTime SyncedDateTime { get; set; }
        public List<AuditChecklist> AuditChecklist { get; set; }
        public List<Attachements> Attachements { get; set; }
    }
}
