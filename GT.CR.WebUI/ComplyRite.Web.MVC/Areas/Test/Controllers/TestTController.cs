using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.Test.Controllers
{
    public class TestTController : Controller
    {
        // GET: Test/TestT
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public string Get()
        {
            return "asdfasdf";
        }
    }
}