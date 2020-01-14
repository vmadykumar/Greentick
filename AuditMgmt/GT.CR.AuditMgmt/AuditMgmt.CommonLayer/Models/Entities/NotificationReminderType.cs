using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class NotificationReminderType
    {
        #region NotificationReminderType_Attributes
        [Key]
        public int NotificationReminderTypeID { get; set; }
        public string Name { get; set; }
        public string NotificationReminderTypeCode { get; set; }
        #endregion
    }
}
