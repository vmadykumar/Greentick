using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Dashboard;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling.DTO;
using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingAudit;
using ComplyRite.Web.MVC.ServiceRepository;
using MasterMgmt.CommonLayer.Models.DTO;

namespace ComplyRite.Web.MVC.Facade.SchedulingMgmt
{
    public class ScheduleMgmtFacade : IScheduleMgmtFacade
    {
        #region Global Variable Declaration
        private readonly IServiceUtility IServiceUtility = new ServiceUtility();
        #endregion

        public async Task<List<AuditListModel>> GetPublishedAuditList(string url)
        {
            return await IServiceUtility.GetDataFromService<List<AuditListModel>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<Checklist>> GetAuditDetailsByAuditId(string url)
        {
            return await IServiceUtility.GetDataFromService<List<Checklist>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<CheckModel>> GetCheckDataByChecklistID(string url)
        {
            return await IServiceUtility.GetDataFromService<List<CheckModel>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<object> SaveAuditSchedule(string url, object auditSchedule)
        {
            return await IServiceUtility.PostDataToService<object>(url, "ScheduleMgmtServiceURL", auditSchedule);
        }

        public async Task<List<Area>> GetAllUserAreasByLocation(string url)
        {
            return await IServiceUtility.GetDataFromService<List<Area>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<SubArea>> GetAllUserSubAreasbyLocation(string url)
        {
            return await IServiceUtility.GetDataFromService<List<SubArea>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<UserDetailsModel>> GetUserDetailsByUUID(string url)
        {
            return await IServiceUtility.GetDataFromService<List<UserDetailsModel>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<UserDetailsModel> GetUserByUUID(string url)
        {
            return await IServiceUtility.GetDataFromService<UserDetailsModel>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<AuditChecklist>> GetAllChecklistForAreaSubArea(string url)
        {
            return await IServiceUtility.GetDataFromService<List<AuditChecklist>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<AuditorModel>> GetAllAuditorsByLocation(string url)
        {
            return await IServiceUtility.GetDataFromService<List<AuditorModel>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<AuditCheckViewModel>> GetAllChecksByChecklistID(string url)
        {
            return await IServiceUtility.GetDataFromService<List<AuditCheckViewModel>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<bool> PublishScheduledAudit(string url, string auditId)
        {
            return await IServiceUtility.PostDataToService<bool>(url, "ScheduleMgmtServiceURL", null);
        }

        //To get Dashboard Count
        public async Task<DashboardModel> GetPublishedAuditCount(string url)
        {
            return await IServiceUtility.GetDataFromService<DashboardModel>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<City>> GetAllCitiesByUUID(string url)
        {
            return await IServiceUtility.GetDataFromService<List<City>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<Location>> GetAllLocationsByCityAndUUID(string url)
        {
            return await IServiceUtility.GetDataFromService<List<Location>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<LocationUserbyManagerDTO>> GetAllLocationUserbyManager(string url)
        {
            return await IServiceUtility.GetDataFromService<List<LocationUserbyManagerDTO>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<List<AreaDTO>> GetAllUserAreasandSubAreasbyLocation(string url)
        {
            return await IServiceUtility.GetDataFromService<List<AreaDTO>>(url, "ScheduleMgmtServiceURL");
        }
    }
}