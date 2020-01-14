using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class Template
    {
        #region Template_Attributes
        [Key]
        public int TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }
        public string TemplateMessage { get; set; }
        #endregion

        #region FKRelationship
        public int? NotificationCategoryID { get; set; }
        [ForeignKey("NotificationCategoryID")]
        public NotificationCategory NotificationCategory { get; set; }

        public int? ReminderCategoryID { get; set; }
        [ForeignKey("ReminderCategoryID")]
        public ReminderCategory ReminderCategory { get; set; }
        #endregion
    }
}
