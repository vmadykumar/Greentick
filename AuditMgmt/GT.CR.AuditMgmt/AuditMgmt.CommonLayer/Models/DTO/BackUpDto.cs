using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.Models.DTO
{
    public class BackUpDto
    {
        public string FboCode { get; set; }
        public string LobCode { get; set; }
        public string LocationCode { get; set; }
        public string UUID { get; set; }
        public string Type { get; set; }
    }
}
