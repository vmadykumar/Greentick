using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterMgmt.CommonLayer.Models.DTO
{
    public class MasterChecklistDTO
    {
        public int ChecklistID { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string LocationName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string ChecklistCode { get; set; }
        public string ChecklistIcon { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistDescription { get; set; }
        public int TotalNoOfChecks { get; set; }
        public string ChecklistStatus { get; set; }
        public string CreatedBy { get; set; }
        public double MaxScore { get; set; }
        public string ChecklistCategory { get; set; }
        public string ChecklistType { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string SubAreaCode { get; set; }
        [NotMapped]
        public string AreaCode { get; set; }
        [NotMapped]
        public string LocationCode { get; set; }     
        public List<Entities.Check> Checks { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
