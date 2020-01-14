using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
   public class AttachementDto
    {
        public int AttachementID { get; set; }
        public string AttachementName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }

        public int AttachementTypeID { get; set; }
        [ForeignKey("AttachementTypeID")]
        public AttachementTypeDto AttachementType { get; set; }
    }
}
