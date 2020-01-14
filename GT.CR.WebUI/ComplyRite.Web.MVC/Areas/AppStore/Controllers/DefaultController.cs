using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.Test.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Test/Default
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public string Get()
        {
            return "asdfsdf";
        }
    }
}