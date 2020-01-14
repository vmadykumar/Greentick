using ReportMgmt.CommonLayer.DTOs;
using ReportMgmt.CommonLayer.Models.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMgmt.BusinessLayer.BusinessInterfaceLayer
{
    public interface IReportManager
    {
        List<AuditReportDto> GetAllReport();
        AuditReportDto GetAuditReport(string auditID);
    }
}
