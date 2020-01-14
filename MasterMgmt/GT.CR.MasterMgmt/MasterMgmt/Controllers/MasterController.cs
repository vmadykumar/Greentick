
///-----------------------------------------------------------------
///   Namespace:   MasterMgmt.Controllers
///   Class:         MasterController
///   Description:    Controller for Master data
///   Author:        Keshav M                   Date: 21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------


using System;
using System.Collections.Generic;
using MasterMgmt.BusinessLayer.BusinessInterfaceLayer;
using MasterMgmt.CommonLayer.Models.DTO;
using MasterMgmt.CommonLayer.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using MasterMgmt.BusinessLayer.ExcelFactory;


namespace MasterMgmt.Controllers
{
    [Route("api/Master/[Action]")]
    public class MasterController : Controller
    {
        private readonly IMasterManager _masterManager = null; 
        private readonly LoadData _loadData;

        public MasterController(IMasterManager masterManager, LoadData loadData)
        {
            this._masterManager = masterManager;
            _loadData = loadData; 
        }

       
        /// <summary>
        /// Returns list of all checklists by Account
        /// </summary>
        /// <returns>List of Checklist</returns>
        [HttpGet]
        public List<MasterChecklistDTO> GetAllChecklistsByAccount(string Lobcode)
        {
            try
            {
                return _masterManager.GetAllChecklistsByAccount(Lobcode); // Returning list of all checklists
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets all the checklist code based on checklist code and subarea code
        /// </summary>
        /// <param name="ChecklistCode"></param>
        /// <param name="Subareacode"></param>
        /// <returns>Checklists</returns>
        [HttpGet]
        public MasterChecklistDTO GetChecklistByChecklistCode(string ChecklistCode, string Subareacode)
        {
            try
            {
                return _masterManager.GetChecklistByChecklistCode(ChecklistCode, Subareacode); // Returning a checklist
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Checks if a check title already exits in master data
        /// </summary>
        /// <param name="CheckTitle"></param>
        /// <param name="CheckID"></param>
        /// <returns></returns>
        [HttpGet]
        public bool ValidateDuplicateCheckTitle(string CheckTitle, int CheckID = 0)
        {
            try
            {
                return _masterManager.ValidateDuplicateCheckTitle(CheckTitle, CheckID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Checks if a checklist title already exits in master data
        /// </summary>
        /// <param name="ChecklistName"></param>
        /// <param name="ChecklistID"></param>
        /// <returns></returns>
        [HttpGet]
        public bool ValidateDuplicateChecklistTitle(string SubAreaCode,string ChecklistName, int ChecklistID = 0)
        {
            try
            {
                return _masterManager.ValidateDuplicateChecklistTitle(SubAreaCode,ChecklistName, ChecklistID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Checks if a checklist is already exits in master data
        /// </summary>
        /// <param name="ChecklistName"></param>
        /// <param name="ChecklistID"></param>
        /// <returns></returns>
        [HttpGet]
        public bool ValidateDuplicateChecklistChecks(string SubAreaCode, string Checks, int ChecklistID=0)
        {
            try
            {
                string[] CheckIDs = Checks.Split(",");
                int[] SelectedChecks = Array.ConvertAll(CheckIDs, int.Parse);
                return _masterManager.ValidateDuplicateChecklistChecks(SubAreaCode, SelectedChecks, ChecklistID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns list of all checks
        /// </summary>
        /// <returns>List of Checks</returns>
        [HttpGet]
        public List<Check> GetAllChecks()
        {
            try
            {
                return _masterManager.GetAllChecks(); // Returning list of all checks
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns a check
        /// </summary>
        /// <returns>Check</returns>
        [HttpGet]
        public Check GetCheckByCheckCode(string CheckCode)
        {
            try
            {
                return _masterManager.GetCheckByCheckCode(CheckCode); // Returning a check
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Saves list of checklist
        /// </summary>
        /// <param name="checklist"></param>
        /// <returns>True on success</returns>
        [HttpPost]
        public object CreateNewChecklist([FromBody]List<Checklist> checklist)
        {
            try
            {
                return _masterManager.CreateNewChecklist(checklist); // Saving a list of Checklists
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Saves list of checklist
        /// </summary>
        /// <param name="checklist"></param>
        /// <returns>True on success</returns>
        [HttpPost]
        public object EditChecklist([FromBody]Checklist checklist)
        {
            try
            {
                return _masterManager.EditChecklist(checklist); // Saving a list of Checklists
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Save a check
        /// </summary>
        /// <param name="check"></param>
        /// <returns>True on success</returns>
        [HttpPost]
        public bool SaveChecks(List<Check> checks)
        {
            try
            {
                return _masterManager.SaveChecks(checks); // Saving a Check
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public object CreateNewCheck([FromBody]Check Check)
        {
            try
            {
                return _masterManager.CreateNewCheck(Check); // Creating a Check
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public object EditCheck([FromBody]Check Check)
        {
            try
            {
                return _masterManager.EditCheck(Check); // Editing a Check
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get All Checklist With Checks By ChecklistID
        /// </summary>
        ///<param name="ChecklistID"></param>
        /// <returns>Checklist</returns>
        [HttpGet]
        public Checklist GetChecklistWithChecksByChecklistID(int ChecklistID)
        {
            try
            {
                return _masterManager.GetChecklistWithChecksByChecklistID(ChecklistID); // Returns Checklist with Checks based on ChecklistID
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get all checklists for particular area
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <param name="LocationCode"></param>
        /// <param name="DepartmentCode"></param>
        /// <param name="AreaCode"></param>
        /// <param name="SubAreaCode"></param>
        /// <returns>List of checklist for that area</returns>
        [HttpGet]
        public List<ChecklistDTO> GetChecklistForArea(string SubAreaCode)
        {
            try
            {
                return _masterManager.GetChecklistForArea(SubAreaCode); // Returns Checklists based on company & location details
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        public List<AuditInfo> GetAuditInfo(DateTime? StartDateTime, DateTime? EndDateTime)
        {
            return _masterManager.GetAuditInfo(StartDateTime, EndDateTime);
        }

        /// <summary>
        /// Get all checklists based on AuditID
        /// </summary>
        /// <param name="AuditID"></param>
        /// <returns>Checklists</returns>
        [HttpGet]
        public List<ChecklistDTO> GetChecklistsByAuditID(int AuditID)
        {
            return _masterManager.GetChecklistsByAuditID(AuditID);
        }

        /// <summary>
        /// Get all checks by checklistID and subarea code
        /// </summary>
        /// <param name="ChecklistID"></param>
        /// <param name="SubAreaCode"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Check> GetAllChecksByChecklistIDAndSubAreaCode(int ChecklistID, string SubAreaCode)
        {
            return _masterManager.GetAllChecksByChecklistID(ChecklistID, SubAreaCode);
        }

        /// <summary>
        /// Get all checks by checklistID
        /// </summary>
        /// <param name="ChecklistID"></param>
        /// <returns>Checks</returns>
        [HttpGet]
        public List<Check> GetAllChecksByChecklistID(int ChecklistID)
        {
            return _masterManager.GetAllChecksByChecklistID(ChecklistID);
        }

        /// <summary>
        /// Gets the count of published audits for a FBO
        /// </summary>
        /// <param name="Fbo"></param>
        /// <returns>Returns Published Count</returns>
        [HttpGet]
        public CountDto GetCountForPublishedAudits(string Fbo)
        {
            return _masterManager.GetCountForPublishedAudits(Fbo);
        }

        /// <summary>
        /// Saves AuditInformation For Scheduling the Audit
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns>AuditCode</returns>
        [HttpPost]
        public List<string> SaveAuditInfo([FromBody]List<AuditInfoDto> auditInfo)
        {
 
            return _masterManager.SaveAuditInfo(auditInfo);
        }


        /// <summary>
        /// Schedule Bulk audits  Data through excel 
        /// </summary>
        /// <param name="uploadedFile"></param>
        [HttpPost]
        public void ScheduleBUlkData(IFormFile uploadedFile)
        {

            #region Save File Local
            //Create new filename if file Exists and save file Locally
            if (uploadedFile == null)
                throw new FileNotFoundException("File not received to the server.\n Please check and try again.\n if this error persists contact adminstrator");

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, uploadedFile.FileName);
            if (System.IO.File.Exists(filePath))
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMddHHmmssfff") + uploadedFile.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }
            #endregion
            List<SheetInfo> sheetMappings = new List<SheetInfo>();
            sheetMappings.Add(new SheetInfo() { SheetNumber = 0, TableName = "ScheduleDataExcel", Procedure = "Sp_UploadBulkExcelData" });
            _loadData.toTable(filePath, sheetMappings);

        }

        /// <summary>
        /// Updates the published audits
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns>AuditCode</returns>
        [HttpPost]
        public List<string> UpdatePublishedAudits([FromBody]List<AuditInfoDto> auditInfo)
        {
            return _masterManager.UpdatePublishedAudits(auditInfo);
        }

    }
}