using System;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Models
{
    public class AuditDetails
    {
        public DateTime? TimeStamp { get; set; }
        public string AuditCheckpoint { get; set; }
        public string AuditorsResponse { get; set; }
        public string Comments { get; set; }
        public int Score { get; set; }
        public float CompliancePercentage { get; set; }
    }
}