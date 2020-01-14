using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMgmt.CommonLayer.Models.Entities
{
    public class Check
    {
        public int CheckID { get; set; }
        public string CheckCode { get; set; }
        public string CheckTitle { get; set; }
        public string CheckDescription { get; set; }
        public string CheckAnswer { get; set; }
        public string CheckImage { get; set; }
        public double Score { get; set; }
        public string Status { get; set; }
        public string CheckType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<Media> Medias { get; set; }
        public List<AreaChecks> AreaChecks { get; set; }

    }
}
