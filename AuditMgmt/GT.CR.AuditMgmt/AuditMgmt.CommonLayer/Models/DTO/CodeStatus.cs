using System;

namespace AuditMgmt.CommonLayer.Models.DTO
{
    public class CodeStatusDto
    {
        public string ParentCode { get; set; }
        public string Code { get; set; }
        public string CodeStatus { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }
}