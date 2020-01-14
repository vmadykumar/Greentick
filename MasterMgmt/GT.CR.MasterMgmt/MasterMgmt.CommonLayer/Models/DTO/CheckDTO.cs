using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterMgmt.CommonLayer.Models.DTO
{
    public class CheckDTO
    {
        [Key]
        public int CheckID { get; set; }
        public string CheckCode { get; set; }
        public string CheckTitle { get; set; }
        public string CheckDescription { get; set; }
        public double Score { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }
}
