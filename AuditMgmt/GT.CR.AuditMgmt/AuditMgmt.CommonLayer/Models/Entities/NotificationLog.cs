using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class NotificationLog
    {
        #region Notification_Attributes
        [Key]
        public string NotificationID { get; set; }
        public string FirebaseTopic { get; set; }
        public string NotificationBody { get; set; }
        public DateTime NotificationDateTime { get; set; }
        public string NotificationStatus { get; set; }
        #endregion
    
    }
}
