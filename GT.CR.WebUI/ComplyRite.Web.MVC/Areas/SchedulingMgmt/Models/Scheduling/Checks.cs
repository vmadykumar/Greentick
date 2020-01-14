using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling
{
    public class Checks
    {
        public int CheckId { get; set; }
        public string CheckQuestion { get; set; }
        public int ExpectedResponse { get; set; }
        public int Score { get; set; }
    }
}