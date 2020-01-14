using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReportMgmt.CommonLayer.DTOs
{
    public class AuditCheck
    {
       
      
        public int AuditCheckID { get; set; }
        public string CheckCode { get; set; }
        public string CheckName { get; set; }
        public string CheckImage { get; set; }
        public string CheckResponse { get; set; }
        public string CheckAnswer { get; set; }
        public string PerformedBy { get; set; }
        public DateTime PerformedDateTime { get; set; }
        public string Remark { get; set; }
        public int CheckScore { get; set; }
        public string CheckType { get; set; }
        public string CheckCategory { get; set; }
           
        public List<Attachements> Attachement { get; set; }
      
    }
}
