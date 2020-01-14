using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.Entities
{
    public class Certificate
    {
        #region Certificate_Attributes
        [Key]
        public int CertificateID { get; set; }
        public string CertificateName { get; set; }
        public string CertificateDescription { get; set; }
        public DateTime IssuedDateTime { get; set; }
        public DateTime ValidityDateTime { get; set; }
        public string AuthorizedBy { get; set; }
        public string IssuerLogo { get; set; }
        public string IssuerName { get; set; }
        #endregion

        #region FKRelationship
        public int CertificateTypeID { get; set; }
        [ForeignKey("CertificateTypeID")]
        public CertificateType CertificateType { get; set; }
        #endregion
    }
}
