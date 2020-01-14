using ReportMgmt.CommonLayer.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportMgmt.CommonLayer.DTOs
{
    public class UserChecklistAuditInfo
    {
        
        public int UserChecklistAuditInfoID { get; set; }
        public string UserID { get; set; }
        public string FBO { get; set; }
        public string LocationName { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public List<Audit> Audit { get; set; }
        public List<AuditChecklist> AuditChecklists { get; set; }
    }
}
