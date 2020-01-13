using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.CheckMgmt;
using ComplyRite.Web.MVC.ServiceRepository;

namespace ComplyRite.Web.MVC.Facade.MasterDataMgmt
{
    public class CheckManagementFacade : ICheckManagementFacade
    {
        #region Global Variable Declaration
        private readonly IServiceUtility IServiceUtility = new ServiceUtility();
        #endregion
        public async Task<CheckModel> GetCheckViewData(string id)
        {
            //return IServiceUtility.GetDataFromService<CheckModel>(URL, "CheckMgmtServiceUrl");
            throw new NotImplementedException();
        }

        public Task<List<CheckModel>> GetListOfChecks(string status)
        {
            //return await IServiceUtility.GetDataFromService<CheckModel>(URL, "CheckMgmtServiceURL")
            throw new NotImplementedException();
        }

        public async Task<List<CheckModel_New>> GetAllChecks(string url)
        {
            return await IServiceUtility.GetDataFromService<List<CheckModel_New>>(url, "ScheduleMgmtServiceURL");
        }

        public async Task<CheckModel_New> GetCheckByCheckCode(string Url)
        {
            return await IServiceUtility.GetDataFromService<CheckModel_New>(Url, "ScheduleMgmtServiceURL");
        }

        public async Task<object> CreateNewCheck(string Url, CheckModel_New Check)
        {
            return await IServiceUtility.PostDataToService<object>(Url, "ScheduleMgmtServiceURL", Check);
        }

        public async Task<object> EditCheck(string Url, CheckModel_New Check)
        {
            return await IServiceUtility.PostDataToService<object>(Url, "ScheduleMgmtServiceURL", Check);
        }

        public async Task<bool> ValidateDuplicateCheckTitle(string Url)
        {
            return await IServiceUtility.GetDataFromService<bool>(Url, "ScheduleMgmtServiceURL");
        }
    }
}