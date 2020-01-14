using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.Controllers;
using AuditMgmt.Controllers.AuditAggregator;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AuditControllerLayerUnitTest
{
    [TestClass]
    public class GetAuditsByUserIDTest
    {
        Mock<IAuditManager> auditMgr;
        private Mock<IConfiguration> config;
        public GetAuditsByUserIDTest()
        {
            auditMgr = new Mock<IAuditManager>();
            config = new Mock<IConfiguration>();
            config.SetupGet(c => c["AMBaseURL"]).Returns("http://172.21.10.138:8015/");
        }

        [TestMethod]
        [DataRow("UU0000001", "Auditor", "LOB000004", "BM0000001", "MCD000001", "SAVE",24)]
        [DataRow("UU0000001", "Manager", "LOB000004", "BM0000001", "MCD000001", "SAVE", 24)]
        public void Returns_ValidCount(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName ,int count)
        {

            //auditMgr.Setup(m => m.GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode, TeamName)).Returns(new AuditDto[count].ToList());
            //var audit = new AuditController(auditMgr.Object, config.Object).GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode);
           // Assert.AreEqual(audit.Count, count);
        }

        [TestMethod]
        [DataRow("1", "Auditor", "LOB000004", "BM0000001", "MCD000001", "SAVE", 0)]
        [DataRow("1", "Manager", "LOB000004", "BM0000001", "MCD000001", "SAVE", 0)]
        public void InvlaidUUIDReturns_Zero(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName,int count)
        {
            //auditMgr.Setup(m => m.GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode, TeamName)).Returns(new AuditDto[count].ToList());
           // var audit = new AuditController(auditMgr.Object, config.Object).GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode);
           // Assert.AreEqual(audit.Count, count);
        }
        [TestMethod]
        [DataRow("UU0000001", "a", "LOB000004", "BM0000001", "MCD000001", "SAVE", 0)]
        [DataRow("UU0000001", "b", "LOB000004", "BM0000001", "MCD000001", "SAVE", 0)]
        public void InvlaidRoleReturns_Zero(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName, int count)
        {
           // auditMgr.Setup(m => m.GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode, TeamName)).Returns(new AuditDto[count].ToList());
           // var audit = new AuditController(auditMgr.Object, config.Object).GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode);
            //Assert.AreEqual(audit.Count, count);
        }

        [TestMethod]
        [DataRow("UU0000001", "Auditor", "L", "BM0000001", "MCD000001", "SAVE", 0)]
        [DataRow("UU0000001", "Manager", "L", "BM0000001", "MCD000001", "SAVE", 0)]

        public void InvlaidLOBReturns_Zero(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName, int count)
        {
            //auditMgr.Setup(m => m.GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode, TeamName)).Returns(new AuditDto[count].ToList());
            //var audit = new AuditController(auditMgr.Object, config.Object).GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode);
            //Assert.AreEqual(audit.Count, count);
        }
        [TestMethod]
        [DataRow("UU0000001", "Auditor", "LOB000004", "B", "MCD000001", "SAVE", 0)]
        [DataRow("UU0000001", "Manager", "LOB000004", "B", "MCD000001", "SAVE", 0)]

        public void InvlaidBMReturns_Zero(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName, int count)
        {
           // auditMgr.Setup(m => m.GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode, TeamName)).Returns(new AuditDto[count].ToList());
           // var audit = new AuditController(auditMgr.Object, config.Object).GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode);
           // Assert.AreEqual(audit.Count, count);
        }

        [TestMethod]
        [DataRow("UU0000001", "Auditor", "LOB000004", "BM0000001", "M", "SAVE", 0)]
        [DataRow("UU0000001", "Manager", "LOB000004", "BM0000001", "M", "SAVE", 0)]

        public void InvlaidLocationCodeReturns_Zero(string userID, string Role, string LOBCode, string BMCode, string LocationCode, string TeamName, int count)
        {
            //auditMgr.Setup(m => m.GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode, TeamName)).Returns(new AuditDto[count].ToList());
            //var audit = new AuditController(auditMgr.Object, config.Object).GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode);
           // Assert.AreEqual(audit.Count, count);
        }
    }
}
