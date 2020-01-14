namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Controllers
{
    public class WaterMarkDetail
    {
        public WaterMarkDetail()
        {
            PageCustomize = PageCustomize.AllPages;
            Angle = 45;
            FillOpacity = 0.2f;
            StrokeOpacity = 0.3f;
        }
        public string Text { get; set; }
        public float FontSize { get; set; }
        public float Angle { get; set; }
        public float Xpos { get; set; }
        public float Ypos { get; set; }
        public float FillOpacity { get; set; }
        public float StrokeOpacity { get; set; }
        public PageCustomize PageCustomize { get; set; }
    }
}
