using AuditMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
    public class AuditDto
    { 
        public int UserChecklistAuditInfoID { get; set; }
        public string UserID { get; set; }
        public string AuditName { get; set; }
        public string AuditFBO { get; set; }
        public int AuditID { get; set; }
        public string AuditCode { get; set; }
        public string AuditImage { get; set; }
        public string AuditLocation { get; set; }
        public string AuditCity { get; set; }
        public string FBOCode { get; set; }
        public DateTime AuditStartDateTime { get; set; }
        public string AuditObservation { get; set; }
        public DateTime AuditEndDateTime { get; set; }
        public DateTime AuditScheduledStartDateTime { get; set; }
        public DateTime AuditScheduledEndDateTime { get; set; }
        public string AuditedBy { get; set; }
        public string AuditedByRole { get; set; }
        public string AssignedBy { get; set; }
        public string AuditStatus { get; set; }
        public int TotalNumberOfChecklist { get; set; }
        public int TotalOpenChecklist { get; set; }
        public int TotalDraftChecklist { get; set; }
        public int TotalClosedChecklist { get; set; }
        public int TotalNoOfChecks { get; set; }
        public int TotalYesChecks { get; set; }
        public int TotalNoChecks { get; set; }
        public double AuditScore { get; set; }
        public double AuditMaxScore { get; set; }
        public double AuditCompliancePercentage { get; set; }
        public bool Submitted { get; set; }
        public DateTime SubmittedDateTime { get; set; }
        public bool Locked { get; set; }
        public bool Synced { get; set; }
        public DateTime SyncedDateTime { get; set; }
        public string AuditType { get; set; }
        public string AuditCategory { get; set; }
        public int? CertificateID { get; set; }
        public int? ScheduleID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public string LocationCode { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<ChecklistDto> AuditChecklist { get; set; }
    }
}
