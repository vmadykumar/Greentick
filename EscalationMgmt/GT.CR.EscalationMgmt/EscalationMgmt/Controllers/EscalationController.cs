using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscalationMgmt.BusinessLayer.BusinessInterfaceLayer;
using EscalationMgmt.CommonLayer.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscalationMgmt.Controllers
{
    [Produces("application/json")]
    [Route("api/Escalation")]
    public class EscalationController : Controller
    {
        readonly IEscalationManager _escalationManager = null;
        public EscalationController(IEscalationManager escalationManager)
        {
            this._escalationManager = escalationManager;
        }

        /// <summary>
        /// Get All Escalation
        /// </summary>
        /// <returns>List Of Escalations</returns>

        [HttpGet]
        public List<Escalation> GetAllEscalation()
        {
            try
            {
                return _escalationManager.GetAllEscalation();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}