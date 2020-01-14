using MasterMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterMgmt.CommonLayer.Models.DTO
{
    public class ChecklistDTO
    {
        [Key]
        public int ChecklistID { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string ChecklistCode { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistDescription { get; set; }
        public DateTime ChecklistScheduledStartDateTime { get; set; }
        public DateTime ChecklistScheduledEndDateTime { get; set; }
        public double TotalNoOfChecks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public double MaxScore { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDateTime { get; set; }
        public string SubAreaCode { get; set; }
        public string AreaCode { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<CheckDTO> Checks { get; set; }
    }
}
