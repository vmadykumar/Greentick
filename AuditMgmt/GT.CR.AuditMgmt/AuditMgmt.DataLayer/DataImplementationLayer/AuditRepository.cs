
///-----------------------------------------------------------------
///   Namespace:    AuditMgmt.DataLayer.DataImplementationLayer
///   Class:         AuditRepository
///   Description:    Data Layer for Audit data
///   Author:        Keshav M                   Date: 21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------

using AuditMgmt.CommonLayer.Models.Entities;
using AuditMgmt.DataLayer.DataInterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using AuditMgmt.CommonLayer.Models.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AuditMgmt.Excepetions;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dapper;
using System.Text.RegularExpressions;

namespace AuditMgmt.DataLayer.DataImplementationLayer
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditContext _auditContext = null;
        private readonly IMapper Mapper = null;
        readonly IConfiguration configuration;
        SqlConnection connection;

        public AuditRepository(AuditContext auditContext, IMapper mapper, IConfiguration _Configuration)
        {
            _auditContext = auditContext;
            Mapper = mapper;
            configuration = _Configuration;
        }


        /// <summary>
        /// Gets all audits for a user. For a manager gets all audits for auditors under them.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="uuids"></param>
        /// <param name="Role"></param>
        /// <returns>List of audits</returns>
        public List<UserChecklistAuditInfoDto> GetAuditsByUserID(string userID, List<string> UUIDs, string Role, string LocationCode)
        {
            return Mapper.Map<List<UserChecklistAuditInfo>, List<UserChecklistAuditInfoDto>>(_auditContext.UserChecklistAuditInfos.Where(x => UUIDs.Contains(x.UserID))// getting audit based on user and status closed
             .Include("Audit").Include("Audit.AuditChecklist").Include("Audit.AuditChecklist.AuditCheck").Include("Audit.AuditChecklist.AuditCheck.Attachement").Where(a => a.Audit.Count != 0).ToList());// returning audits
        }
        #region Last Performed Details
        /// <summary>
        /// Get last performed checklist details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of Last Performed Checklist Details</returns>
        public List<ChecklistLastPerformedDetailsDto> GetLastPerformCheckListDetails(List<LastPerformedDto> model)
        {
            try
            {
                var parameter = new SqlParameter("@DT", SqlDbType.Structured); // defining the parameter and the type 
                parameter.Value = ToDataTable(model); // passing value to the data model
                parameter.TypeName = " [dbo].[LastPerformed]";// passing sored procedure name
                return _auditContext.ChecklistLastPerformedDetailsDTO.FromSql("Sp_LastPerformedDetails @DT", parameter).ToList(); // returning last performed details
            }
            catch (Exception e)
            {
                throw new DataLayerException("Error while Getting last performed checklist details", e);
            }
        }
        #endregion 

        #region Data Conversion for Data Table
        /// <summary>
        /// List Conversion to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        private DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            if (items != null)
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private DataTable ToDataTable(List<string> items)
        {
            DataTable dataTable = new DataTable("List");

            dataTable.Columns.Add("Value", typeof(string));

            foreach (string item in items)
            {
                dataTable.Rows.Add(item);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        #endregion



        public void logMessagesToDb(List<NotificationBody> msgBodies)
        {
            msgBodies.ForEach(msg =>
        {
            _auditContext.NotificationLog.Add(new NotificationLog()
            {
                FirebaseTopic = msg.Topic,
                NotificationBody = msg.body,
                NotificationDateTime = DateTime.UtcNow,
                NotificationID = msg.NotificationId,
                NotificationStatus = "INITIATED"
            });
            _auditContext.SaveChanges();
        });
        }

        public bool SaveChecklist(List<AuditChecklist> checklist)
        {
            checklist?.ForEach(acl => // looping inside each checklist
            {
                SaveAuditChecklist(acl);
            });
            _auditContext.SaveChanges();
            return true;
        }

        public bool SaveCheck(List<AuditCheck> check)
        {
            check?.ForEach(ach => // looping inside each check
            {
                SaveAuditCheck(ach);
            });
            _auditContext.SaveChanges();
            return true;
        }

        public List<NotificationLog> CheckOutBox(string firebaseTopic)
        {
            var outboxstatus = configuration.GetValue<string>("OutboxStatuses").ToUpper().Split(",");
            return _auditContext.NotificationLog.Where(n => n.FirebaseTopic.Equals(firebaseTopic) && outboxstatus.Any(o => o.Equals(n.NotificationStatus.ToUpper()))).ToList();
        }

        public void UpdateNotificationStatus(string NotificationID, string status)
        {
            var notification = new NotificationLog() { NotificationID = NotificationID, NotificationStatus = status };
            _auditContext.NotificationLog.Attach(notification);
            _auditContext.Entry(notification).Property(x => x.NotificationStatus).IsModified = true;
            _auditContext.SaveChanges();
        }

        public List<UserNotificationDto> GetAllNotificationforUser(string firebaseTopic)
        {
            List<UserNotificationDto> notification = new List<UserNotificationDto>();
            var Notifications = _auditContext.NotificationLog.Where(n => n.FirebaseTopic == firebaseTopic)
        .Select(n => (JObject)JsonConvert.DeserializeObject(n.NotificationBody)).ToList();
            foreach (var item in Notifications)
            {
                notification.Add(new UserNotificationDto()
                {
                    title = (string)item["data"]["title"],
                    body = (string)item["data"]["body"]

                }
                );
            }
            return notification;

        }
        public bool DeleteAttachement(string filename)
        {
            try
            {
                _auditContext.Attachement.RemoveRange(_auditContext.Attachement.Where(a => a.AttachementName == filename));
                _auditContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<string> GetAllAttachementNames()
        {
            return _auditContext.Attachement.Select(a => a.AttachementName).Distinct().ToList();
        }

        public bool PublishAudits(List<string> AuditCodes)
        {
            try
            {
                connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
                AuditCodes.ForEach(AuditCode =>
                connection.Query("Exec Sp_PublishAudit " + AuditCode));
            }
            catch (Exception e)
            {

                throw new DataLayerException("Error while publishing audits", e);
            }

            return true;
        }

        public List<UserChecklistAuditInfoDto> GetAuditsByAuditCode(List<string> AuditCode)
        {
            var userAudits = _auditContext.UserChecklistAuditInfos
                   .Include("Audit").Include("Audit.AuditChecklist").Include("Audit.AuditChecklist.AuditCheck").Include("Audit.AuditChecklist.AuditCheck.Attachement").Where(au => au.Audit.Count != 0 || au.AuditChecklists.Count != 0).ToList();
            foreach (var item in userAudits)
            {
                item.Audit.RemoveAll(b => !AuditCode.Contains(b.AuditCode));
                item.AuditChecklists?.Clear();
            }
            return Mapper.Map<List<UserChecklistAuditInfo>, List<UserChecklistAuditInfoDto>>(userAudits.Where(a => a.Audit.Count != 0).ToList());// returning audits
        }

        public List<UserChecklistAuditInfoDto> GetAuditBasicInfoForUserID(string userID, List<string> UUIDs, string Role, string LocationCode)
        {
            return Mapper.Map<List<UserChecklistAuditInfo>, List<UserChecklistAuditInfoDto>>
           (_auditContext.UserChecklistAuditInfos.Where(x => UUIDs.Contains(x.UserID) && x.LocationCode == LocationCode).Include("Audit").Where(a => a.Audit.Count != 0).ToList());// returning audits

        }

        public List<ReportDto> GetAuditSummaryReport(List<string> UserID)
        {
            var uuids = new SqlParameter("@UUID", SqlDbType.Structured);
            uuids.Value = ToDataTable(UserID);
            uuids.TypeName = " [dbo].[List]";
            return _auditContext.ReportDto.FromSql("Sp_GetAuditSummaryCounts {0}", uuids).ToList();
        }

        public string CleanString(String stringValue)
        {
           
            if(stringValue!=null)
            {
                if(stringValue.IndexOf(' ') >= 0) {
                    stringValue = stringValue.Substring(0, stringValue.IndexOf(' '));
                }
                stringValue = Regex.Replace(stringValue, @"[^0-9a-zA-Z]+", "");
            }
            return stringValue;
        }

        public List<AuditDto> GetAudits(AuditInfoDTO auditDTO)
        {
            auditDTO.BMCode = CleanString(auditDTO.BMCode);
            foreach(CodeStatusDto codeStatusDto in auditDTO.CodeStatus)
            {
                codeStatusDto.Code = CleanString(codeStatusDto.Code);
                codeStatusDto.ParentCode = CleanString(codeStatusDto.ParentCode);
                codeStatusDto.CodeStatus = CleanString(codeStatusDto.CodeStatus);
            }
            
            var uuids = new SqlParameter("@UUID", SqlDbType.Structured); // defining the parameter and the type 
            uuids.Value = ToDataTable(auditDTO.UUIDs); // passing value to the data model
            uuids.TypeName = " [dbo].[List]";// passing sored procedure name
            var auditCodes = new SqlParameter("@AuditCode", SqlDbType.Structured); // defining the parameter and the type 
            auditCodes.Value = ToDataTable(auditDTO.CodeStatus); // passing value to the data model
            auditCodes.TypeName = " [dbo].[LastModified]";// passing sored procedure name 
            var a = _auditContext.AuditDto.FromSql($"EXECUTE dbo.Sp_GetAuditInfo {uuids} ,{auditDTO.Role},{auditDTO.LOBCode},{auditCodes} ,{auditDTO.StartDate},{auditDTO.EndDate}").ToList();
            return a;
        }


        public List<ChecklistDto> GetChecklistByAuditCode(List<CodeStatusDto> codeStatus)
        {
            foreach (CodeStatusDto codeStatus1 in codeStatus)
            {
                codeStatus1.Code = CleanString(codeStatus1.Code);
                codeStatus1.ParentCode = CleanString(codeStatus1.ParentCode);
                codeStatus1.CodeStatus = CleanString(codeStatus1.CodeStatus);
            }

            var auditCodes = new SqlParameter("@AuditCode", SqlDbType.Structured); // defining the parameter and the type 
            auditCodes.Value = ToDataTable(codeStatus); // passing value to the data model
            auditCodes.TypeName = "[dbo].[LastModified]";// passing sored procedure name
            return _auditContext.ChecklistDto.FromSql($"EXECUTE dbo.Sp_GetChecklistInfo  {auditCodes}").ToList();
        }


        public List<AuditCheck> GetChecksByChecklistCode(List<CodeStatusDto> codeStatus)
        {
            foreach (CodeStatusDto codeStatus1 in codeStatus)
           {              
               
                codeStatus1.CodeStatus = CleanString(codeStatus1.CodeStatus);
            }


            List<int> checkLists = _auditContext.AuditChecklist.Where(b => codeStatus.Any(c => c.ParentCode == b.ChecklistCode)).Select(d => d.AuditChecklistID).ToList();
            var checks = _auditContext.AuditCheck.Include("Attachement")
                 .Where(a => checkLists.Any(b => b == a.AuditChecklistID)).ToList();
            foreach (var check in codeStatus)
                checks.RemoveAll(a => a.LastModifiedDateTime == check.LastModifiedDateTime && a.CheckCode == check.Code && a.CheckStatus == check.CodeStatus && "OPEN" != check.CodeStatus.ToUpper());

            return checks;

        }


        #region Save Audit

        /// <summary>
        /// Is Audit Status Matched
        /// </summary>
        /// <param name="audit"></param>
        /// <param name="status"></param>
        /// <returns>returns true of false</returns>
        public bool IsAuditStatusMatched(string auditCode, List<string> statuses)
        {
            return _auditContext.Audit.Any(a => a.AuditCode.ToUpper().Equals(auditCode) && statuses.Contains(a.AuditStatus.ToUpper()));
        }

        public bool IsCheckListStatusMatched(string checkListCode, List<string> statuses)
        {
            return _auditContext.AuditChecklist.Any(a => a.ChecklistCode.ToUpper().Equals(checkListCode) && statuses.Contains(a.ChecklistStatus.ToUpper()));
        }

        /// <summary>
        /// Saves the list of audit details
        /// </summary>
        /// <param name="audits"></param>
        /// <returns>True on success</returns>
        public bool SaveAudit(List<UserChecklistAuditInfo> userinfo)
        {
            try
            {
                userinfo?.ForEach(u =>
                {
                    int userInfoId = getUserInfoId(u);

                    u.Audit?.ForEach(a =>  //looping inside each audit
                    {
                        a.UserChecklistAuditInfoID = userInfoId;
                        a.SyncedDateTime = DateTime.UtcNow;// adding latest sync date time for the audit
                        CheckingAuditHasAttachements(a); // checking audit has attachements
                        CheckingAuditHasComments(a); // checking audit has comments
                        _auditContext.Entry(a).State = a.AuditID == 0 ? EntityState.Added : EntityState.Modified; // checking the state of each audit

                        a.AuditChecklist?.ForEach(acl => // looping inside each checklist
                        {
                            acl.SyncedDateTime = DateTime.UtcNow;
                            SaveAuditChecklist(acl); // checking audit has checklist
                        });
                    });
                    u.AuditChecklists?.ForEach(acl => // looping inside each checklist
                    {
                        u.UserChecklistAuditInfoID = userInfoId;
                        SaveAuditChecklist(acl); // checking audit has checklist
                    });
                });

                _auditContext.SaveChanges();// saving all changes
                return true;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Error while Saving the list of audit details", e);
            }
        }

        private int getUserInfoId(UserChecklistAuditInfo u)
        {
            var _userInfo = _auditContext.UserChecklistAuditInfos.FirstOrDefault(k => k.UserID == u.UserID && k.FBOCode == u.FBOCode && k.LocationCode == u.LocationCode && k.City == u.City);
            if (_userInfo == null)
            {
                _auditContext.Entry(u).State = u.UserChecklistAuditInfoID == 0 ? EntityState.Added : EntityState.Modified;
                _auditContext.SaveChanges();
                _userInfo = _auditContext.UserChecklistAuditInfos.FirstOrDefault(k => k.UserID == u.UserID && k.FBOCode == u.FBOCode && k.LocationCode == u.LocationCode && k.City == u.City);
            }
            return _userInfo.UserChecklistAuditInfoID;
        }

        /// <summary>
        /// Checking Audit Has Comments
        /// </summary>
        /// <param name="a"></param>
        private void CheckingAuditHasComments(Audit a)
        {
            try
            {
                a.Comments?.ForEach(comment => // looping inside each comment
                {
                    _auditContext.Entry(comment).State = a.AuditID == 0 ? EntityState.Added : EntityState.Modified; // checking the state of each audit comment
                });
            }
            catch (Exception e)
            {

                throw new DataLayerException("Error while Checking Audit Has Comments", e);
            }

        }

        /// <summary>
        /// Checking Audit Has Attachements
        /// </summary>
        /// <param name="a"></param>
        private void CheckingAuditHasAttachements(Audit a)
        {
            try
            {
                a.Attachements?.ForEach(attachement => // looping inside each attachement
                {
                    _auditContext.Entry(attachement).State = a.AuditID == 0 ? EntityState.Added : EntityState.Modified;// checking the state of each audit attachement
                });
            }
            catch (Exception e)
            {

                throw new DataLayerException("Error while Checking Audit Has Attachements", e);
            }

        }

        /// <summary>
        /// Checking Audit Has Checklist
        /// </summary>
        /// <param name="a"></param>
        private void SaveAuditChecklist(AuditChecklist acl)
        {
            try
            {
                _auditContext.Entry(acl).State = acl.AuditChecklistID == 0 ? EntityState.Added : EntityState.Modified;// checking the state of each auditchecklist
                acl.AuditCheck?.ForEach(ac => // looping inside each check
                {
                    //ac.AuditChecklistID = acl.AuditChecklistID; // asigning aaudit checklist ID
                    SaveAuditCheck(ac);   // check audit has checks
                });
            }
            catch (Exception e)
            {

                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Checking Audit Has Checks
        /// </summary>
        /// <param name="acl"></param>
        private void SaveAuditCheck(AuditCheck ac)
        {
            try
            {

                CheckingEachCheckHasAttachement(ac); // checking check has attachements 
                _auditContext.Entry(ac).State = ac.AuditCheckID == 0 ? EntityState.Added : EntityState.Modified;// checking the state of each check 
            }
            catch (Exception e)
            {
                throw new DataLayerException("Error while Checking Audit Has Checks", e);
            }

        }

        /// <summary>
        /// Checking if a check has an attachment
        /// </summary>
        /// <param name="ac"></param>
        private void CheckingEachCheckHasAttachement(AuditCheck ac)
        {
            try
            {
                ac.Attachement?.ForEach(a =>// looping inside each check attachement
                {
                    _auditContext.Attachement.RemoveRange(_auditContext.Attachement.Where(at => a.prevAttachment.Contains(at.AttachementName)));// checking database for previous image
                    if (_auditContext.Attachement.FirstOrDefault(c => c.AttachementName == a.AttachementName || a.AttachementName == "" || a.AttachementName == null) == null)
                    {
                        a.AttachementTypeID = 1; // since for now there is only one type
                        a.AttachementSize = 0;  // since we are not using this data
                        a.AuditCheckID = ac.AuditCheckID; // assigining checkid to attachemt CheckID
                        _auditContext.Entry(a).State = a.AttachementID == 0 ? EntityState.Added : EntityState.Modified;// checking the state of each check attachement
                    }
                });

            }
            catch (Exception e)
            {
                throw new DataLayerException("Error while Checking if a check has an attachment", e);
            }

        }
        #endregion
    }
}

