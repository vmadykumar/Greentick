using AutoMapper;
using ReportMgmt.CommonLayer.DTOs;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMgmt.DataLayer.DataImplementationLayer
{
   public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<List<RedisKey>, List<AuditReportDto>>();
        }
    }
}
