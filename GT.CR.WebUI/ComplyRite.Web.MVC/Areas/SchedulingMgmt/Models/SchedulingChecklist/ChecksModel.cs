using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingChecklist
{
    public class ChecksModel
    {
        public int CheckId { get; set; }
        public string CheckTitle { get; set; }
        public string checkDescription { get; set; }
        public string checkAnswer { get; set; }
        public float score { get; set; }
    }
}