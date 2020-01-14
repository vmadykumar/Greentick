using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling
{
    public class UserAccounts
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UUID { get; set; }
        public string UserType { get; set; }
        public DateTime DOB { get; set; }
        public string UserName { get; set; }
        public List<Location> Locations { get; set; }
    }
}