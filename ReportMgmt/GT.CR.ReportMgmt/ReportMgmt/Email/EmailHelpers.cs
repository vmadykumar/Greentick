using Microsoft.Extensions.Configuration;
using ReportMgmt.CommonLayer.DTOs;
using ReportMgmt.CommonLayer.ExternalServices;
using ReportMgmt.CommonLayer.Models.EmailModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportMgmt.Email
{
    public class EmailHelpers
    {
        readonly MailService mailService;
        readonly string AMBaseURL, DeployedReportWebURL;
        public EmailHelpers(IConfiguration _configuration, MailService _mailService)
        {
            AMBaseURL = _configuration["AMBaseURL"];
            mailService = _mailService;
            DeployedReportWebURL = _configuration["DeployedReportWebURL"];
        }
        /// <summary>
        /// Building the Email Body
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        private static string buildEmailBody(EmailReportDto report)
        {
            string body = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Resources/AuditSucessEmailBody.txt");
            body = body.Replace("@Model.EmailRecipientName", report.EmailRecipientName)
                .Replace("@Model.AuditID", report.AuditID)
               .Replace("@Model.PresentedBy", report.PresentedBy)
                .Replace("@Model.AuditorUUID", report.AuditorUUID)
                 .Replace("@Model.Location", report.Location)
                  .Replace("@Model.City", report.City)
                   .Replace("@auditScheduledDate", report.AuditScheduledDate)
                    .Replace("@auditDate", report.AuditDate)
                    .Replace("@Model.URL", report.URL);
            return body;
        }

        /// <summary>
        /// Getting Subject for email
        /// </summary>
        /// <param name="report"></param>
        /// <param name="userName"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        private string GetSubjctbySettingReportDefaults(ref EmailReportDto report)
        {
            report.URL = string.Concat(DeployedReportWebURL, Base64Encode(report.AuditID));
            if (report.AuditDate != null)
                report.AuditDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(report.AuditDate.ToString()), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dddd dd MMMM yyyy");
            if (report.AuditScheduledDate != null)
                report.AuditScheduledDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(report.AuditScheduledDate.ToString()), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("d MMMM yyyy H: mm");
          
            string subject = string.Concat(report.CompanyName, report.Location, " Audit Report");
            return subject;
        }

        /// <summary>
        /// Sending Email to single recipient
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="recipientMail"></param>
        private void SendMail(string body, string subject, string recipientMail)
        {
            SendMail(body, subject, new List<string>() { recipientMail });
        }

        /// <summary>
        /// Sending Email to multiple Recipients
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="recipientMails"></param>
        private void SendMail(string body, string subject, List<string> recipientMails)
        {
            mailService.SendEmail(new SendEmailModel()
            {
                Body = body,
                Async = false,
                Subject = subject,
                ToRecipients = recipientMails,
            });
        }

        /// <summary>
        /// Encoding plain text to Base 64 Encode
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            try
            {
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        /// <summary>
        /// Send Audit Report link through email
        /// </summary>
        /// <param name="report"></param>
        public void SendAuditLinkEmail(EmailReportDto report)
        {

            report.ManagerInfo.ForEach(
                mgr =>
                {
                    report.EmailRecipientName = mgr.Values.FirstOrDefault();
                    report.EmailRecipientId = mgr.Keys.FirstOrDefault();
                    SendMail(buildEmailBody(report),
           GetSubjctbySettingReportDefaults(ref report),report.EmailRecipientId);
                });
        }
    }
}
