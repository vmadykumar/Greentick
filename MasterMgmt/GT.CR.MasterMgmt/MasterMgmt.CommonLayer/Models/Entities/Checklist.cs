using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMgmt.CommonLayer.Models.Entities
{
    public class Checklist
    {
        public int ChecklistID { get; set; }
        public string ChecklistCode { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistIcon { get; set; }
        public string ChecklistDescription { get; set; }
        public double TotalNoOfChecks { get; set; }
        public double MaxScore { get; set; }
        public string Status { get; set; }
        public string ChecklistCategory { get; set; }
        public string ChecklistType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<Media> Medias { get; set; }

        public List<AreaChecks> AreaChecks { get; set; }
        [NotMapped]
        public List<Check> Checks { get; set; }

    }
}
