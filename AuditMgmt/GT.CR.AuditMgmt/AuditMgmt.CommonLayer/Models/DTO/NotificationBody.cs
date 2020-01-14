using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
    public class NotificationBody
    { 
        public string NotificationId { get; set; }
        public string Topic { get; set; }
        public string body { get; set; }
    }
}
