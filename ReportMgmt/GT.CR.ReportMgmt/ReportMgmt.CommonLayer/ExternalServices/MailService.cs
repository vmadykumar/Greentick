using Pratian.Components.Email.CommonContracts.Models;
using Pratian.Components.Email.Core.Facade;
using ReportMgmt.CommonLayer.Models.EmailModel;
using System;
using Microsoft.Extensions.Configuration;

namespace ReportMgmt.CommonLayer.ExternalServices
{
    public class MailService
    {
        IConfiguration configuration;
        public MailService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public bool SendEmail(SendEmailModel emailmodel)
        {
            try
            {
                string MailID = configuration["EMailID"];
                string Password = configuration["EMailPassword"];

                Mail email = new Mail();
                email.MailProtocolName = ProtocolName.SMTP;
                email.SMTPPortNum = int.Parse(configuration["EmailPortNumber"]);
                email.Host = configuration["EmailHost"];
                email.EnableSsl = true;
                email.Async = emailmodel.Async;
                email.EmailSender = new DomainUser(MailID, MailID, Password);
                foreach (var recipient in emailmodel.ToRecipients)
                    email.ToRecipients.Add(new DomainUser(recipient));
                email.Subject = emailmodel.Subject;
                email.Body = emailmodel.Body;
                email.Async = false;
                var GetStatus = EmailSendingFacade.SendMail(email);

            }
            catch (Exception exp)
            {

                throw exp;
            }


            return true;

        }

    }
}