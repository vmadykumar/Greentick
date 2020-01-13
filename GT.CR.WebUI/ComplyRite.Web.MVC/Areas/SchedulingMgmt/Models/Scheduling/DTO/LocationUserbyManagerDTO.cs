using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling.DTO
{
    public class LocationUserbyManagerDTO
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountAbbreviation { get; set; }

        public List<CityDTO> Cities { get; set; }

    }
}