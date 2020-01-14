using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class Audit
    {
        public Audit()
        {
            AuditChecklist = new List<AuditChecklist>();
            Attachements = new List<Attachements>();
        }
        #region Audit_Attributes
        [Key]
        public int AuditID { get; set; }

        public string AuditName { get; set; }
        public string AuditImage { get; set; }
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

        public string AuditObservation { get; set; }

        #region AuditChecklist_Attributes
        public int TotalNumberOfChecklist { get; set; }
        public int TotalOpenChecklist { get; set; }
        public int TotalDraftChecklist { get; set; }
        public int TotalClosedChecklist { get; set; }
        #endregion
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

        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }



        #endregion

        #region FKRelationship
        public int? CertificateID { get; set; }
        [ForeignKey("CertificateID")]
        public Certificate Certificate { get; set; }

        public int? ScheduleID { get; set; }
        [ForeignKey("ScheduleID")]
        public Schedule Schedule { get; set; }

        public int? UserChecklistAuditInfoID { get; set; } 
        public List<AuditHistory> AuditHistory { get; set; }
        public List<AuditChecklist> AuditChecklist { get; set; }
        public List<NotificationLog> Notification { get; set; }
        public List<Reminder> Reminder { get; set; }
        public List<Attachements> Attachements { get; set; }
        public List<Comment> Comments { get; set; }
        public List<ExternalAuditLog> ExternalAuditLog { get; set; }
        #endregion

        #region Not_Mapped_Attributes
        [NotMapped]
        public string LOBCode { get; set; }
        [NotMapped]
        public string BMCode { get; set; }
        [NotMapped]
        public string LocationCode { get; set; }
        [NotMapped]
        public string TeamName { get; set; }
        #endregion
    }
}
