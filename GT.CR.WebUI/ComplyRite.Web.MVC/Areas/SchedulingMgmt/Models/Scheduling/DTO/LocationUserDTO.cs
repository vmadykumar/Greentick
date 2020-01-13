using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling.DTO
{
    public class LocationUserDTO
    {
        public string LocationName { get; set; }
        public string LocationCode { get; set; }
        public List<UserInfoDTO> Users { get; set; }
    }
}