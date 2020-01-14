using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMgmt.CommonLayer.Models.Entities
{
    public class AuditChecklistInfo
    {
        public int AuditChecklistInfoID { get; set; }
        public List<AuditCheck> AuditCheck { get; set; }
         
        public DateTime ChecklistScheduledStartDateTime { get; set; }
        public DateTime ChecklistScheduledEndDateTime { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string SubAreaCode { get; set; }

    }
}
