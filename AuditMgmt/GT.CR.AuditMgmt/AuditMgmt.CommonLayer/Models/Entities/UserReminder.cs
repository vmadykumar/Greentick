using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class UserReminder
    {
        #region UserReminder_Attributes
        [Key]
        public int UserReminderID { get; set; }
        public string UserID { get; set; }
        #endregion

        #region FKRelationship
        public int? ReminderID { get; set; }
        [ForeignKey("ReminderID")]
        public Reminder Reminder { get; set; }
        #endregion
    }
}
