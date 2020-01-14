using AuditMgmt.BusinessLayer.BusinessImplementationLayer;
using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
using AuditMgmt.CommonLayer.CommonImplementationLayer;
using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using AuditMgmt.Controllers;
using AuditMgmt.DataLayer;
using AuditMgmt.DataLayer.DataImplementationLayer;
using AuditMgmt.DataLayer.DataInterfaceLayer;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestAuditMgmt
{
    [TestClass]
    public class GetAuditsByUserID
    {


        [TestMethod]
        [DataRow("UU0000001", "Auditor", "LOB000004", "BM0000001", "MCD000001", 24)]
        [DataRow("UU0000001", "Manager", "LOB000004", "BM0000001", "MCD000001", 24)]
        public void Controller_GetAuditsByUserID_returns_true_value(string userID, string Role, string LOBCode, string BMCode, string LocationCode, int count)
        {
            var auditMgr = new Mock<IAuditManager>();
            var config = new Mock<IConfiguration>();
            auditMgr.Setup(m => m.GetAuditsByUserID(userID, new List<string>() { userID }, Role)).Returns(new AuditDto[count].ToList());
            var audit = new AuditController(auditMgr.Object, config.Object).GetAuditsByUserID(userID, Role, LOBCode, BMCode, LocationCode);
            Assert.AreEqual(audit.Count, count);
        }

        [TestMethod]
        [DataRow("UU0000001", "Auditor", "LOB000004", "BM0000001", "MCD000001", 24)]
        [DataRow("UU0000001", "Manager", "LOB000004", "BM0000001", "MCD000001", 24)]
        public void Manager_GetAuditsByUserID_returns_true_value(string userID, string Role, string LOBCode, string BMCode, string LocationCode, int count)
        {
            var auditDl = new Mock<IAuditRepository>();
            var rabitMQ = new Mock<IRabbitMQManager>();
            auditDl.Setup(m => m.GetAuditsByUserID(userID, new List<string>() { userID }, Role)).Returns(new AuditDto[count].ToList());
            var audit = new AuditManager(auditDl.Object, rabitMQ.Object).GetAuditsByUserID(userID, new List<string>() { userID }, Role);
            Assert.AreEqual(audit.Count, count);
        }

        //[TestMethod]
        //[DataRow("UU0000001", "Auditor", "LOB000004", "BM0000001", "MCD000001", 24)]
        //[DataRow("UU0000001", "Manager", "LOB000004", "BM0000001", "MCD000001", 24)]
        //public void DL_GetAuditsByUserID_returns_true_value(string userID, string Role, string LOBCode, string BMCode, string LocationCode, int count)
        //{

        //    //var mockSet = new Mock<DbSet<Audit>>();
        //    //var options = new DbContextOptionsBuilder() { };
        //    //options.UseInMemoryDatabase("abc");
        //    //var mockContext = new Mock<AuditContext>(options);
        //    //mockContext.Setup(m => m.Audit).Returns(mockSet.Object);
        //    var autoMapper = new Mock<IMapper>();
        //    var service = new AuditRepository(context, autoMapper.Object);
        //    var Auditcount = service.GetAuditsByUserID(userID);
        //    Assert.AreEqual(Auditcount.Count, count);
        //}

    }
}
