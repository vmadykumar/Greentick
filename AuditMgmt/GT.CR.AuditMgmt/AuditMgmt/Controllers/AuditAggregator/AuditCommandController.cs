
///-----------------------------------------------------------------
///   Namespace:      AuditMgmt.Controllers
///   Class:         AuditController
///   Description:    Controller for Audit data
///   Author:        Keshav M                   Date: 21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
using AuditMgmt.BusinessLayer.ExcelFactory;
using AuditMgmt.CommonLayer.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AuditMgmt.Controllers.AuditAggregator
{

    [Route("api/Audit/[Action]")]
    public partial class AuditController : Controller
    {
        readonly IAuditManager _auditManager = null;
        readonly IConfiguration _configuration;
        LoadData loadData;
        public AuditController(IAuditManager auditManager, IConfiguration Configuration, LoadData _loadData)
        {
            _auditManager = auditManager;
            _configuration = Configuration;
            loadData = _loadData;
        }

        #region Audit CUD
        /// <summary>
        /// Saves the list of audit details
        /// </summary>
        /// <param name="audits"></param>
        [HttpPost]
        public void SaveAudit([FromBody]List<UserChecklistAuditInfo> audits)
        {
            _auditManager.SaveAudit(audits);
        }

        [HttpPost]
        public void SaveChecklist([FromBody]List<AuditChecklist> checklist)
        {
            _auditManager.SaveChecklist(checklist);
        }

        [HttpPost]
        public void SaveCheck([FromBody]List<AuditCheck> check)
        {
            _auditManager.SaveCheck(check);
        }

        #endregion

        /// <summary>
        /// Dummy method
        /// </summary>
        /// <returns></returns>

        public string Abc => "Sucess";
    }
}
