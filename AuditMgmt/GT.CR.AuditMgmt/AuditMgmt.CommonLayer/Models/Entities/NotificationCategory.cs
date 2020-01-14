using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class NotificationCategory
    {
        #region NotificationCategory_Attributes
        [Key]
        public int NotificationCategoryID { get; set; }
        public string Name { get; set; }
        public string NotificationCategoryCode { get; set; }
        #endregion

        #region FKRelationship
        public List<Template> Template { get; set; }
        #endregion
    }
}
