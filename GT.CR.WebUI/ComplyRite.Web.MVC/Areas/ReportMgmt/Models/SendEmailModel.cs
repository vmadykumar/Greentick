﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Models
{
    public class SendEmailModel
    {

        public bool Async { get; set; }

        public string Body { get; set; }

        public string Subject { get; set; }
        public List<string> ToRecipients { get; set; }

        //Attachements Properties
        public string AttachementName { get; set; }
        public MemoryStream Attachements { get; set; }
        //public byte[] AttachementByteArray { get; set; }

    }
}