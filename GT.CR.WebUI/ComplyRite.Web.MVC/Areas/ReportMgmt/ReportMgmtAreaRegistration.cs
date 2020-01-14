using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt
{
    public class ReportMgmtAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ReportMgmt";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ReportMgmt_default",
                "ReportMgmt/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}