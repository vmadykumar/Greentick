
/////-----------------------------------------------------------------
/////   Namespace:      AuditMgmt.Controllers
/////   Class:         AuditController
/////   Description:    Controller for Audit data
/////   Author:        Keshav M                   Date: 21/6/2017
/////   Notes:          <Notes>
/////   Revision History:
/////   Name:           Date:        Description:
/////-----------------------------------------------------------------
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
//using AuditMgmt.CommonLayer.Models.DTO;
//using AuditMgmt.CommonLayer.Models.Entities;
//using Microsoft.AspNetCore.Mvc;
//using System.IO;
//using System.Linq;
//using Microsoft.Extensions.Configuration;
//using AuditMgmt.CommonLayer.Utilities;


//namespace AuditMgmt.Controllers
//{
//    //[Produces("application/json")]
//    [Route("api/Audit/[Action]")]
//    // [Authorize]
//    public class AuditController : Controller
//    {
//        readonly IAuditManager _auditManager = null;
//        readonly IConfiguration configuration;
//        readonly string authorizationKey, firebaseURL;
//        public AuditController(IAuditManager auditManager, IConfiguration _Configuration)
//        {
//            _auditManager = auditManager;
//            configuration = _Configuration;
//            authorizationKey = configuration["authorizationKey"];
//            firebaseURL = configuration["firebaseURL"];
//        }

//        #region GetAuditMethods


//        /// <summary>
//        /// Gets all audits with checklists,checks
//        /// </summary>
//        /// <param name="userID"></param>
//        /// <param name="Role"></param>
//        /// <param name="LOBCode"></param>
//        /// <param name="BMCode"></param>
//        /// <param name="LocationCode"></param>
//        /// <param name="TeamName"></param>
//        /// <returns>List of Audits</returns>
//        [HttpPost]
//        public List<AuditDto> GetAuditsByUserID(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName = "SAVE")
//        {
//            try
//            {
//                return _auditManager.GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode, TeamName); //returning audits based on userId, role and location

//            }
//            catch (Exception e)
//            {
//                throw e;
//            }

//        }

//        #endregion

//        /// <summary>
//        /// Send Audit Sync Status
//        /// </summary>
//        public void SendAuditSyncStatus(bool status)
//        {
//            _auditManager.SendAuditSyncStatus(status);
//        }



//        #region Audit CUD

//        /// <summary>
//        /// Saves the list of audit details
//        /// </summary>
//        /// <param name="audits"></param>
//        [HttpPost]
//        public void SaveAudit([FromBody]List<Audit> audits)
//        {
//            try
//            {
//                _auditManager.SaveAudit(audits);
//            }
//            catch (Exception e)
//            {
//                throw e;
//            }
//        }

//        #endregion


//        #region Last Performed details

//        /// <summary>
//        /// Get last performed checklist details
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns>List of Last Performed Checklist Details</returns>
//        [HttpPost]
//        public List<ChecklistLastPerformedDetailsDto> GetLastPerformCheckListDetails([FromBody]List<LastPerformedDto> model)
//        {
//            try
//            {
//                return _auditManager.GetLastPerformCheckListDetails(model); // Returns list of last performed details for the checklist
//            }
//            catch (Exception e)
//            {
//                throw e;
//            }

//        }
//        #endregion

//        #region FileManagement

//        /// <summary>
//        /// Uploads a file
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        public async Task<IActionResult> UploadFile()
//        {
//            try
//            {

//                var files = Request.Form.Files;
//                foreach (var file in files) // Looping inside each file
//                {

//                    var path = Path.Combine(configuration["FilePath"].ToString(), file.FileName); // Combines filepath and filename to get the path for the target file

//                    using (var stream = new FileStream(path, FileMode.Create)) // Creates a new target file using the path 
//                    {
//                        await file.CopyToAsync(stream); //Copies the uploaded file content to the target file
//                    }
//                }
//                return files.Count != 0 ? Content("Files Uploaded Successfully") : Content("File not selected"); // If file count is not zero, display file added successfully otherwise display file not selected
//            }
//            catch (Exception e)
//            {

//                throw e;
//            }

//        }

//        /// <summary>
//        /// Downloads a file
//        /// </summary>
//        /// <param name="filename"></param>
//        /// <returns></returns>
//        [HttpGet]
//        public IActionResult Download(string filename)
//        {
//            Byte[] bytes = System.IO.File.ReadAllBytes(configuration["FilePath"].ToString() + filename);   // You can use your own method over here.         
//            return File(bytes, "image/jpeg");
//        }

//        #endregion

//    }
//}
