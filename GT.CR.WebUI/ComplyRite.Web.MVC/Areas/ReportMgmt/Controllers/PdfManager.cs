using System;
using System.Collections.Generic;
using System.IO;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Controllers
{
    public class PdfManager
    {

        public PdfManager()
        {

        }
        public string Name { get; set; }
        public string Path { get; set; }
        public Stream Inputstream { get; set; }
        public Byte[] InputBytes { get; set; }
        public BorderDetail Border { get; set; }
        public List<WaterMarkDetail> Watermarks { get; set; }
        public List<ImageDetail> Images { get; set; }
        public FooterTemplate Footer { get; set; }
    }
    public enum PageCustomize
    {
        AllPages,
        OnlyEvenPages,
        OnlyOddPages,
        OnlyFirstPage,
        OnlyLastPage
    };
}