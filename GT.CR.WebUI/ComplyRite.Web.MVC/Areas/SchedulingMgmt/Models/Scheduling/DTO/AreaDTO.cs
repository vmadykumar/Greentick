using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling.DTO
{
    public class AreaDTO
    {
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string AreaDescription { get; set; }
        public List<SubAreaDTO> SubAreas { get; set; }
    }
}