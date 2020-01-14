
///-----------------------------------------------------------------
///   Namespace:     AuditMgmt.BusinessLayer.BusinessImplementationLayer
///   Class:         AuditManager
///   Description:    Businees Layer for Audit data
///   Author:        Keshav M                   Date:21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------

using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
using AuditMgmt.CommonLayer.CommonImplementationLayer;
using AuditMgmt.CommonLayer.ExternalServices;
using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using AuditMgmt.DataLayer.DataInterfaceLayer;
using AuditMgmt.Excepetions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AuditMgmt.CommonLayer.Utilities;

namespace AuditMgmt.BusinessLayer.BusinessImplementationLayer
{
    public class AuditManager : IAuditManager
    {
        readonly IAuditRepository _auditRepository = null;
        private readonly IRabbitMQManager _rabbitMQManager;
        readonly FirebaseNotificationUtil firebaseNotificationUtil = new FirebaseNotificationUtil();
        readonly IConfiguration configuration;
        readonly string authorizationKey, firebaseURL, AMBaseURL;
        public AuditManager(IAuditRepository auditRepository, IRabbitMQManager rabbitMQManager, IConfiguration _Configuration)
        {
            _auditRepository = auditRepository;
            _rabbitMQManager = rabbitMQManager;
            configuration = _Configuration;
            authorizationKey = configuration["authorizationKey"];
            firebaseURL = configuration["firebaseURL"];
            AMBaseURL = configuration["AMBaseURL"];
        }

        /// <summary>
        /// Gets all audits for a user. For a manager gets all audits for auditors under them.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="uuids"></param>
        /// <param name="Role"></param>
        /// <returns>List of audits</returns>
        public List<UserChecklistAuditInfoDto> GetAuditsByUserID(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName = "SAVE")
        {
            try
            {
                var audits = _auditRepository.GetAuditsByUserID(userID, GetUUIDSinTeambyUserRole(userID, Role, LOBCode, BMCode, TeamName, LocationCode), Role, LocationCode);
                foreach (var ua in audits)
                {
                    ua.AuditChecklists.Clear();
                    Role = string.IsNullOrEmpty(Role) ? Role : Role.ToUpper();
                    switch (Role)
                    {
                        case "MANAGER":
                            ua.Audit.RemoveAll(a => a.AuditScheduledStartDateTime <= DateTime.Now.ToUniversalTime().AddDays(-60) || a.AuditScheduledStartDateTime >= DateTime.Now.ToUniversalTime().AddDays(60));
                            break;
                        default:
                            ua.Audit.RemoveAll(a => a.AuditScheduledStartDateTime <= DateTime.Now.ToUniversalTime().AddDays(-5) || a.AuditScheduledStartDateTime >= DateTime.Now.ToUniversalTime().AddDays(5));
                            break;
                    }
                   
                    foreach (var a in ua.Audit)
                    {
                        a.AuditFBO = ua.FBO;
                        a.AuditLocation = ua.LocationName;
                        a.AuditCity = ua.City;
                        a.FBOCode = ua.FBOCode;
                        a.LocationCode = ua.LocationCode;
                    }
                }
                return audits.Where(a => a.Audit.Count != 0).ToList();
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        public List<UserChecklistAuditInfoDto> GetAuditBasicInfoForUserID(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName = "SAVE")
        {
            return _auditRepository.GetAuditBasicInfoForUserID(userID, GetUUIDSinTeambyUserRole(userID, Role, LOBCode, BMCode, TeamName, LocationCode), Role, LocationCode);
        }

        public List<UserChecklistAuditInfoDto> GetAuditsByAuditCode(List<string> AuditCode)
        {
            try
            {
                return _auditRepository.GetAuditsByAuditCode(AuditCode);
            }
            catch (Exception e)
            {

                throw new BusinessLayerException("Business Layer Exception", e.InnerException);
            }
        }



        /// <summary>
        /// Saves the list of audit details
        /// </summary>
        /// <param name="audits"></param>
        /// <returns>True on success</returns>
        public bool SaveAudit(List<UserChecklistAuditInfo> userAudits)
        {
            try
            {
               
                //Removing Closed Audits and Checklists
                userAudits?.ForEach(ua => ua.Audit?.RemoveAll(a => _auditRepository.IsAuditStatusMatched(a.AuditCode, new List<string>() { "CLOSED" })));
                userAudits?.ForEach(ua => ua.AuditChecklists?.RemoveAll(a => _auditRepository.IsCheckListStatusMatched(a.ChecklistCode, new List<string>() { "CLOSED" })));


                if (userAudits?.Count != 0 && userAudits.Any(ua => (ua.Audit != null && ua.Audit?.Count != 0) || (ua.AuditChecklists != null && ua.AuditChecklists.Count != 0)) && _auditRepository.SaveAudit(userAudits))
                {
                    SendNotificationForClosedAudits(userAudits);// sending Notifications to the higher role if the status of audit is closed
                    Task.Run(() => userAudits.Where(u => u.Audit.Any(a => a.AuditStatus.ToUpper() == "CLOSED")).ToList().ForEach(audit => _rabbitMQManager.Publish(JsonConvert.SerializeObject(audit, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))));
                }
                return true;
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        /// <summary>
        /// Get last performed checklist details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of Last Performed Checklist Details</returns>
        public List<ChecklistLastPerformedDetailsDto> GetLastPerformCheckListDetails(List<LastPerformedDto> model)
        {
            try
            {
                return _auditRepository.GetLastPerformCheckListDetails(model);
            }
            catch (Exception e)
            {

                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }


        #region External Services
        /// <summary>
        /// Gets list of users in a team based on the user role
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="role"></param>
        /// <param name="LOBCode"></param>
        /// <param name="BMCode"></param>
        /// <param name="TeamName"></param>
        /// <param name="LocationCode"></param>
        /// <param name="PrependString"></param>
        /// <returns>List of UUIDs</returns>
        private List<string> GetUUIDSinTeambyUserRole(string userID, string role, string LOBCode, string BMCode, string TeamName, string LocationCode, string PrependString = null)
        {
            List<string> uuids = new List<string>() { userID };
            role = role?.ToUpper(); //changing role data to upper Case
            switch (role) //checking Role for getting UUIDs 
            {
                case "MANAGER": //checking if role is Manager
                    uuids.AddRange(new ExternalServiceUtility().GetDataFromURL<List<string>>(AMBaseURL + "api/TeamManager/GetUserTeamLowerHierarchy?LOBCode=" + LOBCode + "&BMCode=" + BMCode + "&LocationCode=" + LocationCode + "&TeamName=" + TeamName + "&UUID=" + userID).Select(i => string.Concat(PrependString, i).ToUpper()).ToList()); //adding the UUIDs of the auditors under that manager
                    break;
            }
            return uuids.Distinct().ToList(); //returning list of UUIDs for that role
        }

        /// <summary>
        /// Gets the list of users for the notification
        /// </summary>
        /// <param name="TeamName"></param>
        /// <param name="LOBCode"></param>
        /// <param name="BMCode"></param>
        /// <param name="LocationCode"></param>
        /// <param name="UUID"></param>
        /// <returns>List of UUIDs</returns>
        List<string> GetUUIDsForNotificaton(string TeamName, string LOBCode, string BMCode, string LocationCode, string UUID, string prependText = null)
        {
            string Url = configuration["AMBaseURL"].ToString();
            return new ExternalServiceUtility().GetDataFromURL<List<string>>
                (Url + "api/TeamManager/GetUserTeamHigherHierarchy?LOBCode=" + LOBCode + "&BMCode=" + BMCode + "&LocationCode=" + LocationCode + "&TeamName=" + TeamName + "&UUID=" + UUID) // Getting team higher hierarchy based on location and UUID
                .Select(i => string.Concat(prependText, i)).Distinct().ToList(); // Selects the UUIDs to send the notification to
        }


        /// <summary>
        /// Send Notification For Closed Audits
        /// </summary>
        /// <param name="audits"></param>
        private void SendNotificationForClosedAudits(List<UserChecklistAuditInfo> userAudits)
        {
            foreach (var userAudit in userAudits)
            {
                var auditlocation = userAudit.LocationName;
                foreach (var audit in userAudit.Audit.Where(a => a.AuditStatus.ToUpper().Equals("CLOSED"))) //checking if the audit is closed
                {
                    var msgBodies = firebaseNotificationUtil.NotificationBodyBuilder( // Building Notification Body
                            GetUUIDsForNotificaton("SAVE", audit.LOBCode, audit.BMCode, audit.LocationCode, userAudit.UserID, "/topics/SAVE"), // getting all UUIDS For Notification
                                 new NotificationDto()// inserting data to body
                                 {
                                     AuditCode = audit.AuditCode,
                                     AuditID = audit.AuditID,
                                     AuditCity = userAudit.City,
                                     AuditName = audit.AuditName, 
                                     PerformedBy = audit.AuditedBy,
                                     AuditImage = audit.AuditImage,
                                     AuditedByRole = audit.AuditedByRole,
                                     AuditCompliancePercentage= audit.AuditCompliancePercentage,
                                     AuditStatus= audit.AuditStatus,
                                     AuditEndDateTime = audit.AuditEndDateTime,
                                     AuditScheduledStartDateTime = audit.AuditScheduledStartDateTime,
                                     AuditScheduledEndDateTime= audit.AuditScheduledEndDateTime,
                                     TotalNoOfChecks = audit.TotalNoOfChecks,
                                     TotalYesChecks = audit.TotalYesChecks,
                                     TotalNoChecks= audit.TotalNoChecks,
                                     TotalNumberOfChecklist= audit.TotalNumberOfChecklist,
                                     AuditLocation=auditlocation

                                 },
                           title: string.Concat(userAudit.FBO, " ", userAudit.LocationName, " audit report"),// title for notification
                           text: string.Concat(audit.AuditedBy, " has completed an audit on ", audit.AuditEndDateTime.GetLocalDatefromUTC(), " scheduled for the date ", audit.AuditScheduledStartDateTime.GetLocalShortDatefromUTC(), ". Please refresh the home screen to view the updated report"));
                    logMessagesToDb(msgBodies);

                    Task.Run(() => firebaseNotificationUtil.SendBulkNotificationtoFirebase(// sending Bulk Notifications
                        msgBodies.Select(m => m.body).ToList(),
                      authorizationKey,
                        firebaseURL)); // adding body to notification
                }
            }


        }

        private void logMessagesToDb(List<NotificationBody> msgBodies)
        {
            _auditRepository.logMessagesToDb(msgBodies);
        }

        public bool SaveChecklist(List<AuditChecklist> checklist)
        {
            return _auditRepository.SaveChecklist(checklist);
        }
        public bool SaveCheck(List<AuditCheck> check)
        {
            return _auditRepository.SaveCheck(check);
        }

        public void CheckOutBox(string firebaseTopic)
        {
            firebaseNotificationUtil.SendBulkNotificationtoFirebase(// sending Bulk Notifications by getting body from log table
       _auditRepository.CheckOutBox(firebaseTopic).Select(m => m.NotificationBody).ToList(),
      authorizationKey,
        firebaseURL);
        }

        public void UpdateNotificationStatus(string NotificationID, string status)
        {
            _auditRepository.UpdateNotificationStatus(NotificationID, status);
        }

        public List<UserNotificationDto> GetAllNotificationforUser(string firebaseTopic)
        {
            return _auditRepository.GetAllNotificationforUser(firebaseTopic);
        }

        public bool DeleteAttachement(string filename)
        {
            return _auditRepository.DeleteAttachement(filename);
        }

        public bool PublishAudits(List<string> AuditCodes)
        {

            return _auditRepository.PublishAudits(AuditCodes);
        }

      

        public List<AuditDto> GetAudits(AuditInfoDTO auditDTO)
        {
            try
            {
                auditDTO.UUIDs = GetUUIDSinTeambyUserRole(auditDTO.userID, auditDTO.Role, auditDTO.LOBCode, auditDTO.BMCode, auditDTO.TeamName, auditDTO.LocationCode);
                return _auditRepository.GetAudits(auditDTO);
            }
            catch (Exception e)
            {
                throw new BusinessLayerException("Business Layer Exception", e);
            }
        }

        public List<ChecklistDto> GetChecklistByAuditCode(List<CodeStatusDto> codeStatus)
        {
            return _auditRepository.GetChecklistByAuditCode(codeStatus);
        }

        public List<AuditCheck> GetChecksByChecklistCode(List<CodeStatusDto> codeStatus)
        {
            return _auditRepository.GetChecksByChecklistCode(codeStatus);
        }

        
        public List<ReportDto> GetAuditSummaryReport(string userID, string LOBCode, string BMCode, string TeamName)
        {
            var uuids = new ExternalServiceUtility().
                GetDataFromURL<List<string>>(AMBaseURL + "api/TeamManager/GetUserTeamLowerHierarchy?LOBCode=" + LOBCode + "&BMCode=" + BMCode + "&TeamName=" + TeamName + "&UUID=" + userID).ToList();
            return _auditRepository.GetAuditSummaryReport(uuids);
        }



        #endregion

        #region Notifications
        ///// <summary>
        ///// Notifies about the acknowledgement details
        ///// </summary>
        ///// <param name="auditorUUID"></param>
        ///// <param name="AuditID"></param>
        ///// <param name="ViewerUsername"></param>
        //public void NotifyAcknowledgement(string auditorUUID, string AuditID, string ViewerUsername)
        //{
        //    firebaseNotificationUtil.SendNotificationtoFirebase(firebaseNotificationUtil.NotificationBodyBuilder(new List<string>() { "/topics/SAVE" + auditorUUID }, new NotificationDto() { AuditID = AuditID }, "Your Audit has been Viewed by " + ViewerUsername).FirstOrDefault(), authorizationKey, firebaseURL); //Sending notification when an audit has been viewed
        //}
        #endregion




    }
}
