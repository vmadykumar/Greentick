using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class Schedule
    {
        #region Schedule
        [Key]
        public int ScheduleID { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ScheduleStatus { get; set; }
        #endregion

        #region FKRelationship
        public List<ScheduleHistory> ScheduleHistory { get; set; }
        #endregion
    }
}
