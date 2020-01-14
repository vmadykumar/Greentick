///-----------------------------------------------------------------
///   Namespace:     EscalationMgmt.DataLayer.DataInterfaceLayer
///   Class:         IEscalationRepository
///   Description:    Data interface Layer for Escalation data
///   Author:        Keshav M                   Date:24/10/2018
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------

using EscalationMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscalationMgmt.DataLayer.DataInterfaceLayer
{
    public interface IEscalationRepository
    {
        List<Escalation> GetAllEscalation();
    }
}
