using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class AuditHistory
    {
        #region AuditHistory_Attributes
        [Key]
        public int AuditHistoryID { get; set; }
        public string UserID { get; set; }
        public string AuditName { get; set; }
        public string AuditImage { get; set; }
        public string AuditLocation { get; set; }
        public string AuditCity { get; set; }

        public DateTime AuditStartDateTime { get; set; }
        public DateTime AuditEndDateTime { get; set; }
        public string AuditedBy { get; set; }
        public string AssignedBy { get; set; }
        public string AuditStatus { get; set; }
        public string AuditType { get; set; }
        public string AuditCategory { get; set; }

        #region AuditChecklist_Attributes
        public int TotalNumberOfChecklist { get; set; }
        public int TotalOpenChecklist { get; set; }
        public int TotalDraftChecklist { get; set; }
        public int TotalClosedChecklist { get; set; }
        #endregion
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
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        #endregion

        #region FKRelationship
        public int? AuditID { get; set; }
        [ForeignKey("AuditID")]
        public Audit Audit { get; set; }
        
        public int? CertificateID { get; set; }
        #endregion
         
    } 
}
