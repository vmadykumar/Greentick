using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
   public class UserChecklistAuditInfoDto
    {
        public int UserChecklistAuditInfoID { get; set; }
        public string UserID { get; set; }
        public string FBO { get; set; }
        public string FBOCode { get; set; }
        public string LocationName { get; set; }
        public string City { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LocationCode { get; set; }
        public List<AuditDto> Audit { get; set; }
        public List<ChecklistDto> AuditChecklists { get; set; }
    }
}
