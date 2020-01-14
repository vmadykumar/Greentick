using AutoMapper;
using MasterMgmt.CommonLayer.Models.DTO;
using MasterMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMgmt.DataLayer
{
   public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Checklist, ChecklistDTO>();//.ForMember(d => d.Checks, opt => opt.MapFrom(a => a.ChecklistChecks.ForEach(h=>h.Check));
            CreateMap<Check, CheckDTO>();
            CreateMap<AuditInfoDto, AuditInfo>();
            CreateMap<AuditChecklistDto, AuditChecklistInfo>();
            CreateMap<Checklist, MasterChecklistDTO>();
        }
       
    }
}
