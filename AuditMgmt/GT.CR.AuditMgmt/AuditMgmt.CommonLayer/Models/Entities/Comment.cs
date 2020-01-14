using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class Comment
    {
        #region Comment_Attributes
        [Key]
        public int CommentID { get; set; }
        public string CommentDescription { get; set; }
        public string CommentedBy { get; set; }
        public DateTime CommentedDateTime { get; set; }
        public bool Pinned { get; set; }
        #endregion

        #region FKRelationship
        public int? AuditChecklistID { get; set; }
        [ForeignKey("AuditChecklistID")]
        public AuditChecklist AuditChecklist { get; set; }

        public int? AuditCheckID { get; set; }
        [ForeignKey("AuditCheckID")]
        public AuditCheck AuditCheck { get; set; }

        public int? AuditID { get; set; }
        [ForeignKey("AuditID")]
        public Audit Audit { get; set; }

        public List<Attachements> Attachements { get; set; }
        #endregion
    }
}
