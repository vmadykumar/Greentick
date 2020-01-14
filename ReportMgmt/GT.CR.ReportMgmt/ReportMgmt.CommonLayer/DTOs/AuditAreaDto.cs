using System.Collections.Generic;
using System.Linq;
using System;

namespace ReportMgmt.CommonLayer.DTOs
{
    public class AuditAreaDto
    {
        public string AreaOfAudit { get; set; }
        public List<AuditDetailsDto> AuditDetails { get; set; }
        public List<string> Attachments { get; set; }
        public int TotalCheckpointsPassed { get; set; }
        public int TotalCheckpointsFailed { get; set; }
        public float PassedCheckpointPercentage
        {
            get
            {
                if(AuditDetails != null && AuditDetails.Any())
                {
                    var totalScore = (float)AuditDetails.Select(a => a.Score).Sum();
                    var passedScore = (float)AuditDetails.Where(a => a.AuditorsResponse.Equals("YES", StringComparison.OrdinalIgnoreCase)).Select(a => a.Score).Sum();
                    var passedCheckpointPercentage = passedScore * 100 / totalScore;
                    return passedCheckpointPercentage;
                }
                else
                {
                    return 0;
                }
            }
        }
        public float FailedCheckpointPercentage
        {
            get
            {
                if (AuditDetails != null && AuditDetails.Any())
                {
                    var totalScore = (float)AuditDetails.Select(a => a.Score).Sum();
                    var failedScore = (float)AuditDetails.Where(a => a.AuditorsResponse.Equals("NO", StringComparison.OrdinalIgnoreCase)).Select(a => a.Score).Sum();
                    var failedCheckpointPercentage = failedScore * 100 / totalScore;
                    return failedCheckpointPercentage;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}