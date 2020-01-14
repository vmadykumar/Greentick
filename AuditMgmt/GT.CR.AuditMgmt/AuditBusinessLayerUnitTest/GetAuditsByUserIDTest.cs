//using AuditMgmt.DataLayer.DataInterfaceLayer;
//using AuditMgmt.CommonLayer.Models.DTO;
//using Microsoft.Extensions.Configuration;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using System.Linq;
//using Moq;
//using AuditMgmt.BusinessLayer.BusinessImplementationLayer;
//using AuditMgmt.CommonLayer.CommonImplementationLayer;

//namespace AuditBusinessLayerUnitTest
//{
//    [TestClass]
//    public class GetAuditsByUserIDTest
//    {
//        Mock<IAuditRepository> auditRepo;
//        private Mock<IRabbitMQManager> rabbitconfig;

//        public GetAuditsByUserIDTest()
//        {
//            auditRepo = new Mock<IAuditRepository>();
//            rabbitconfig = new Mock<IRabbitMQManager>();
//        }

//        [TestMethod]
//        [DataRow("UU0000001", "Auditor",  "UU0000011",  24)]
//        [DataRow("UU0000001", "Manager",  "UU0000011",  24)]
//        public void Returns_ValidCount(string userID, string Role, string UUIDs, int count)
//        {
//            //auditRepo.Setup(m => m.GetAuditsByUserID(userID, new List<string>() { UUIDs }, Role)).Returns(new AuditDto[count].ToList());
//            //var audit = new AuditManager(auditRepo.Object, rabbitconfig.Object).GetAuditsByUserID(userID, new List<string>() { UUIDs }, Role);
//            //Assert.AreEqual(audit.Count, count);
//        }

//        [TestMethod]
//        [DataRow("1", "Auditor",  "UU0000011",  0)]
//        [DataRow("1", "Manager",  "UU0000011",  0)]
//        public void InvlaidUUIDReturns_Zero(string userID, string Role, List<string> UUIDs, int count)
//        {
//            //auditRepo.Setup(m => m.GetAuditsByUserID(userID, new List<string>() { userID }, Role)).Returns(new AuditDto[count].ToList());
//            //var audit = new AuditManager(auditRepo.Object, rabbitconfig.Object).GetAuditsByUserID(userID, UUIDs, Role);
//            //Assert.AreEqual(audit.Count, count);
//        }
//        [TestMethod]
//        [DataRow("UU0000001", "a",  "UU0000011",  0)]
//        [DataRow("UU0000001", "b",  "UU0000011",  0)]
//        public void InvlaidRoleReturns_Zero(string userID, string Role, List<string> UUIDs, int count)
//        {
//            auditRepo.Setup(m => m.GetAuditsByUserID(userID, new List<string>() { userID }, Role)).Returns(new AuditDto[count].ToList());
//            var audit = new AuditManager(auditRepo.Object, rabbitconfig.Object).GetAuditsByUserID(userID, UUIDs, Role);
//            Assert.AreEqual(audit.Count, count);
//        }

//    }

//}




