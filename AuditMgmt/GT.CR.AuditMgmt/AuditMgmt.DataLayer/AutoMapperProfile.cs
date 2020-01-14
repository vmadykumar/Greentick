using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.DataLayer
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Audit, AuditDto>();
            CreateMap<AuditChecklist, ChecklistDto>();
            CreateMap<AuditCheck, CheckDto>();
            CreateMap<AuditChecklistHistory, ChecklistLastPerformedDetailsDto>();
        }
    }
}
