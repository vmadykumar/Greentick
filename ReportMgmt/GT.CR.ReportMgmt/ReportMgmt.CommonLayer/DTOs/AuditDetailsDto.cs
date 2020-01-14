using System;

namespace ReportMgmt.CommonLayer.DTOs
{
    public class AuditDetailsDto
    {
        public DateTime? TimeStamp { get; set; }
        public string AuditCheckpoint { get; set; }
        public string AuditorsResponse { get; set; }
        public string Comments { get; set; }
        public int Score { get; set; }
    }
}