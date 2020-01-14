///-----------------------------------------------------------------
///   Namespace:     EscalationMgmt.DataLayer.DataImplementationLayer
///   Class:         EscalationRepository
///   Description:    Data Layer for Escalation data
///   Author:        Keshav M                   Date:24/10/2018
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------


using EscalationMgmt.CommonLayer.Models.Entities;
using EscalationMgmt.DataLayer.DataInterfaceLayer;
using EscalationMgmt.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscalationMgmt.DataLayer.DataImplementationLayer
{
    public class EscalationRepository : IEscalationRepository
    {
        /// <summary>
        /// Get All Escalation
        /// </summary>
        /// <returns>List Of Escalations</returns>
        public List<Escalation> GetAllEscalation()
        {
            try
            {
                return new List<Escalation>();
            }
            catch (Exception e)
            {
                throw new DataLayerException("Business Layer Exception", e);
            }
            
        }
    }
}
