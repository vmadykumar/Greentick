using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class AttachementType
    {
        #region AttachementType_Attributes
        [Key]
        public int AttachementTypeID { get; set; }
        public string Name { get; set; }
        public string AttachementTypeCode { get; set; }
        #endregion
    }
}
