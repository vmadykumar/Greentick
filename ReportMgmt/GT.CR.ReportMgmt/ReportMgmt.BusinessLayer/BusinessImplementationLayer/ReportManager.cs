
///-----------------------------------------------------------------
///   Namespace:    ReportMgmt.BusinessLayer.BusinessImplementationLayer
///   Class:         ReportManager
///   Description:    Businees Layer for Report data
///   Author:        Keshav M                   Date:21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------


using ReportMgmt.BusinessLayer.BusinessInterfaceLayer;
using ReportMgmt.CommonLayer.DTOs;
using ReportMgmt.CommonLayer.Models.Entities;
using ReportMgmt.DataLayer.DataInterfaceLayer;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMgmt.BusinessLayer.BusinessImplementationLayer
{
    public class ReportManager : IReportManager
    {
        readonly IReportRepository _reportRepository = null;
        public ReportManager(IReportRepository reportRepository)
        {
            this._reportRepository = reportRepository;
        }

        /// <summary>
        /// Get All Report
        /// </summary>
        /// <returns>List Of Reports</returns>
        public List<AuditReportDto> GetAllReport()
        {
            return _reportRepository.GetAllReport();
        }

        /// <summary>
        /// Get Audit Report for AuditCode
        /// </summary>
        /// <param name="auditID"></param>
        /// <returns></returns>
        public AuditReportDto GetAuditReport(string auditID)
        {
         return _reportRepository.GetAuditReport(auditID);
        }
    }
}
