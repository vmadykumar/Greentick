using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.AuditManagement
{
    public class AuditChecklistModel
    {
        public int AuditId { get; set; }
        public int ChecklistId { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistArea { get; set; }
        public string ChecklistSubArea { get; set; }
        public int CountOfChecks { get; set; }
    }
}
