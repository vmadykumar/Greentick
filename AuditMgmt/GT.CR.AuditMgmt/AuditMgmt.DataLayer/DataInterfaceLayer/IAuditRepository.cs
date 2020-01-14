using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;

namespace AuditMgmt.DataLayer.DataInterfaceLayer
{
    public interface IAuditRepository
    {
        List<UserChecklistAuditInfoDto> GetAuditsByUserID(string userID, List<string> UUIDs, string Role ,string LocationCode);
        List<UserChecklistAuditInfoDto>  GetAuditBasicInfoForUserID(string userID, List<string> UUIDs, string Role ,string LocationCode);
        bool SaveAudit(List<UserChecklistAuditInfo> userinfo);
        bool SaveChecklist(List<AuditChecklist> checklist);
        bool SaveCheck(List<AuditCheck> check);
        List<ChecklistLastPerformedDetailsDto> GetLastPerformCheckListDetails(List<LastPerformedDto> model);
        
        bool IsAuditStatusMatched(string  auditCode, List<string> statuses);
        bool IsCheckListStatusMatched(string checkListCode, List<string> statuses);
        void logMessagesToDb(List<NotificationBody> msgBodies);
        List<NotificationLog> CheckOutBox(string firebaseTopic);

        void UpdateNotificationStatus(string NotificationID, string status);
        List<UserNotificationDto> GetAllNotificationforUser(string firebaseTopic);
        bool DeleteAttachement(string filename);

        List<string> GetAllAttachementNames();
        bool PublishAudits(List<string> AuditCodes);

        List<UserChecklistAuditInfoDto> GetAuditsByAuditCode(List<string> AuditCode);
         List<ReportDto> GetAuditSummaryReport(List<string> UserID);
        List<AuditDto> GetAudits(AuditInfoDTO auditDTO);
        List<ChecklistDto> GetChecklistByAuditCode(List<CodeStatusDto> codeStatus);
         List<AuditCheck> GetChecksByChecklistCode(List<CodeStatusDto> codeStatus);
    }
}
