using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
   public class SheetInfo
    {
        public int SheetNumber { get; set; }
        public string TableName { get; set; }
        public string Procedure { get; set; }
    }
}
