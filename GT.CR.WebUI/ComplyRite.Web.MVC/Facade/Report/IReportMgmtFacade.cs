using ComplyRite.Web.MVC.Areas.ReportMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyRite.Web.MVC.Facade.Report
{
    public interface IReportMgmtFacade
    {
        Task<AuditReport> GetAuditReportData(string auditID, string url);
        Task<string> GetAuditReportAttachments(string filename, string url);

        Task<List<AccountLocationsDTO>> GetAllLocationsByUUID(string url);
    }
}
