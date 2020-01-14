using MasterMgmt.CommonLayer.Models.DTO;
using MasterMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;

namespace MasterMgmt.BusinessLayer.BusinessInterfaceLayer
{
    public interface IMasterManager
    {
        List<MasterChecklistDTO> GetAllChecklistsByAccount(string Lobcode);
        List<Check> GetAllChecks();
        Check GetCheckByCheckCode(string CheckCode);
        MasterChecklistDTO GetChecklistByChecklistCode(string ChecklistCode, string Subareacode);
        object CreateNewChecklist(List<Checklist> checklist);
        object EditChecklist(Checklist checklist);
        bool SaveChecks(List<Check> checks);
        object CreateNewCheck(Check Check);
        object EditCheck(Check Check);
        Checklist GetChecklistWithChecksByChecklistID(int ChecklistID);
        List<ChecklistDTO> GetChecklistForArea(string SubAreaCode);
        List<AuditInfo> GetAuditInfo(DateTime? StartDateTime, DateTime? EndDateTime);
        List<ChecklistDTO> GetChecklistsByAuditID(int AuditID);
        List<Check> GetAllChecksByChecklistID(int ChecklistID, string SubAreaCode);
        List<Check> GetAllChecksByChecklistID(int ChecklistID);
        CountDto GetCountForPublishedAudits(string Fbo);
        List<string> SaveAuditInfo(List<AuditInfoDto> auditInfo);
        bool ValidateDuplicateCheckTitle(string CheckTitle, int CheckID);
        bool ValidateDuplicateChecklistTitle(string SubAreaCode,string ChecklistName, int ChecklistID);
        bool ValidateDuplicateChecklistChecks(string SubAreaCode, int[] Checks, int ChecklistID);
        List<string> UpdatePublishedAudits(List<AuditInfoDto> auditInfo);
    }
}
