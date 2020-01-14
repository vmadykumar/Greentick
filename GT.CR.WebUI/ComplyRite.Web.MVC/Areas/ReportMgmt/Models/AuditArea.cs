using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Models
{
    public class AuditArea
    {
        public AuditArea()
        {
            AuditDetails = new List<AuditDetails>();
            Attachments = new List<string>();
        }
        public string AreaOfAudit { get; set; }
        public List<AuditDetails> AuditDetails { get; set; }
        public List<string> Attachments { get; set; }
        public int TotalCheckpointsPassed { get; set; }
        public int TotalCheckpointsFailed { get; set; }
        public float PassedCheckpointPercentage { get; set; }
        public float FailedCheckpointPercentage { get; set; }
    }
}