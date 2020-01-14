using System;
using System.Collections.Generic;
using System.Text;
using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using AuditMgmt.Controllers;
using AuditMgmt.DataLayer.DataImplementationLayer;
using AuditMgmt.DataLayer.DataInterfaceLayer;
using NUnit.Framework;

namespace AuditMgmtUnitTest.AuditControllerTest
{
    [TestCase]
    public class GetAuditsByUserID
    {
        [TestMethod]
        public void GetAuditsByUserID_WithAllValidParameters_ReturnallAudits()
        {
            //Arraange--- 
            //IAuditRepository ac = new AuditRepository();

            //List<string> listonbj = new List<string>() { "UU0000001", "UU0000002" };
            ////Act--- 
            //List<AuditDto> Result = ac.GetAuditsByUserID("UU0000001", listonbj, "RR0000001");
            ////Assert--- 
            //Assert.AreEqual("McDonalds", Result[0].AuditName);



            //Assert.IsNotNull(Result);
          
        }
    }
}
