using ReportMgmt.CommonLayer.DTOs;
using ReportMgmt.CommonLayer.Models.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMgmt.DataLayer.DataInterfaceLayer
{
    public interface IReportRepository
    {
        List<AuditReportDto> GetAllReport();
        AuditReportDto GetAuditReport(string auditID);
        bool SaveAuditReport(AuditReportDto auditReport);
    }
}
