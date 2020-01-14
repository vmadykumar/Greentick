using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMgmt.CommonLayer.Models.Entities
{
    public class AuditCheckInfo
    {
        public int AuditCheckInfoID { get; set; }
        public List<AuditCheck> AuditCheck { get; set; }
    }
}
