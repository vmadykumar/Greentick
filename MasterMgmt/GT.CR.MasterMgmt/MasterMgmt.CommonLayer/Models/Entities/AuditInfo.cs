using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMgmt.CommonLayer.Models.Entities
{
    public class AuditInfo

    {
        public int AuditInfoID { get; set; }
        public string AuditName { get; set; }
        public string AuditImage { get; set; }
        public string AuditCode { get; set; }
        public string AssignedToUserName { get; set; }
        public string AssignedToUserUUID { get; set; }
        public string AuditLocation { get; set; }
        public string AuditFBO { get; set; }
        public string AuditLocationCode { get; set; }
        public string FBOCode { get; set; }

        public DateTime AuditScheduledStartDateTime { get; set; }
        public DateTime AuditScheduledEndDateTime { get; set; }
        public DateTime AuditExpiryDateTime { get; set; }


        public string AuditType { get; set; }
        public string AuditCity { get; set; }
        public string AuditCategory { get; set; }
        public string AuditStatus { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }


        public List<AuditCheck> AuditCheck { get; set; }
    }
}
