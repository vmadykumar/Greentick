using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMgmt.CommonLayer.DTOs
{
    public class EmailReportDto
    {

        public string AuditID { get; set; }
        public string AuditorUUID { get; set; }
        public string CompanyLogo { get; set; }//Company being audited
        public string CompanyName { get; set; }
        public string AuditReportType { get; set; }//Food safety
        public string Location { get; set; }
        public string LocationCode { get; set; }
        public string URL { get; set; }
        public string EmailRecipientId { get; set; }
        public string EmailRecipientName { get; set; }

        public string City { get; set; }
        public string LOBCode { get; set; }
        public string BMCode { get; set; }
        public string TeamName { get; set; }
        public string AuditDate { get; set; }
        public string AuditScheduledDate { get; set; }
        public List<Dictionary<string, string>> ManagerInfo { get; set; }//key: email, value: name
        public string PresentedBy { get; set; }
        public string Team { get; set; }
        public string Introduction { get; set; }
        public string Summary { get; set; }
        public string Methodology { get; set; }
        public List<string> Stickers { get; set; }
    }
}
