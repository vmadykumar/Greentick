using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling.DTO
{
    public class CityDTO
    {
        public string City { get; set; }

        public List<LocationUserDTO> LocationUsers { get; set; }
    }
}