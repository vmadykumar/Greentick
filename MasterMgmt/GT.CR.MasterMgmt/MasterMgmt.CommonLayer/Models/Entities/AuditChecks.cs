using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMgmt.CommonLayer.Models.Entities
{
    public class AuditCheck
    {
        public int AuditCheckID { get; set; }
        public int? AuditInfoID { get; set; }
        [ForeignKey("AuditInfoID")]
        public AuditInfo AuditInfo { get; set; }

        public int? AuditChecklistInfoID { get; set; }
        [ForeignKey("AuditChecklistInfoID")]
        public AuditChecklistInfo AuditChecklistInfo { get; set; }

        public int? AuditCheckInfoID { get; set; }
        [ForeignKey("AuditCheckInfoID")]
        public AuditCheckInfo AuditCheckInfo { get; set; }

        public int AreaChecksID { get; set; }
        [ForeignKey("AreaChecksID")]
        public AreaChecks AreaChecks { get; set; }


    }
}
