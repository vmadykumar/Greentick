using AuditMgmt.CommonLayer.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AuditMgmt.CommonLayer.ExternalServices
{
    public class FirebaseNotificationUtil
    {


        /// <summary>
        /// Sends notification to the firebase
        /// </summary>
        /// <param name="msgBody"></param>
        /// <param name="authorizationKey"></param>
        /// <param name="firebaseURL"></param>
        public void SendNotificationtoFirebase(string msgBody, string authorizationKey, string firebaseURL)
        {
            new ExternalServiceUtility().GetDataFromURL<object>(firebaseURL, msgBody, "POST", authorizationKey); 
        }

        /// <summary>
        /// Sends bulk notifications to the firebase
        /// </summary>
        /// <param name="messageBodies"></param>
        /// <param name="authorizationKey"></param>
        /// <param name="firebaseURL"></param>
        public void SendBulkNotificationtoFirebase(List<string> messageBodies, string authorizationKey, string firebaseURL)
        {
            messageBodies.ForEach(msgBody => SendNotificationtoFirebase(msgBody, authorizationKey, firebaseURL));
        }

        /// <summary>
        /// Builds the notification body
        /// </summary>
        /// <param name="topics"></param>
        /// <param name="title"></param>
        /// <param name="auditId"></param>
        /// <param name="UUID"></param>
        /// <param name="text"></param>
        /// <returns>List of message bodies</returns>
        public List<NotificationBody> NotificationBodyBuilder(List<string> topics, NotificationDto data, string title = null, string text = null)
        {
            data.title = title;
            data.body = text;
            List<NotificationBody> messageBodies = new List<NotificationBody>();
            foreach (var topic in topics)
            {
                var notificationId = Guid.NewGuid().ToString();
                var msgBody = new
                {
                    to = topic,
                    content_available = true,
                    data = data,
                    notification = new
                    {
                        NotificationId= notificationId,
                        title = title,
                        body = text,
                        text = text,
                        click_action = "AuditReportModuleActivity", 
                        sound = "default"
                    },
                    mutable_content = true
                };
                messageBodies.Add(
                    new NotificationBody()
                    {
                        NotificationId=notificationId,
                        Topic = topic,
                        body = JsonConvert.SerializeObject(msgBody)
                    });
            }
            return messageBodies;
        }
    }
}