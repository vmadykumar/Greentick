using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
    public class LastPerformedDto
    {
        public string Company { get; set; }
        public string Location { get; set; }
        public string ChecklistCode { get; set; }
    }
}
