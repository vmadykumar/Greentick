using ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.Checklist_Mgmt;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.App_LocalResources;
using ComplyRite.Web.MVC.Facade.MasterDataMgmt;
using ComplyRite.Web.MVC.Facade.SchedulingMgmt;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.MasterDataMgmt.Controllers
{
    public class ChecklistManagementController : Controller
    {
        #region Global Variable Declaration
        private static IChecklistManagementFacade _iChecklistMgmtFacade = new ChecklistManagementFacade();
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
            string[] UserNameAccount = await GetUserFullName(uuid);
            ViewBag.UserFullName = UserNameAccount[0];
            ViewBag.UserAccount = UserNameAccount[1];

            return true;
        }

        public async Task<string[]> GetUserFullName(string uuid)
        {
            string surl = ScheduleServiceUrls.GetUserDetailsByUUID + uuid;
            UserDetailsModel userdata = await _iScheduleMgmtFacade.GetUserByUUID(surl);
            string[] userNameAccount = new string[2];
            userNameAccount[0] = userdata.UserBasicInfo.FirstName + " " + userdata.UserBasicInfo.LastName;
            userNameAccount[1] = userdata.UserAccounts.Select(ua => ua.LobCode).FirstOrDefault();
            return userNameAccount;
        }
        #endregion

        // VIEW:
        [HttpGet]
        public async Task<ActionResult> ViewChecklists()
        {
            await GetUserDetails();
            string Account = ViewBag.UserAccount;
            List<ChecklistModel_New> AllChecklists = await _iChecklistMgmtFacade.GetAllChecklistsByAccount("/Master/GetAllChecklistsByAccount?Lobcode=" + Account);
            ViewBag.ThumbnailUrl = ConfigurationManager.AppSettings["ScheduleMgmtServiceURL"];
 
            return View(AllChecklists);
        }

        [HttpGet]
        public async Task<ActionResult> ChecklistDetails(string ChecklistCode, string SubAreaCode)
        {
            await GetUserDetails();
            ChecklistModel_New Checklist = await _iChecklistMgmtFacade.GetChecklistByChecklistCode("/Master/GetChecklistByChecklistCode?ChecklistCode=" + ChecklistCode + "&Subareacode=" + SubAreaCode);
            ViewBag.ImageUrl = ConfigurationManager.AppSettings["ScheduleMgmtServiceURL"];
            ViewBag.ThumbnailUrl = ConfigurationManager.AppSettings["ScheduleMgmtServiceURL"];

            ViewBag.Feedback = (TempData["Feedback"] ?? "").ToString();
            ViewBag.Color = (TempData["Color"] ?? "").ToString();

            return View(Checklist);
        }



        // VALIDATE DUPLICATE CHECKLIST TITLE:
        public async Task<JsonResult> ValidateDuplicateChecklistTitle(string SubAreaCode, string ChecklistName, int ChecklistID=0)
        {
            return Json(!await _iChecklistMgmtFacade.ValidateDuplicateChecklistTitle("Master/ValidateDuplicateChecklistTitle?SubAreacode="+ SubAreaCode + "&ChecklistName="+ChecklistName+"&ChecklistID=" + ChecklistID), JsonRequestBehavior.AllowGet);
        }


        // VALIDATE DUPLICATE CHECKLIST CHECKS:
        public Task<bool> ValidateDuplicateChecklistChecks(string SubAreaCode, int[] SelectedChecks, int ChecklistID = 0)
        {
            string TotalSelectedChecks = "";
            foreach (int check in SelectedChecks)
            {
                TotalSelectedChecks = TotalSelectedChecks + check + ',';
            }

            TotalSelectedChecks = TotalSelectedChecks.Substring(0, TotalSelectedChecks.Length - 1);

            return _iChecklistMgmtFacade.ValidateDuplicateChecklistTitle("Master/ValidateDuplicateChecklistChecks?SubAreaCode=" + System.Net.WebUtility.UrlEncode(SubAreaCode) + "&Checks=" + TotalSelectedChecks + "&ChecklistID=" + ChecklistID);

        }

        // CREATE
        [HttpGet]
        public async Task<ActionResult> CreateNewChecklist()
        {
            await GetUserDetails();

            // LOCATIONS:
            UserDetailsModel UserDetails = await _iChecklistMgmtFacade.GetLocationsByUUID("UserManager/GetUserDetailsByUUID?UUID=" + ViewBag.UUID);
            ViewBag.Locations = UserDetails.UserAccounts.FirstOrDefault().Locations;

            // CHECKS:
            ViewBag.Checks = await _iCheckMgmtFacade.GetAllChecks("/Master/GetAllChecks");
            //ViewBag.CheckTypes = ((List<Models.CheckMgmt.CheckModel_New>)ViewBag.Checks).Select(c => c.CheckType).Where(c => c != null).Distinct();

            ViewBag.Feedback = (TempData["Feedback"] ?? "").ToString();
            ViewBag.Color = (TempData["Color"] ?? "").ToString();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewChecklist(ChecklistModel_New Checklist)
        {
            if (ModelState.IsValid)
            {
                if(await ValidateDuplicateChecklistChecks(Checklist.SubAreaCode, Checklist.SelectedChecks, Checklist.ChecklistID))
                {
                    await GetUserDetails();
                    TempData["Feedback"] = "Checklist already exists!";
                    TempData["Color"] = "red";
                    return RedirectToAction("CreateNewChecklist");
                }
                ChecklistDTO ChecklistDTO = new ChecklistDTO
                {
                    ChecklistName = Checklist.ChecklistName,
                    ChecklistDescription = Checklist.ChecklistDescription,
                    ChecklistCategory = Checklist.ChecklistCategory,
                    ChecklistType = Checklist.ChecklistType,
                    CreatedBy = User.Identity.Name,
                    LastModifiedBy = User.Identity.Name,
                    CreatedDateTime = DateTime.UtcNow,
                    LastModifiedDateTime = DateTime.UtcNow,
                    Status = "Active"
                };

                foreach (var CheckID in Checklist.SelectedChecks)
                {
                    ChecklistDTO.AreaChecks.Add(new AreaChecks()
                    {
                        CheckID = CheckID,
                        SubAreaCode = Checklist.SubAreaCode
                    });
                }

                // SAVE NEW IMAGE
                bool NewImageError = Checklist.ChecklistIconFile != null ? !SaveImageToServer(Checklist, ChecklistDTO) : false;

                // RETURN IF SOME ERROR OCCURS
                if (NewImageError)
                {
                    await GetUserDetails();
                    TempData["Feedback"] = "Error while saving the image file!";
                    TempData["Color"] = "red";
                    return RedirectToAction("CreateNewChecklist");
                }

                // ADD CHECKLIST
                var Result = await _iChecklistMgmtFacade.CreateNewChecklist("Master/CreateNewChecklist", new List<ChecklistDTO>() { ChecklistDTO });
                string[] ChecklistCodes = ((IEnumerable)Result).Cast<object>().Select(x => x.ToString()).ToArray();

                if (ChecklistCodes.Count() != 0)
                {
                    TempData["Feedback"] = "Successfully saved the new checklist!";
                    TempData["Color"] = "green";
                    return RedirectToAction("ChecklistDetails", new { ChecklistCode = ChecklistCodes[0], Checklist.SubAreaCode });
                }
                else
                {
                    await GetUserDetails();
                    TempData["Feedback"] = "This is a duplicate checklist!";
                    TempData["Color"] = "red";
                    return RedirectToAction("CreateNewChecklist");
                }
            }

            await GetUserDetails();
            ViewBag.Feedback = "Please provide the required fields!";
            ViewBag.Color = "red";
            return View();
        }

        // DROPDOWN POPULATION CONTROLLER:
        [HttpGet]
        public async Task<string> GetAreaByUUIDAndLocationCode(string UUID, string LocationCode)
        {
            var UserDetails = await _iChecklistMgmtFacade.GetAreaByUUIDAndLocationCode("UserManager/GetAllUserAreasbyLocation?UUID=" + UUID + "&LocationCode=" + LocationCode);
            return UserDetails.ToString();
        }
        [HttpGet]
        public async Task<string> GetSubAreaByUUIDLocationCodeAndAreaCode(string UUID, string LocationCode, string AreaCode)
        {
            var UserDetails = await _iChecklistMgmtFacade.GetSubAreaByUUIDLocationCodeAndAreaCode("UserManager/GetAllUserSubAreasbyLocation?UUID=" + UUID + "&LocationCode=" + LocationCode + "&AreaCode=" + AreaCode);
            return UserDetails.ToString();
        }

        // HELPERS:
        private bool SaveImageToServer(ChecklistModel_New Checklist, ChecklistDTO ChecklistDTO)
        {
            try
            {
                string Prefix = DateTime.UtcNow.Ticks.ToString() + "_";
                ChecklistDTO.ChecklistIcon = Prefix + Checklist.ChecklistIconFile.FileName;
                Checklist.ChecklistIconFile.SaveAs(Path.Combine(ConfigurationManager.AppSettings["ChecklistImagePath"], ChecklistDTO.ChecklistIcon));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }







        // EDIT: TO DO
        private bool DeleteOldImageFromServer(ChecklistModel_New Checklist)
        {
            try
            {
                System.IO.File.Delete(Path.Combine(ConfigurationManager.AppSettings["ChecklistImagePath"], Checklist.ChecklistIcon));
                Checklist.ChecklistIcon = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpGet]
        public async Task<ActionResult> EditChecklist(string ChecklistCode, string SubAreaCode)
        {
            IEnumerable<Claim> Claims = ((ClaimsIdentity)User.Identity).Claims;
            string UUID = Claims.ToList().Where(i => i.Type.Split('/').LastOrDefault() == "muid").Select(j => j.Value).ToList()[0];

            ChecklistModel_New Checklist = await _iChecklistMgmtFacade.GetChecklistByChecklistCode("/Master/GetChecklistByChecklistCode?ChecklistCode=" + ChecklistCode + "&Subareacode=" + SubAreaCode);
           
            //ChecklistModel_New Checklist = ChecklistAndSubSreas.Checklist;

            UserDetailsModel UserDetails = await _iChecklistMgmtFacade.GetLocationsByUUID("UserManager/GetUserDetailsByUUID?UUID=" + UUID);
            ViewBag.Locations = UserDetails.UserAccounts.FirstOrDefault().Locations;
            ViewBag.Checks = await _iCheckMgmtFacade.GetAllChecks("/Master/GetAllChecks");
            ViewBag.UUID = UUID;

            int[] CheckIds = Checklist.Checks.Select(i => i.CheckID).ToArray();
            Checklist.SelectedChecks = CheckIds;
            ViewBag.Feedback = (TempData["Feedback"] ?? "").ToString();
            ViewBag.Color = (TempData["Color"] ?? "").ToString();

            ViewBag.ImageUrl= ConfigurationManager.AppSettings["ScheduleMgmtServiceURL"];

            return View(Checklist);
        }


        [HttpPost]
        public async Task<ActionResult> EditChecklist(ChecklistModel_New Checklist)
        {
            if (ModelState.IsValid)
            {

                if (await ValidateDuplicateChecklistChecks(Checklist.SubAreaCode, Checklist.SelectedChecks, Checklist.ChecklistID))
                {
                    await GetUserDetails();
                    TempData["Feedback"] = "Checklist already exists!";
                    TempData["Color"] = "red";
                    return RedirectToAction("EditChecklist", new { Checklist.ChecklistCode, Checklist.SubAreaCode });
                }
         
                ChecklistDTO ChecklistDTO = new ChecklistDTO
                {
                    ChecklistID=Checklist.ChecklistID,
                    ChecklistCode=Checklist.ChecklistCode,
                    ChecklistName = Checklist.ChecklistName,
                    ChecklistDescription = Checklist.ChecklistDescription,
                    ChecklistIcon=Checklist.ChecklistIcon,
                    ChecklistCategory = Checklist.ChecklistCategory,
                    ChecklistType = Checklist.ChecklistType,
                    LastModifiedBy = User.Identity.Name,
                    LastModifiedDateTime = DateTime.UtcNow,
                    CreatedBy = Checklist.CreatedBy,
                    CreatedDateTime = Checklist.CreatedDateTime,
                    Status = "Active"
                };

                foreach (var CheckID in Checklist.SelectedChecks)
                {
                    ChecklistDTO.AreaChecks.Add(new AreaChecks()
                    {
                        CheckID = CheckID,
                        SubAreaCode = Checklist.SubAreaCode
                    });
                }

                // DELETE OLD IMAGE
                bool OldImageError = Checklist.DeleteImage ? !DeleteOldImageFromServer(Checklist) : false;
                if (OldImageError)
                {
                    TempData["Feedback"] = "Error while deleting the old image file!";
                    TempData["Color"] = "red";
                    return RedirectToAction("EditChecklist", new { Checklist.ChecklistCode });
                }

                // SAVE NEW IMAGE
                bool NewImageError = Checklist.ChecklistIconFile != null ? !SaveImageToServer(Checklist, ChecklistDTO) : false;

                // RETURN IF SOME ERROR OCCURS
                if (NewImageError)
                {
                    await GetUserDetails();
                    TempData["Feedback"] = "Error while saving the image file!";
                    TempData["Color"] = "red";
                    return RedirectToAction("EditChecklist", new { Checklist.ChecklistCode, Checklist.SubAreaCode });
                }

                // UPDATE CHECKLIST
                var Result = await _iChecklistMgmtFacade.EditChecklist("Master/EditChecklist", ChecklistDTO );
                string[] ChecklistCodes = ((IEnumerable)Result).Cast<object>().Select(x => x.ToString()).ToArray();

                if (ChecklistCodes.Count() != 0)
                {
                    TempData["Feedback"] = "Successfully updated the checklist!";
                    TempData["Color"] = "green";
                    return RedirectToAction("ChecklistDetails", new { ChecklistCode = ChecklistCodes[0], Checklist.SubAreaCode });
                }
                else
                {
                    await GetUserDetails();
                    TempData["Feedback"] = "This is a duplicate checklist!";
                    TempData["Color"] = "red";
                    return RedirectToAction("EditChecklist", new { Checklist.ChecklistCode, Checklist. SubAreaCode });
                }
            }

            await GetUserDetails();
            ViewBag.Feedback = "Please provide the required fields!";
            ViewBag.Color = "red";
            return View();
        }

    }
}