///-----------------------------------------------------------------
///   Namespace:     EscalationMgmt.BusinessLayer.BusinessInterfaceLayer
///   Class:         IEscalationManager
///   Description:    Businees interface Layer for Escalation data
///   Author:        Keshav M                   Date:24/10/2018
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------


using EscalationMgmt.CommonLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscalationMgmt.BusinessLayer.BusinessInterfaceLayer
{
    public interface IEscalationManager
    {
        List<Escalation> GetAllEscalation();
    }
}
