using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Controllers
{
    public class AuditReportApiController : ApiController
    {
        [HttpPost]
 
        public async Task SendEmailForReport(string auditId)
        {
            ReportController reportController = new ReportController();
           
            await reportController.ReportData(auditId);
        }
    }
    
}
