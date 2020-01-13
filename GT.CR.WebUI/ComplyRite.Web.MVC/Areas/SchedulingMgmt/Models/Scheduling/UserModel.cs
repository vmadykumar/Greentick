using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling
{
    public class UserModel
    {

        public int UserChecklistAuditInfoId { get; set; }
        public string UserId { get; set; }
        public string LocationName { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public List<Audit> Audits { get; set; }
        public List<AuditChecklist> AuditChecklists { get; set; }
    }
}