///-----------------------------------------------------------------
///   Namespace:      ReportMgmt.Redis_config
///   Class:         RedisConfiguration
///   Description:    COnfiguration for Redis data
///   Author:        Keshav M                   Date: 09/4/2018
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using ReportMgmt.CommonLayer;
using ReportMgmt.CommonLayer.DTOs;
using ReportMgmt.CommonLayer.ExternalServices;
using ReportMgmt.CommonLayer.Utility.IUtilityLayer;
using ReportMgmt.Email;
using System;
using System.Collections.Generic;

namespace ReportMgmt.Redis_config
{
    public class RedisConfiguration
    {
        private readonly IConfiguration configuration;
        private readonly IRedisManager redisManager;
        private readonly string AMBaseURL;
        private readonly EmailHelpers _emailHelpers;
        public RedisConfiguration(IRedisManager _redisManager, IConfiguration _Configuration, EmailHelpers emailHelpers)
        {
            configuration = _Configuration;
            AMBaseURL = configuration["AMBaseURL"];
            redisManager = _redisManager;
            _emailHelpers = emailHelpers;
        }

        /// <summary>
        /// Process the message recived by RabbitMQ
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProcessMessage(string msg, IServiceProvider serviceProvider)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(msg)) // checking the msg has data in it
                {
                    AuditReportDto report = Utilities.MapReport(msg); // Mapping the data into auditreport DTO
                    report.ManagerInfo = GetManagerInfo(report); // getting manager info from account management
                    if (redisManager.IsExists(report.AuditID)) // check auditCode is present in database 
                    {
                        redisManager.Delete(report.AuditID); // delete audit data if present
                    }
                    redisManager.Set(report.AuditID, report); // set audit report data to database
                    _emailHelpers.SendAuditLinkEmail(ModelMapper(report));  // send audit link to email
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Get Manager Information from account management Service
        /// </summary>
        /// <param name="reportDto"></param>
        /// <returns></returns>
        private List<Dictionary<string, string>> GetManagerInfo(AuditReportDto reportDto)
        {
            try
            {
                return new ExternalServiceController().GetDataFromURL<List<Dictionary<string, string>>>(AMBaseURL + "api/UserManager/GetManagerInfoes?LobCode=" + reportDto.LOBCode + "&BMCode=" + reportDto.BMCode + "&LocationCode=" + reportDto.LocationCode + "&TeamName=" + reportDto.TeamName + "&auditorUUID=" + reportDto.AuditorUUID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Mapping audit data to email report dto
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        private static EmailReportDto ModelMapper(AuditReportDto report)
        {
            var emailreportDto = new EmailReportDto()
            {
                AuditID = report.AuditID,
                ManagerInfo = report.ManagerInfo,
                PresentedBy = report.PresentedBy,
                AuditorUUID = report.AuditorUUID,
                Location = report.Location,
                City = report.City,
                AuditScheduledDate = report.AuditScheduledDate.ToString(),
                AuditDate = report.AuditDate.ToString()
            };
            return emailreportDto;
        }










    }
}
