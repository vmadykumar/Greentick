using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.MasterDataMgmt
{
    public class MasterDataMgmtAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MasterDataMgmt";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MasterDataMgmt_default",
                "MasterDataMgmt/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}