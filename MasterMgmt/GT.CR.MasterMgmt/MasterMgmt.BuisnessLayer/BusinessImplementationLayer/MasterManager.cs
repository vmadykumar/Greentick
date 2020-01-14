
///-----------------------------------------------------------------
///   Namespace:  MasterMgmt.BusinessLayer.BusinessImplementationLayer
///   Class:         MasterManager
///   Description:    Business layer for Master data
///   Author:        Keshav M                   Date: 21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------

using MasterMgmt.BusinessLayer.BusinessInterfaceLayer;
using MasterMgmt.CommonLayer.Models.DTO;
using MasterMgmt.CommonLayer.Models.Entities;
using MasterMgmt.DataLayer.DataInterfaceLayer;
using MasterMgmt.Exceptions;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MasterMgmt.CommonLayer.ExternalServices;
using System.Linq;

namespace MasterMgmt.BusinessLayer.BusinessImplementationLayer
{
    public class MasterManager : IMasterManager
    {
        static string lockObj = "";
        static List<AccountMasterDTO> _accountMasterDTO = null;
        readonly IMasterRepository _masterRepository = null;
        private readonly IConfiguration _configuration;
        readonly string AMURL;
        public MasterManager(IMasterRepository masterRepository, IConfiguration Configuration)
        {
            _masterRepository = masterRepository;
            _configuration = Configuration;
            AMURL = _configuration["AMURL"];
            GetSubDetailInfo();
        }

        public List<AccountMasterDTO> GetSubDetailInfo()
        {
            lock (lockObj)
            {
                if (_accountMasterDTO == null)
                {
                    _accountMasterDTO = new List<AccountMasterDTO>();
                    _accountMasterDTO = new ExternalServiceUtility().GetDataFromURL<List<AccountMasterDTO>>
                         (AMURL + "api/TeamManager/GetSubAreaDetail");
                }
            }
            return _accountMasterDTO;
        }

        public string RefreshCache()
        {
            lock (lockObj)
            {
                _accountMasterDTO = null;
            }
            return "SUCCESS";
        }

        
        /// <summary>
        /// Returns list of all checklists By Account
        /// </summary>
        /// <returns>List of Checklist</returns>
        public List<MasterChecklistDTO> GetAllChecklistsByAccount(string Lobcode)
        {
            try
            {

               var SubAreaCodes = _accountMasterDTO.Where(am => am.AccountCode == Lobcode).Select(am => am.SubAreaCode).Distinct().ToList();


                var _checklists = _masterRepository.GetAllChecklistsBySubAreas(SubAreaCodes);
                foreach (var checklist in _checklists)
                {
                    var subareaInfo = _accountMasterDTO.FirstOrDefault(a => a.SubAreaCode == checklist.SubAreaCode);
                    if (subareaInfo == null) continue;
                    checklist.AreaName = subareaInfo.AreaName;
                    checklist.SubAreaName = subareaInfo.SubAreaName;
                    checklist.LocationName = subareaInfo.OfficeLocationName;
                    checklist.AreaCode = subareaInfo.AreaCode;
                    checklist.SubAreaCode = subareaInfo.SubAreaCode;
                    checklist.LocationCode = subareaInfo.OfficeLocationCode;
                }
                return _checklists;
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Returns list of all checks
        /// </summary>
        /// <returns>List of Checks</returns>
        public List<Check> GetAllChecks()
        {
            try
            {
                return _masterRepository.GetAllChecks();
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Checks if a check title already exits in master data
        /// </summary>
        /// <param name="CheckTitle"></param>
        /// <param name="CheckID"></param>
        /// <returns></returns>
        public bool ValidateDuplicateCheckTitle(string CheckTitle, int CheckID)
        {
            try
            {
                return _masterRepository.ValidateDuplicateCheckTitle(CheckTitle, CheckID);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Checks if a checklist title already exits in master data
        /// </summary>
        /// <param name="ChecklistName"></param>
        /// <param name="ChecklistID"></param>
        /// <returns></returns>
        public bool ValidateDuplicateChecklistTitle(string SubAreaCode,string ChecklistName, int ChecklistID)
        {
            try
            {
                return _masterRepository.ValidateDuplicateChecklistTitle(SubAreaCode,ChecklistName, ChecklistID);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Checks if a checklist is already exits in master data
        /// </summary>
        /// <param name="SubAreaCode"></param>
        /// <param name="Checks"></param>
        /// <param name="ChecklistID"></param>
        /// <returns></returns>
        public bool ValidateDuplicateChecklistChecks(string SubAreaCode, int[] Checks, int ChecklistID)
        {
            try
            {
                return _masterRepository.ValidateDuplicateChecklistChecks(SubAreaCode, Checks, ChecklistID);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }
        /// <summary>
        /// Returns a check
        /// </summary>
        /// <returns>Check</returns>
        public Check GetCheckByCheckCode(string CheckCode)
        {
            try
            {
                return _masterRepository.GetCheckByCheckCode(CheckCode);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Returns a checklist
        /// </summary>
        /// <returns>Checklist</returns>
        public MasterChecklistDTO GetChecklistByChecklistCode(string ChecklistCode, string Subareacode)
        {
            try
            {
                var checklist = _masterRepository.GetChecklistByChecklistCode(ChecklistCode, Subareacode);
                var subareaInfo = _accountMasterDTO.FirstOrDefault(a => a.SubAreaCode == Subareacode);
                checklist.AreaName = subareaInfo?.AreaName;
                checklist.SubAreaName = subareaInfo?.SubAreaName;
                checklist.LocationName = subareaInfo?.OfficeLocationName;
                checklist.AreaCode = subareaInfo?.AreaCode;
                checklist.SubAreaCode = subareaInfo?.SubAreaCode;
                checklist.LocationCode = subareaInfo?.OfficeLocationCode;
                return checklist;
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Saves list of checklist
        /// </summary>
        /// <param name="checklist"></param>
        /// <returns>True on success</returns>
        public object CreateNewChecklist(List<Checklist> checklist)
        {
            try
            {
                return _masterRepository.CreateNewChecklist(checklist);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }


        /// <summary>
        /// Saves list of checklist
        /// </summary>
        /// <param name="checklist"></param>
        /// <returns>True on success</returns>
        public object EditChecklist(Checklist checklist)
        {
            try
            {
                return _masterRepository.EditChecklist(checklist);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Save a check
        /// </summary>
        /// <param name="check"></param>
        /// <returns>True on success</returns>
        public bool SaveChecks(List<Check> checks)
        {
            try
            {
                return _masterRepository.SaveChecks(checks);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Save a check
        /// </summary>
        /// <param name="check"></param>
        /// <returns>True on success</returns>
        public object CreateNewCheck(Check Check)
        {
            try
            {
                return _masterRepository.CreateNewCheck(Check);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Save a check
        /// </summary>
        /// <param name="check"></param>
        /// <returns>True on success</returns>
        public object EditCheck(Check Check)
        {
            try
            {
                return _masterRepository.EditCheck(Check);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Get All Checklist With Checks By ChecklistID
        /// </summary>
        /// <param name="ChecklistID"></param>
        /// <returns>Checklist</returns>
        public Checklist GetChecklistWithChecksByChecklistID(int ChecklistID)
        {
            try
            {
                return _masterRepository.GetChecklistWithChecksByChecklistID(ChecklistID);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
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
        public List<ChecklistDTO> GetChecklistForArea(string SubAreaCode)
        {
            try
            {
                return _masterRepository.GetChecklistForArea(SubAreaCode);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        public List<AuditInfo> GetAuditInfo(DateTime? StartDateTime, DateTime? EndDateTime)
        {
            if (StartDateTime != null && EndDateTime != null)
            {
                return _masterRepository.GetAuditInfo(DateTime.Parse(StartDateTime.ToString()), DateTime.Parse(EndDateTime.ToString()));
            }
            else
            {
                return _masterRepository.GetAuditInfo();
            }

        }

        public List<ChecklistDTO> GetChecklistsByAuditID(int AuditID)
        {
            return _masterRepository.GetChecklistsByAuditID(AuditID);
        }
        public List<Check> GetAllChecksByChecklistID(int ChecklistID, string SubAreaCode)
        {
            return _masterRepository.GetAllChecksByChecklistID(ChecklistID, SubAreaCode);
        }
        public List<Check> GetAllChecksByChecklistID(int ChecklistID)
        {
            return _masterRepository.GetAllChecksByChecklistID(ChecklistID);
        }

        public CountDto GetCountForPublishedAudits(string Fbo)
        {
            return _masterRepository.GetCountForPublishedAudits(Fbo);
        }

        public List<string> SaveAuditInfo(List<AuditInfoDto> auditInfo)
        {
            return _masterRepository.SaveAuditInfo(auditInfo);
        }

        public List<string> UpdatePublishedAudits(List<AuditInfoDto> auditInfo)
        {
            return _masterRepository.UpdatePublishedAudits(auditInfo);
        }
    }
}
