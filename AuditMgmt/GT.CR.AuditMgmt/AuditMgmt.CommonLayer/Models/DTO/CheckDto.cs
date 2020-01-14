using AuditMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
    public class CheckDto
    {
        [Key]
        public int AuditCheckID { get; set; }
        public string CheckName { get; set; }
        public string CheckImage { get; set; }
        public string CheckResponse { get; set; }
        public string CheckDescription { get; set; }
        public string PerformedBy { get; set; }
        public DateTime PerformedDateTime { get; set; }
        public string Remark { get; set; }
        public float CheckScore { get; set; }
        public string CheckType { get; set; }
        public string CheckCategory { get; set; }
        public int? AuditChecklistID { get; set; }
        public string CheckAnswer { get; set; }
        public string CheckCode { get; set; }
        public string CheckStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<AttachementDto> Attachement { get; set; }
    }
}
