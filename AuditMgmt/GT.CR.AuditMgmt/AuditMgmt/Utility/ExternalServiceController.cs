using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuditMgmt.Utility
{

    [ApiController]
    class ExternalServiceController : ControllerBase
    {
        public T GetDataFromURL<T>(string URL, string msgBody = null, string MethodTYPE = "GET", string authorizationKey = null) //,string LOBCode, string BMCode, string UUID
        {
            T t;
           
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

            #region HTTPConfiguration
            if (!string.IsNullOrEmpty(authorizationKey))
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, authorizationKey);
            httpWebRequest.Method = MethodTYPE;

            httpWebRequest.ContentType = "application/json";
            if (!string.IsNullOrEmpty(msgBody))
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(msgBody);
                    streamWriter.Flush();
                }
            #endregion

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                t = JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
            }
            return t;
        }
    }
}