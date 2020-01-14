using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
   public class ReportDto
    {
        public int Closed { get; set; }
        public int Inprogress { get; set; }
        public int NotPerformed { get; set; }
        public int Open { get; set; }
        public string  LocationName { get; set; }
        public int Incomplete { get; set; }
    }
}
