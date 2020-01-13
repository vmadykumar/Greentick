using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling.DTO
{
    public class UserInfoDTO
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string MiddleName { set; get; }
        public string FullName { set; get; }
        public string UserName { set; get; }
        public string UUID { set; get; }
    }
}