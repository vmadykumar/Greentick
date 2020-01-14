///-----------------------------------------------------------------
///   Namespace:      AuditMgmt.Controllers
///   Class:         AuditController
///   Description:    Controller for Audit data
///   Author:        Keshav M                   Date: 21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuditMgmt.Controllers.AuditAggregator
{
    public partial class AuditController : Controller
    {
        #region GetAuditMethods


        /// <summary>
        /// Gets all audits with checklists,checks
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="Role"></param>
        /// <param name="LOBCode"></param>
        /// <param name="BMCode"></param>
        /// <param name="LocationCode"></param>
        /// <param name="TeamName"></param>
        /// <returns>List of Audits</returns>
        [HttpPost]
        public List<UserChecklistAuditInfoDto> GetAuditsByUserID(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName = "SAVE")
        {
            try
            {
                return _auditManager.GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode, TeamName); //returning audits based on userId, role and location

            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

        }
        [HttpPost]
        public List<UserChecklistAuditInfoDto> GetAuditBasicInfoForUserID(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName = "SAVE")
        {
            try
            {
                return _auditManager.GetAuditBasicInfoForUserID(userID, Role, LOBCode, BMCode, LocationCode, TeamName); //returning audits based on userId, role and location
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

        }
        [HttpGet]
        public List<UserChecklistAuditInfoDto> GetAuditsByAuditCode(List<string> AuditCode)
        {
            try
            {
                return _auditManager.GetAuditsByAuditCode(AuditCode);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

        }

        #endregion

        [HttpPost]
        public List<UserNotificationDto> GetAllNotificationforUser(string firebaseTopic)
        {
            return _auditManager.GetAllNotificationforUser(firebaseTopic);
        }
        [HttpGet]
        public void CheckOutBox(string firebaseTopic)
        {
            _auditManager.CheckOutBox(firebaseTopic);
        }

        [HttpPost]
        public void UpdateNotificationStatus(string NotificationID, string status)
        {
            _auditManager.UpdateNotificationStatus(NotificationID, status);
        }



        #region Last Performed details

        /// <summary>
        /// Get last performed checklist details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of Last Performed Checklist Details</returns>
        [HttpPost]
        public List<ChecklistLastPerformedDetailsDto> GetLastPerformCheckListDetails([FromBody]List<LastPerformedDto> model)
        {
            return _auditManager.GetLastPerformCheckListDetails(model); // Returns list of last performed details for the checklist
        }
        #endregion


        [HttpPost]
        public bool PublishAudits(List<string> AuditCodes)
        {
            return _auditManager.PublishAudits(AuditCodes);
        }


        [HttpPost]
        public List<AuditDto> GetAudits([FromBody]AuditInfoDTO auditDTO)
        {
            return _auditManager.GetAudits(auditDTO);
        }

        [HttpPost]
        public List<ChecklistDto> GetChecklistByAuditCode([FromBody]Data codeStatus)
        {
            return _auditManager.GetChecklistByAuditCode(codeStatus.CodeStatus);
        }

        [HttpPost]
        public List<AuditCheck> GetChecksByChecklistCode([FromBody]Data codeStatus)
        {
            return _auditManager.GetChecksByChecklistCode(codeStatus.CodeStatus);
        }
    }
    public class Data
    {
        public List<CodeStatusDto> CodeStatus { get; set; }
    }

}