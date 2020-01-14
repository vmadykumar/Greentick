using AuditMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
    public class ChecklistDto
    {
       
        public int AuditChecklistID { get; set; }

        public int UserChecklistAuditInfoID { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistCode { get; set; }
        public string ChecklistIcon { get; set; }
        public string ChecklistMasterCode { get; set; }
        public string ChecklistCategory { get; set; }
        public string ChecklistType { get; set; }
        public int LastCheckPerformed { get; set; }
        public string ChecklistObservation { get; set; }
        public string ChecklistDescription { get; set; }
        public DateTime ChecklistScheduledStartDateTime { get; set; }
        public DateTime ChecklistScheduledEndDateTime { get; set; }
        public DateTime ChecklistStartDateTime { get; set; }
        public DateTime ChecklistEndDateTime { get; set; }
        public string ChecklistDepartment { get; set; }
        public string ChecklistArea { get; set; }
        public string ChecklistSubArea { get; set; }
        public string PerformedBy { get; set; }
        public string PerformedRole { get; set; }
        public string ChecklistStatus { get; set; }
        public int TotalNoOfChecks { get; set; }
        public int TotalYesResponse { get; set; }
        public int TotalNoResponse { get; set; }
        public double TotalScore { get; set; }
        public double MaxScore { get; set; }
        public double ChecklistCompliancePercentage { get; set; }
        public bool Submitted { get; set; }
        public DateTime SubmittedDateTime { get; set; }
        public bool Synced { get; set; }
        public DateTime SyncedDateTime { get; set; }
        public bool Locked { get; set; }
        public bool Pinned { get; set; }
        public int AuditID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<CheckDto> AuditCheck { get; set; }
    }
}
