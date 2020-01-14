using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class CertificateType
    {
        #region CertificateType_Attributes
        [Key]
        public int CertificateTypeID { get; set; }
        public string Name { get; set; }
        public string CertificateTypeCode { get; set; }
        #endregion
    }
}
