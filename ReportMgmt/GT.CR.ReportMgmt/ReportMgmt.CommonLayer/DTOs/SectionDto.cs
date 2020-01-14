using System.Collections.Generic;

namespace ReportMgmt.CommonLayer.DTOs
{
    public class SectionDto
    {
        public string SectionName { get; set; }
        public List<AuditAreaDto> AuditAreas { get; set; }

        public SectionDto()
        {
            AuditAreas = new List<AuditAreaDto>();
        }
    }
}