using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComplyRite.Web.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
              namespaces: new string[] { ConfigurationManager.AppSettings["AccountControllerNamespace"] });

            //routes.MapRoute(
            //  name: "Index",
            //  url: "{controller}/{action}",
            //  defaults: new { accountName = "", moduleName = "", controller = "Home", action = "Index" },
            //  namespaces: new string[] { "ComplyRite.Web.MVC.Controllers" });




            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home_Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
