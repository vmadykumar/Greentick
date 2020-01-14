using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class UserChecklistAuditInfo
    {
        public UserChecklistAuditInfo()
        {
            Audit = new List<Audit>();
            AuditChecklists = new List<AuditChecklist>();
        }
        [Key]
        public int UserChecklistAuditInfoID { get; set; }
        public string UserID { get; set; }
        public string FBO { get; set; }
        public string FBOCode { get; set; }
        public string LocationName { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LocationCode { get; set; }
        public List<Audit> Audit { get; set; }
        public List<AuditChecklist> AuditChecklists { get; set; }
    }
}
