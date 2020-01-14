using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class ExternalAuditLog
    {
        #region ExternalAuditLog_Attributes
        [Key]
        public int ExternalAuditLogID { get; set; }
        public DateTime AuditDateTime { get; set; }
        public string AuditedBy { get; set; }
        public string AuditSource { get; set; }
        public string LoggedBy { get; set; }
        public DateTime LogDateTime { get; set; }
        #endregion

        #region FKRelationship
        public int? AuditID { get; set; }
        [ForeignKey("AuditID")]
        public Audit Audit { get; set; }
        #endregion
    }
}
