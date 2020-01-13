using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.AuditManagement
{
    public class Audit2
    {
        public int AuditId { get; set; }
        public string AuditTitle { get; set; }
        public string Location { get; set; }
        public int ChecklistCount { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public List<AuditChecklistModel> AuditChecklists { get; set; }
       

    }
}