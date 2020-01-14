using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class UserNotification
    {
        #region UserNotification_Attributes
        [Key]
        public int UserNotificationID { get; set; }
        public string UserID { get; set; }
        #endregion

       
    }
}
