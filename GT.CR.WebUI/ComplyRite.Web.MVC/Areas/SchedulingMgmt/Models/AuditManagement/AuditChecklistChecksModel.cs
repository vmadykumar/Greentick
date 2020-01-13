using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.AuditManagement
{
    public class AuditChecklistChecksModel
    {
        public int CheckId { get; set; }
        public string CheckQuestion { get; set; }
        public int ExpectedResponse { get; set; }
        public int Score { get; set; }
        public List<AuditChecklistChecksModel> Checks { get; set; }
    }
}