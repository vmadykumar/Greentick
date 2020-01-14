using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMgmt.CommonLayer.Models.DTO
{
    public class AuditChecklistDto
    {
        public AuditChecklistDto()
        {
           // AuditCheckDto = new List<AuditCheckDto>();
        }
        public int AuditChecklistInfoID { get; set; }
        public int ChecklistID { get; set; }
        public string ChecklistCode { get; set; }
        public DateTime ChecklistScheduledStartDateTime { get; set; }
        public DateTime ChecklistScheduledEndDateTime { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string SubAreaCode { get; set; }
        //public List<AuditCheckDto> AuditCheckDto { get; set; }
    }
}
