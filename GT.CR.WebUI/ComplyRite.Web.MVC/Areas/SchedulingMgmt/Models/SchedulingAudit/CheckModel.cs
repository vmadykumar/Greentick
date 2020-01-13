using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingAudit
{
    public class CheckModel
    {
        public int checkID { get; set; }
        public string checkCode { get; set; }
        public string checkTitle { get; set; }
        public string checkDescription { get; set; }
        public string checkAnswer { get; set; }
        public float score { get; set; }
    }
}