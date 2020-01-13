using ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.CheckMgmt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComplyRite.Web.MVC.Facade.MasterDataMgmt
{
    public interface ICheckManagementFacade
    {
        Task<List<CheckModel>> GetListOfChecks(string status);
        Task<CheckModel> GetCheckViewData(string id);
        Task<List<CheckModel_New>> GetAllChecks(string url);
        Task<CheckModel_New> GetCheckByCheckCode(string Url);
        Task<object> CreateNewCheck(string Url, CheckModel_New Check);
        Task<object> EditCheck(string Url, CheckModel_New Check);
        Task<bool> ValidateDuplicateCheckTitle(string Url);
    }
}