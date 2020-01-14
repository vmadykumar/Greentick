using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class AuditType
    {
        #region AuditTypeAttributes
        [Key]
        public int AuditTypeID { get; set; }
        public string Name { get; set; }
        public string AuditTypeCode { get; set; }
        #endregion
    }
}
