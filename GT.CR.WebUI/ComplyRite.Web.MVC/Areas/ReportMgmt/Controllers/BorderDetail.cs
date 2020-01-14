namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Controllers
{
    public class BorderDetail
    {
        public BorderDetail()
        {
            PageCustomize = PageCustomize.AllPages;
            LeftMargin = 15;
            TopMargin = 15;

            RightMargin = 15;

            BottomMargin = 15;
        }
        public float LeftMargin { get; set; }
        public float TopMargin { get; set; }

        public float RightMargin { get; set; }

        public float BottomMargin { get; set; }
        public PageCustomize PageCustomize { get; set; }

    }
}