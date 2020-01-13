//using ComplyRite.Web.MVC.Areas.ReportMgmt.Models;
//using Pratian.Components.Email.CommonContracts.Models;
//using Pratian.Components.Email.Core.Facade;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;

//namespace ComplyRite.Web.MVC.ServiceRepository.EmailService
//{
//    public class MailService
//    {
//        public bool SendEmail(SendEmailModel emailmodel)
//        {
//            try
//            {
//                Mail email = new Mail();
//                email.MailProtocolName = ProtocolName.SMTP;
//                email.SMTPPortNum = 587;
//                email.Host = "smtp.office365.com";
//                email.EnableSsl = true;
//                string UserName = ConfigurationManager.AppSettings["UserName"].ToString();
//                string MailID = ConfigurationManager.AppSettings["MailID"].ToString();
//                string PassCread = ConfigurationManager.AppSettings["PassCread"].ToString();
//                email.Async = emailmodel.Async;
//                email.EmailSender = new DomainUser(UserName, MailID, PassCread);
//                foreach (var recipient in emailmodel.ToRecipients)
//                    email.ToRecipients.Add(new DomainUser(recipient));
//                email.Subject = emailmodel.Subject;
//                email.Body = emailmodel.Body;
//                email.Async = false;
//                var GetStatus = EmailSendingFacade.SendMail(email);

//            }
//            catch (Exception exp)
//            {

//                throw exp;
//            }


//            return true;

//        }

//        public bool SendEmailWithAttachments(SendEmailModel emailmodel)
//        {
//            try
//            {
//                Mail email = new Mail();
//                email.MailProtocolName = ProtocolName.EXCHANGE;

//                if (emailmodel.Attachements != null)
//                    email.Attachments.Add(new EmailAttachment(emailmodel.AttachementName, emailmodel.Attachements));

//                string UserName = ConfigurationManager.AppSettings["UserName"].ToString();
//                string MailID = ConfigurationManager.AppSettings["MailID"].ToString();
//                string PassCread = ConfigurationManager.AppSettings["PassCread"].ToString();
//                email.Async = emailmodel.Async;

//                email.EmailSender = new DomainUser(UserName, MailID, PassCread);

//                foreach (var recipient in emailmodel.ToRecipients)
//                    email.ToRecipients.Add(new DomainUser(recipient));

//                email.Subject = emailmodel.Subject;
//                email.Body = emailmodel.Body;
//                email.Async = false;
//                var GetStatus = EmailSendingFacade.SendMail(email);

//            }
//            catch (Exception exp)
//            {

//                throw exp;
//            }


//            return true;

//        }

//        public List<string> GetRecipientList(string recipientsConfigKey)
//        {

//            return ConfigurationManager.AppSettings[recipientsConfigKey].Split(',').Select(p => p.Trim()).ToList();
//        }


//    }
//}