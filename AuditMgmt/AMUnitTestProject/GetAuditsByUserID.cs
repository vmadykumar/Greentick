using System;
using System.Collections.Generic;
using System.Text;
using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using AuditMgmt.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuditMgmtUnitTest.AuditControllerTest
{
    [TestClass]
    public class GetAuditsByUserID
    {


        [TestMethod]
        public void GetAuditsByUserID_WithAllValidParameters_ReturnallAudits()
        {
            //Arraange--- 
            AuditController ac = new AuditController();
            //Act--- 
            List<AuditDto> Result = ac.GetAuditsByUserID("", "", "", "", "");
            //Assert--- 
            Assert.AreEqual("McDonalds", Result[0].AuditName);
          
        }
    }
}
