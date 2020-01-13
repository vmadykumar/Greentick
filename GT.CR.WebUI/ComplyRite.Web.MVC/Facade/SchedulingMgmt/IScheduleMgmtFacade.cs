using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Dashboard;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling.DTO;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingAudit;
using MasterMgmt.CommonLayer.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyRite.Web.MVC.Facade.SchedulingMgmt
{
    public interface IScheduleMgmtFacade
    {
        Task<List<AuditListModel>> GetPublishedAuditList(string url);
        Task<List<Checklist>> GetAuditDetailsByAuditId(string url);
        Task<List<CheckModel>> GetCheckDataByChecklistID(string url);
        Task<object> SaveAuditSchedule(string url, object auditSchedule);
        Task<List<Area>> GetAllUserAreasByLocation(string url);
        Task<List<SubArea>> GetAllUserSubAreasbyLocation(string url);
        Task<List<UserDetailsModel>> GetUserDetailsByUUID(string url); 
        Task<UserDetailsModel> GetUserByUUID(string url); 
         Task<List<AuditChecklist>> GetAllChecklistForAreaSubArea(string url);
        Task<List<AuditorModel>> GetAllAuditorsByLocation(string url);
        Task<List<AuditCheckViewModel>> GetAllChecksByChecklistID(string url);
        Task<bool> PublishScheduledAudit(string url, string auditId);
        Task<DashboardModel> GetPublishedAuditCount(string url);
        Task<List<City>> GetAllCitiesByUUID(string url);
        Task<List<Location>> GetAllLocationsByCityAndUUID(string url);
        Task<List<LocationUserbyManagerDTO>> GetAllLocationUserbyManager(string url);
        Task<List<AreaDTO>> GetAllUserAreasandSubAreasbyLocation(string url);

        


    }
}
