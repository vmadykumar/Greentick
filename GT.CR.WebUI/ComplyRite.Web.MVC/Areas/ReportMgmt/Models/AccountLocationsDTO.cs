using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Models
{
   public class AccountLocationsDTO
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }

        public List<LocationsDTO> Locations { get; set; }
    }
}
