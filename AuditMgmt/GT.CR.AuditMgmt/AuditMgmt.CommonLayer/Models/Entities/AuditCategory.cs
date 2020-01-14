using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class AuditCategory
    {
        #region AuditCategory_Attributes
        [Key]
        public int AuditCategoryID { get; set; }
        public string Name { get; set; }
        public string AuditCategoryCode { get; set; }
        #endregion
    }
}
