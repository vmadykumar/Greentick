using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReportMgmt.CommonLayer.DTOs
{
    public class Attachements
    {
        public int AttachementID { get; set; }
        public string AttachementName { get; set; }
        public string AttachementDescription { get; set; }
        public string AttachementFormat { get; set; }
        public double AttachementSize { get; set; }
        
    }
}
