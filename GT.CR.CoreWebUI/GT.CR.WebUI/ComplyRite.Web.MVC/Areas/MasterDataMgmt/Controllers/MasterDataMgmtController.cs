using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ComplyRite.Web.MVC.Areas.MasterDataMgmt.Controllers
{
    [Area("MasterDataMgmt")]
    public class MasterDataMgmtController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/MasterDataMgmt/Views/CheckMgmt/_CheckMgmtIndex.cshtml");
        }
        public ActionResult GetListOfChecks()
        {
            return View();
        }
    }
}