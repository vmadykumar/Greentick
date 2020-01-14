using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class Attachements
    {
        #region Attachement_Attributes
        [Key]
        public int AttachementID { get; set; }
        public string AttachementName { get; set; }
        public string AttachementDescription { get; set; }
        public string AttachementFormat { get; set; }
        public double AttachementSize { get; set; }
        #endregion

        #region FKRelationship
        public int AttachementTypeID { get; set; }
        [ForeignKey("AttachementTypeID")]
        public AttachementType AttachementType { get; set; }
      
        public int? CommentID { get; set; }
        [ForeignKey("CommentID")]
        public Comment Comment { get; set; }

        public int? AuditCheckID { get; set; } 
        public int? AuditID { get; set; } 
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }
        public DateTime? DateTime { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }


        #endregion

        [NotMapped]
        public List<string> prevAttachment { get; set; }
    }
}
