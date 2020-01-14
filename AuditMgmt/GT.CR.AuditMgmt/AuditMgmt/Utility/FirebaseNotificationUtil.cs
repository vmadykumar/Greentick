using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AuditMgmt.Utilities
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
            var result = new ExternalServiceController().GetDataFromURL<object>(firebaseURL, msgBody, "POST", authorizationKey);
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
        public List<string> NotificationBodyBuilder(List<string> topics, NotificationDto data, string title = null, string text = null)
        {
            data.title = title;
            data.body = text;
            List<string> messageBodies = new List<string>();
            foreach (var topic in topics)
            {
                var msgBody = new
                {
                    to = topic,
                    content_available = true,
                    data = data,
                    notification = new
                    {
                        title = title,
                        body = text,
                        text = text,
                        sound = "default"
                    },
                    mutable_content = true
                };
                messageBodies.Add(JsonConvert.SerializeObject(msgBody));
            }
            return messageBodies;
        }
    }
}