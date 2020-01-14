using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class Reminder
    {
        #region Reminder
        [Key]
        public int ReminderID { get; set; }
        public string ReminderDescription { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public int ReminderSnooze { get; set; }
        public bool ReminderSent { get; set; }
        #endregion

        #region FKRelationship
       

        public int NotificationReminderTypeID { get; set; }
        [ForeignKey("NotificationReminderTypeID")]
        public NotificationReminderType NotificationReminderType { get; set; }

        public int ReminderCategoryID { get; set; }
        [ForeignKey("ReminderCategoryID")]
        public ReminderCategory ReminderCategory { get; set; }

        public int? AuditID { get; set; }
        [ForeignKey("AuditID")]
        public Audit Audit { get; set; }

        public int? AuditChecklistID { get; set; }
        [ForeignKey("AuditChecklistID")]
        public AuditChecklist AuditChecklist { get; set; }

        public List<UserReminder> UserReminder { get; set; }
        #endregion
    }
}
