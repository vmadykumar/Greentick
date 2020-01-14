
///-----------------------------------------------------------------
///   Namespace:   ReportMgmt.DataLayer.DataImplementationLayer
///   Class:         ReportRepository
///   Description:    Data Layer for Report data
///   Author:        Keshav M                   Date:21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------

using AutoMapper;
using Newtonsoft.Json;
using ReportMgmt.CommonLayer.DTOs;
using ReportMgmt.CommonLayer.Utility.IUtilityLayer;
using ReportMgmt.DataLayer.DataInterfaceLayer;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportMgmt.DataLayer.DataImplementationLayer
{
    public class ReportRepository : IReportRepository
    {
        private readonly IRedisManager _cacheProvider;
        private readonly IMapper Mapper = null;

        public ReportRepository(IRedisManager cacheProvider, IMapper mapper)
        {
            _cacheProvider = cacheProvider;
            Mapper = mapper;
        }
        /// <summary>
        /// Get All Report
        /// </summary>
        /// <returns>List Of Reports</returns>
        public List<AuditReportDto> GetAllReport()
        {
            IEnumerable<RedisKey> ab = _cacheProvider.GetAllKeys().ToList();
            return Mapper.Map<IEnumerable<RedisKey>, List<AuditReportDto>>(ab);
        }

        /// <summary>
        /// Get Audit Report for AuditCode
        /// </summary>
        /// <param name="auditID"></param>
        /// <returns></returns>
        public AuditReportDto GetAuditReport(string auditID)
        {
            try
            {
                return _cacheProvider.Get<AuditReportDto>(auditID);

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool SaveAuditReport(AuditReportDto auditReport)
        {
            try
            {
                _cacheProvider.Set(auditReport.AuditID, auditReport);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
