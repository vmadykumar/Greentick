using System;
using System.IO;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Controllers
{
    public class ImageDetail
    {
        public ImageDetail()
        {
            PageCustomize = PageCustomize.OnlyFirstPage;
            Xpos = 24;
            Ypos = 750;
            Height = 60.65f;
            Width = 105.21f;
        }
        public Stream Inputstream { get; set; }
        public Byte[] InputBytes { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Xpos { get; set; }
        public float Ypos { get; set; }
        public PageCustomize PageCustomize
        {
            get; set;


        }
    }
}