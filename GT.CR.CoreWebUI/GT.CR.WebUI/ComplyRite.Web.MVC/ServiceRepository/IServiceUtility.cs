using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyRite.Web.MVC.ServiceRepository
{
    public interface IServiceUtility
    {
        Task<TObject> GetDataFromService<TObject>(string URL, string serviceName, string ConFigPath = "~/PrivateSettings.config") where TObject : new();

        Task<TObject> PostDataToService<TObject>(string URL, string serviceName, object data, string ConFigPath = "~/PrivateSettings.config") where TObject : new();
    }
}
