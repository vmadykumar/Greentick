using ComplyRite.Web.MVC.Areas.ReportMgmt.Models;
using ComplyRite.Web.MVC.ServiceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ComplyRite.Web.MVC.Facade.Report
{
    public class ReportMgmtFacade : IReportMgmtFacade
    {
        #region Global Variable Declaration
        private readonly IServiceUtility IServiceUtility = new ServiceUtility();
        #endregion

        /// Author : Sharath K M
        /// <summary>
        /// Method to get the audit report data from service
        /// </summary>      
        /// <param name="auditID">Unique id of the audit</param>
        /// <param name="url">Service url</param>
        /// <returns></returns>
        public async Task<AuditReport> GetAuditReportData(string auditID, string url)
        {
            return await IServiceUtility.GetDataFromService<AuditReport>(url, "ReportMgmtServiceURL");
        }
        public async Task<string> GetAuditReportAttachments(string filename, string url)
        {
            //url = "/api/Audit/Download?filename=" + filename;

            //return await IServiceUtility.GetDataFromService<AuditReport>(url, "AuditMgmtServiceURL");

            return ImageData(filename);
        }

        public string ImageData(string filename)
        {
            string url = "http://172.21.10.133:8253/api/Audit/Download?filename=" + filename;
            WebClient wc = new WebClient();
            byte[] data = wc.DownloadData(url);           
            var base64 = Convert.ToBase64String(data);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            return imgSrc;
        }
        public async Task<List<AccountLocationsDTO>> GetAllLocationsByUUID(string url)
        {
            return await IServiceUtility.GetDataFromService<List<AccountLocationsDTO>>(url, "ScheduleMgmtServiceURL");
        }

    }
}