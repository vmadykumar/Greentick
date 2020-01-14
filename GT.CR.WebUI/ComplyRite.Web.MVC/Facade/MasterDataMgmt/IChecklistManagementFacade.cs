using ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.Checklist_Mgmt;
using ComplyRite.Web.MVC.Facade.SchedulingMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyRite.Web.MVC.Facade.MasterDataMgmt
{
    interface IChecklistManagementFacade
    {
      
        Task<List<ChecklistModel_New>> GetAllChecklistsByAccount(string url);
        Task<ChecklistModel_New> GetChecklistByChecklistCode(string url);
        Task<UserDetailsModel> GetLocationsByUUID(string url);
        Task<object> GetAreaByUUIDAndLocationCode(string url);
        Task<object> GetSubAreaByUUIDLocationCodeAndAreaCode(string url);
        Task<object> CreateNewChecklist(string url, List<ChecklistDTO> ChecklistDTO);

        Task<object> EditChecklist(string url, ChecklistDTO ChecklistDTO);
        Task<bool> ValidateDuplicateChecklistTitle(string Url);
        Task<bool> ValidateDuplicateChecklistChecks(string Url);

    }
}