using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
  public class NotificationDto
    { 
        public int AuditID { get; set; }
        public string AuditCode { get; set; }
        public string AuditName { get; set; }
        public string AuditCity { get; set; }
        public string PerformedBy { get; set; }
        public DateTime AuditEndDateTime { get; set; }
        public string body { get; set; }
        public string title { get; set; }
        public int ChecklistID { get; set; }
        public string AuditImage { get; set; }
        public string AuditedByRole { get; set; }
        public double AuditCompliancePercentage { get; set; }
        public string AuditStatus { get; set; }
        public DateTime AuditScheduledStartDateTime { get; set; }
        public DateTime AuditScheduledEndDateTime { get; set; }
        public int TotalNoOfChecks { get; set; }
        public int TotalYesChecks { get; set; }
        public int TotalNoChecks { get; set; }
        public int TotalNumberOfChecklist { get; set; }
        public string AuditLocation { get; set; }


    }
}
