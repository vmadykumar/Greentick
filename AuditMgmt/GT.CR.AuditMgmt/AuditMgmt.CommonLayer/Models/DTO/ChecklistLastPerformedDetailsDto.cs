using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
    [NotMapped]
    public class ChecklistLastPerformedDetailsDto
    {
        [Key]
        public int ID { get; set; }
        public int TotalNoOfChecks { get; set; }
        public int TotalYesResponse { get; set; }
        public int TotalNoResponse { get; set; }
        public string ChecklistCode { get; set; }
        public string PerformedBy { get; set; }
        public string PerformedRole { get; set; }
        public DateTime ChecklistEndDateTime { get; set; }
        public DateTime ChecklistScheduledStartDateTime { get; set; }
        public string ChecklistSubArea { get; set; }
        public string ChecklistArea { get; set; }

    }
}
