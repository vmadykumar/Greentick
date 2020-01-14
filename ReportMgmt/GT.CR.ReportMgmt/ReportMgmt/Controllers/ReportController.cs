
///-----------------------------------------------------------------
///   Namespace:     ReportMgmt.Controllers
///   Class:         ReportController
///   Description:    Controller for Report data
///   Author:        Keshav M                   Date: 21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReportMgmt.BusinessLayer.BusinessInterfaceLayer;
using ReportMgmt.CommonLayer.DTOs;
using ReportMgmt.CommonLayer.Models.Entities;
using StackExchange.Redis;

namespace ReportMgmt.Controllers
{
    //[Produces("application/json")]
    [Route("api/Report/[Action]")]
    public class ReportController : Controller
    {
        readonly IReportManager _reportManager = null;
        
        public ReportController(IReportManager reportManager)
        {
            _reportManager = reportManager;
           
        }

      
        /// <summary>
        /// Get All Report
        /// </summary>
        /// <returns>List Of Reports</returns>

        [HttpGet]
        public List<AuditReportDto> GetAllReport()
        {
            try
            {
                return _reportManager.GetAllReport();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get Audit Report for AuditCode
        /// </summary>
        /// <param name="auditID"></param>
        /// <returns></returns>
        [HttpGet]
        public AuditReportDto GetAuditReport(string auditID)
        {
            return _reportManager.GetAuditReport(auditID);
        }

             
    }
}