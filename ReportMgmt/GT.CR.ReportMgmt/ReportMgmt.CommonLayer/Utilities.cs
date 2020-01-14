using Newtonsoft.Json;
using ReportMgmt.CommonLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ReportMgmt.CommonLayer.Utility.UtilityLayer;

namespace ReportMgmt.CommonLayer
{
    public static class Utilities
    {
        public static AuditReportDto MapReport(string msg)
        {
            AuditReportDto report = null;
            var userAuditMessage = JsonConvert.DeserializeObject<UserChecklistAuditInfo>(msg);
             
            foreach (var auditMessage in userAuditMessage.Audit)
            {

                report = new AuditReportDto
                {
                    AuditID = auditMessage.AuditCode,
                    AuditReportType = auditMessage.AuditType,
                    Location = userAuditMessage.LocationName,
                    AuditDate = DateTimeConverter.GetLocalDatefromUTC(auditMessage.AuditEndDateTime),
                    PresentedBy = auditMessage.AuditedBy,
                    City = userAuditMessage.City,
                    AuditScheduledDate = DateTimeConverter.GetLocalDatefromUTC(auditMessage.AuditScheduledStartDateTime),
                    AuditorUUID = userAuditMessage.UserID,
                    ManagerInfo = auditMessage.ManagerInfo,
                    LocationCode = auditMessage.LocationCode,
                    LOBCode = auditMessage.LOBCode,
                    BMCode = auditMessage.BMCode,
                    TeamName = "SAVE",
                    Sections = auditMessage.AuditChecklist.GroupBy(a => a.ChecklistCategory).Select(c =>
                                    new SectionDto
                                    {
                                        SectionName = c.Key
                                    }).ToList()
                };
                report.Sections.ForEach(c =>
       {
           c.AuditAreas = new List<AuditAreaDto>();
           c.AuditAreas.AddRange(
               auditMessage.AuditChecklist.Where(a => c.SectionName.Equals(a.ChecklistCategory, StringComparison.OrdinalIgnoreCase)).Select(a =>
                          new AuditAreaDto
                          {
                              AreaOfAudit = a.ChecklistSubArea,
                              TotalCheckpointsFailed = a.TotalNoResponse,
                              TotalCheckpointsPassed = a.TotalYesResponse,
                              AuditDetails = a.AuditCheck.Select(m =>
                              {
                                  var auditdetail = new AuditDetailsDto { AuditCheckpoint = m.CheckName, AuditorsResponse = m.CheckResponse, Score = m.CheckScore, Comments = m.Remark, TimeStamp = DateTimeConverter.GetLocalDatefromUTC(m.PerformedDateTime) };

                                  return auditdetail;
                              }).ToList(),
                              Attachments = a.AuditCheck.SelectMany(m =>
                              {
                                  return m.Attachement.Select(k => k.AttachementName);
                              }).ToList()
                          }));

       });
            }
            
            return report;
        }
    }
}

