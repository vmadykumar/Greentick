using MasterMgmt.CommonLayer.Models.DTO;
using MasterMgmt.CommonLayer.Models.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMgmt.DataLayer.DataInterfaceLayer
{
    public interface IMasterRepository
    {
        List<MasterChecklistDTO> GetAllChecklistsBySubAreas(List<string> SubAreaCodes);
        List<Check> GetAllChecks();
        Check GetCheckByCheckCode(string CheckCode);
        MasterChecklistDTO GetChecklistByChecklistCode(string ChecklistCode, string Subareacode);
        object CreateNewChecklist(List<Checklist> checklists);
        bool SaveChecks(List<Check> checks);
        object CreateNewCheck(Check Check);
        object EditCheck(Check Check);
        Checklist GetChecklistWithChecksByChecklistID(int ChecklistID);
        List<ChecklistDTO> GetChecklistForArea(string SubAreaCode);
        List<AuditInfo> GetAuditInfo();
        List<ChecklistDTO> GetChecklistsByAuditID(int AuditID);
        List<Check> GetAllChecksByChecklistID(int ChecklistID, string SubAreaCode);
        List<Check> GetAllChecksByChecklistID(int ChecklistID);
        CountDto GetCountForPublishedAudits(string Fbo);
        List<string> SaveAuditInfo(List<AuditInfoDto> auditInfoes);
        List<string> UpdatePublishedAudits(List<AuditInfoDto> auditInfoes);
        
        List<AuditInfo> GetAuditInfo(DateTime StartDateTime, DateTime EndDateTime);
        bool ValidateDuplicateCheckTitle(string CheckTitle, int CheckID);
        bool ValidateDuplicateChecklistTitle(string SubAreaCode,string ChecklistName, int ChecklistID);
        bool ValidateDuplicateChecklistChecks(string SubAreaCode, int[] Checks,int ChecklistID);
        object EditChecklist(Checklist checklist);
    }
}
