using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

//[assembly: OwinStartupAttribute(typeof(ComplyRite.Web.MVC.Startup))]
namespace ComplyRite.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}