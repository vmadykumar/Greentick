using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class AuditChecklistHistory
    {
        #region AuditChecklistHistory_Attributes
        [Key]
        public int AuditChecklistHistoryID { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistCode { get; set; }
        public string ChecklistIcon { get; set; }
        public string ChecklistDescription { get; set; }
        #region AuditChecklistHistoryArea_Attributes
        public string ChecklistDepartment { get; set; }
        public string ChecklistArea { get; set; }
        public string ChecklistSubArea { get; set; }
        #endregion
        public string ChecklistStatus { get; set; }
        public string ChecklistCategory { get; set; }
        public string ChecklistType { get; set; }
        public int TotalNoOfChecks { get; set; }
        public int TotalYesResponse { get; set; }
        public int TotalNoResponse { get; set; }
        public double TotalScore { get; set; }
        public double MaxScore { get; set; }
        public double CompliancePercentage { get; set; }
        public string PerformedBy { get; set; }
        public string PerformedRole { get; set; }
        public DateTime ChecklistStartDateTime { get; set; }
        public DateTime ChecklistEndDateTime { get; set; }
        public bool Submitted { get; set; }
        public DateTime SubmittedDateTime { get; set; }
        public bool Synced { get; set; }
        public DateTime SyncedDateTime { get; set; }
        public bool Locked { get; set; }
        public bool Pinned { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        #endregion

        #region FKRelationship
        public int? AuditChecklistID { get; set; }
        [ForeignKey("AuditChecklistID")]
        public AuditChecklist AuditChecklist { get; set; }

        public int? NotificationID { get; set; }
        public int? ReminderID { get; set; }
        #endregion
    }
}
