using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Models
{
    public class AuditReport
    {
        public AuditReport()
        {
            Sections = new List<Section>(); 
        }
        public string AuditID { get; set; }
        public string CompanyLogo { get; set; }//Company being audited
        public string AuditReportType { get; set; }//Food safety
        public string Location { get; set; }
        public DateTime AuditDate { get; set; }
        public string PresentedBy { get; set; }
        public string CompanyName { get; set; }
        public string Team { get; set; }

        public string URL { get; set; }
        public string City { get; set; }
        public string Introduction { get; set; }
        public string Summary { get; set; }

        public String EmailRecipientName { get; set; }
        public String EmailRecipientId { get; set; }
        public string Methodology { get; set; }
        //public Section Section { get; set; }
        public List<Section> Sections { get; set; }
        public DateTime AuditScheduledDate { get; set; }
        public List<Dictionary<string, string>> ManagerInfo { get; set; }//key: email, value: name 

        
    }
}