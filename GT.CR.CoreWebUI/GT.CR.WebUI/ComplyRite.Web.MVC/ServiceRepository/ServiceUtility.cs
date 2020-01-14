using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ComplyRite.Web.MVC.ServiceRepository
{
    public class ServiceUtility : IServiceUtility
    {
        public Task<TObject> GetDataFromService<TObject>(string URL, string serviceName, string ConFigPath = "~/PrivateSettings.config") where TObject : new()
        {
            throw new NotImplementedException();
        }

        public Task<TObject> PostDataToService<TObject>(string URL, string serviceName, object data, string ConFigPath = "~/PrivateSettings.config") where TObject : new()
        {
            throw new NotImplementedException();
        }
    }
}