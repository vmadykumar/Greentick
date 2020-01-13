using System;
using System.IO;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Controllers
{
    public class FooterTemplate
    {
        public FooterTemplate()
        {

            PageCustomize = PageCustomize.OnlyLastPage;
            YPosition = 720;
        }
        public string TemplatePath { get; set; }
        public Stream Inputstream { get; set; }
        public Byte[] InputBytes { get; set; }
        public float YPosition { get; set; }
        public float XPosition { get; set; }
        public PageCustomize PageCustomize { get; set; }

    }
}