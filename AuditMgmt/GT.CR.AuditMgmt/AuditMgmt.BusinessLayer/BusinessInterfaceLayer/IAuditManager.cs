using AuditMgmt.CommonLayer.Models;
using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuditMgmt.BusinessLayer.BusinessInterfaceLayer
{
    public interface IAuditManager
    {
        List<UserChecklistAuditInfoDto> GetAuditsByUserID(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName);
        List<UserChecklistAuditInfoDto> GetAuditBasicInfoForUserID(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName);
        bool SaveAudit(List<UserChecklistAuditInfo> userAudits);
        bool SaveCheck(List<AuditCheck> check);
        bool SaveChecklist(List<AuditChecklist> checklist);
        List<ChecklistLastPerformedDetailsDto> GetLastPerformCheckListDetails(List<LastPerformedDto> model);
        void CheckOutBox(string firebaseTopic);
        void UpdateNotificationStatus(string NotificationID, string status);
        List<UserNotificationDto> GetAllNotificationforUser(string firebaseTopic);
        bool DeleteAttachement(string filename);
        bool PublishAudits(List<string> AuditCode);
        List<UserChecklistAuditInfoDto> GetAuditsByAuditCode(List<string> AuditCode);
          List<ReportDto> GetAuditSummaryReport(string userID , string LOBCode, string BMCode, string TeamName);
        List<AuditDto> GetAudits(AuditInfoDTO auditDTO);
        List<ChecklistDto> GetChecklistByAuditCode(List<CodeStatusDto> codeStatus);
        List<AuditCheck> GetChecksByChecklistCode(List<CodeStatusDto> codeStatus);

    }
}
