using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
    public class AuditInfoDTO
    {
        public AuditInfoDTO()
        {
            TeamName = "SAVE";
        }
        public string userID { get; set; }
        public string Role { get; set; }
        public List<string> UUIDs { get; set; }
        public string LOBCode { get; set; }
        public string BMCode { get; set; }
        public string LocationCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [DefaultValue("SAVE")]
        public string TeamName { get; set; }
        public List<CodeStatusDto> CodeStatus { get; set; }
    }
}
