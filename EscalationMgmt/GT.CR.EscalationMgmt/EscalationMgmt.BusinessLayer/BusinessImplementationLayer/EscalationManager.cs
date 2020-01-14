///-----------------------------------------------------------------
///   Namespace:     EscalationMgmt.BusinessLayer.BusinessImplementationLayer
///   Class:         EscalationManager
///   Description:    Businees Layer for Escalation data
///   Author:        Keshav M                   Date:24/10/2018
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------


using EscalationMgmt.BusinessLayer.BusinessInterfaceLayer;
using EscalationMgmt.CommonLayer.Models.Entities;
using EscalationMgmt.DataLayer.DataInterfaceLayer;
using EscalationMgmt.Exceptions;
using System;
using System.Collections.Generic;

namespace EscalationMgmt.BusinessLayer.BusinessImplementationLayer
{
    public class EscalationManager : IEscalationManager
    {
        readonly IEscalationRepository _escalationRepository = null;
        public EscalationManager(IEscalationRepository escalationRepository)
        {
            this._escalationRepository = escalationRepository;
        }

        /// <summary>
        /// Get All Escalation
        /// </summary>
        /// <returns>List Of Escalations</returns>
        public List<Escalation> GetAllEscalation()
        {
            try
            {
                return _escalationRepository.GetAllEscalation();
            }
            catch (Exception e)
            {

                throw new BusinessLayerException("Business Layer Exception" , e);
            }
            
        }
    }
}
