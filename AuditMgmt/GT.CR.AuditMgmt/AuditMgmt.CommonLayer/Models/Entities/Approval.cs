using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class Approval
    {
        #region Approval_Attributes
        [Key]
        public int ApprovalID { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDateTime { get; set; }
        public string Remarks { get; set; }
        public string ApprovalStatus { get; set; }
        public string UserID { get; set; }
        #endregion

        #region FKRelationship
        public int? AuditChecklistID { get; set; }
        [ForeignKey("AuditChecklistID")]
        public AuditChecklist AuditChecklist { get; set; }
        #endregion
    }
}
