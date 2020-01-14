using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.Checklist_Mgmt;
using ComplyRite.Web.MVC.Facade.SchedulingMgmt;
using ComplyRite.Web.MVC.ServiceRepository;

namespace ComplyRite.Web.MVC.Facade.MasterDataMgmt
{
    public class ChecklistManagementFacade : IChecklistManagementFacade
    {
        private readonly IServiceUtility IServiceUtility = new ServiceUtility();
        //public async Task<List<ChecklistModel_New>> GetAllChecklists(string url)
        //{
        //    return await IServiceUtility.GetDataFromService<List<ChecklistModel_New>>(url, "ScheduleMgmtServiceURL");
        //}
        public async Task<List<ChecklistModel_New>> GetAllChecklistsByAccount(string url)
        {
            return await IServiceUtility.GetDataFromService<List<ChecklistModel_New>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<ChecklistModel_New> GetChecklistByChecklistCode(string url)
        {
            return await IServiceUtility.GetDataFromService<ChecklistModel_New>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<UserDetailsModel> GetLocationsByUUID(string url)
        {
            return await IServiceUtility.GetDataFromService<UserDetailsModel>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<object> GetAreaByUUIDAndLocationCode(string url)
        {
            return await IServiceUtility.GetDataFromService<object>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<object> GetSubAreaByUUIDLocationCodeAndAreaCode(string url)
        {
            return await IServiceUtility.GetDataFromService<object>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<object> CreateNewChecklist(string url, List<ChecklistDTO> ChecklistDTO)
        {
            return await IServiceUtility.PostDataToService<object>(url, "ScheduleMgmtServiceURL", ChecklistDTO);
        }

        public async Task<object> EditChecklist(string url, ChecklistDTO ChecklistDTO)
        {
            return await IServiceUtility.PostDataToService<object>(url, "ScheduleMgmtServiceURL", ChecklistDTO);
        }


        public async Task<bool> ValidateDuplicateChecklistTitle(string Url)
        {
            return await IServiceUtility.GetDataFromService<bool>(Url, "ScheduleMgmtServiceURL");
        }
        
        public async Task<bool> ValidateDuplicateChecklistChecks(string Url)
        {
            return await IServiceUtility.GetDataFromService<bool>(Url, "ScheduleMgmtServiceURL");
        }
    }
}