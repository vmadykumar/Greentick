using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class ReminderCategory
    {
        #region ReminderCategory_Attributes
        [Key]
        public int ReminderCategoryID { get; set; }
        public string Name { get; set; }
        public string ReminderCategoryCode { get; set; }
        #endregion

        #region FKRelationship
        public List<Template> Template { get; set; }
        #endregion
    }
}
