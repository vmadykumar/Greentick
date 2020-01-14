using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt
{
    public class SchedulingMgmtAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SchedulingMgmt";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SchedulingMgmt_default",
                "SchedulingMgmt/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}