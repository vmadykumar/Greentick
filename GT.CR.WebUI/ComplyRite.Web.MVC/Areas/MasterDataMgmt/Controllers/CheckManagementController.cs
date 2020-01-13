using ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.CheckMgmt;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.App_LocalResources;
using ComplyRite.Web.MVC.Facade.MasterDataMgmt;
using ComplyRite.Web.MVC.Facade.SchedulingMgmt;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.MasterDataMgmt.Controllers
{
    public class CheckManagementController : Controller
    {
        #region Global Variable Declaration
        private static ICheckManagementFacade _iCheckMgmtFacade = new CheckManagementFacade();
        private static IScheduleMgmtFacade _iScheduleMgmtFacade = new ScheduleMgmtFacade();
        #endregion

        #region
        /// <summary>
        /// Method to Get the User Details for the session
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetUserDetails()
        {
            string userId = User.Identity.GetUserId();
            string userName = User.Identity.GetUserName();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string uuid = claims.ToList().Where(i => i.Type.Split('/').LastOrDefault() == "muid").Select(j => j.Value).ToList()[0];
            System.Web.HttpContext.Current.Request.Headers["UUID"] = uuid;
            System.Web.HttpContext.Current.Request.Headers["UserName"] = userName;
            System.Web.HttpContext.Current.Request.Headers["UserId"] = userId;
            ViewBag.UUID = uuid;
            ViewBag.UserName = userName;
            ViewBag.UserId = userId;
            string userFullName = await GetUserFullName(uuid);
            ViewBag.UserFullName = userFullName;

            return true;
        }

        public async Task<string> GetUserFullName(string uuid)
        {
            string surl = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userdata = await _iScheduleMgmtFacade.GetUserByUUID(surl);
            string userFullName = userdata.UserBasicInfo.FirstName + " " + userdata.UserBasicInfo.LastName;
            return userFullName;
        }
        #endregion

        // VIEW:
        [HttpGet]
        public async Task<ActionResult> ViewChecks()
        {
            await GetUserDetails();
            List<CheckModel_New> AllChecks = await _iCheckMgmtFacade.GetAllChecks("/Master/GetAllChecks");
            ViewBag.ThumbnailUrl = ConfigurationManager.AppSettings["ScheduleMgmtServiceURL"];

            return View(AllChecks);
        }

        [HttpGet]
        public async Task<ActionResult> CheckDetails(string CheckCode)
        {
            await GetUserDetails();
            CheckModel_New Check = await _iCheckMgmtFacade.GetCheckByCheckCode("/Master/GetCheckByCheckCode?CheckCode=" + CheckCode);
            ViewBag.ImageUrl = ConfigurationManager.AppSettings["ScheduleMgmtServiceURL"];

            ViewBag.Feedback = (TempData["Feedback"] ?? "").ToString();
            ViewBag.Color = (TempData["Color"] ?? "").ToString();

            return View(Check);
        }

        // CREATE:
        [HttpGet]
        public async Task<ActionResult> CreateNewCheck()
        {
            await GetUserDetails();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewCheck(CheckModel_New Check)
        {
            if (ModelState.IsValid)
            {
                Check.CreatedDateTime = DateTime.UtcNow;
                Check.LastModifiedDateTime = DateTime.UtcNow;
                Check.CreatedBy = User.Identity.Name;
                Check.LastModifiedBy = User.Identity.Name;
                Check.Status = "Active";
                Check.Score = Check.Score ?? "0";

                // SAVE NEW IMAGE
                bool NewImageError = Check.CheckImageFile != null ? !SaveImageToServer(Check) : false;

                // RETURN IF SOME ERROR OCCURS
                if (NewImageError)
                {
                    await GetUserDetails();
                    ViewBag.Feedback = "Error while saving the image file!";
                    ViewBag.Color = "red";
                    return View();
                }

                // ADD CHECK
                string CheckCode = (string)await _iCheckMgmtFacade.CreateNewCheck("Master/CreateNewCheck", Check);

                if (CheckCode.Length != 0)
                {
                    TempData["Feedback"] = "Successfully saved the new check!";
                    TempData["Color"] = "green";
                    return RedirectToAction("CheckDetails", new { CheckCode });

                }
                else
                {
                    await GetUserDetails();
                    ViewBag.Feedback = "The new check could not be added due to some error!";
                    ViewBag.Color = "red";
                    return View();
                }
            }

            await GetUserDetails();
            ViewBag.Feedback = "Please provide the required fields!";
            ViewBag.Color = "red";
            return View();
        }

        // EDIT:
        [HttpGet]
        public async Task<ActionResult> EditCheck(string CheckCode)
        {
            await GetUserDetails();
            CheckModel_New Check = await _iCheckMgmtFacade.GetCheckByCheckCode("/Master/GetCheckByCheckCode?CheckCode=" + CheckCode);
            ViewBag.ImageUrl = ConfigurationManager.AppSettings["ScheduleMgmtServiceURL"];
            ViewBag.Feedback = (TempData["Feedback"] ?? "").ToString();
            ViewBag.Color = (TempData["Color"] ?? "").ToString();

            return View(Check);
        }

        [HttpPost]
        public async Task<ActionResult> EditCheck(CheckModel_New Check)
        {
            if (ModelState.IsValid)
            {
                Check.Score = Check.Score ?? "0";
                Check.LastModifiedDateTime = DateTime.UtcNow;
                Check.LastModifiedBy = User.Identity.Name;

                // DELETE OLD IMAGE
                bool OldImageError = Check.DeleteImage ? !DeleteOldImageFromServer(Check) : false;
                if (OldImageError)
                {
                    TempData["Feedback"] = "Error while deleting the old image file!";
                    TempData["Color"] = "red";
                    return RedirectToAction("EditCheck", new { Check.CheckCode });
                }

                // SAVE NEW IMAGE
                bool NewImageError = (Check.CheckImageFile != null) ? !SaveImageToServer(Check) : false;
                if (NewImageError)
                {
                    TempData["Feedback"] = "Error while saving the new image file!";
                    TempData["Color"] = "red";
                    return RedirectToAction("EditCheck", new { Check.CheckCode });
                }

                // UPDATE CHECK
                bool IsUpdated = (bool)await _iCheckMgmtFacade.EditCheck("Master/EditCheck", Check);

                if (IsUpdated)
                {
                    TempData["Feedback"] = "Successfully updated the check!";
                    TempData["Color"] = "green";
                    return RedirectToAction("CheckDetails", new { Check.CheckCode });
                }
                else
                {
                    TempData["Feedback"] = "This check could not be update due to some error!";
                    TempData["Color"] = "red";
                    return RedirectToAction("EditCheck", new { Check.CheckCode });
                }
            }

            TempData["Feedback"] = "Please provide the required fields!";
            TempData["Color"] = "red";
            return RedirectToAction("EditCheck", new { Check.CheckCode });
        }

        // VALIDATE DUPLICATE CHECK TITLE:
        public async Task<JsonResult> ValidateDuplicateCheckTitle(string CheckTitle, int CheckID = 0)
        {
            return Json(!await _iCheckMgmtFacade.ValidateDuplicateCheckTitle("Master/ValidateDuplicateCheckTitle?CheckTitle=" + System.Net.WebUtility.UrlEncode(CheckTitle) + "&CheckID=" + CheckID), JsonRequestBehavior.AllowGet);
        }

        // HELPERS:
        private bool SaveImageToServer(CheckModel_New Check)
        {
            try
            {
                string Prefix = DateTime.UtcNow.Ticks.ToString() + "_";
                Check.CheckImage = Prefix + Check.CheckImageFile.FileName;
                Check.CheckImageFile.SaveAs(Path.Combine(ConfigurationManager.AppSettings["CheckImagePath"], Prefix + Check.CheckImageFile.FileName));
                Check.CheckImageFile = null;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool DeleteOldImageFromServer(CheckModel_New Check)
        {
            try
            {
                System.IO.File.Delete(Path.Combine(ConfigurationManager.AppSettings["CheckImagePath"], Check.CheckImage));
                Check.CheckImage = null;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}