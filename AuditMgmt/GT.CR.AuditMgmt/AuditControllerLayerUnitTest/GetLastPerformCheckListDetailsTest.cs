using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.Controllers.AuditAggregator;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AuditControllerLayerUnitTest
{
    [TestClass]
    public class GetLastPerformCheckListDetailsTest
    {
        private Mock<IAuditManager> auditMgr;
        private Mock<IConfiguration> config;
        public GetLastPerformCheckListDetailsTest()
        {
            auditMgr = new Mock<IAuditManager>();
            config = new Mock<IConfiguration>();
            config.SetupGet(c => c["AMBaseURL"]).Returns("http://172.21.10.138:8015/");
        }
        [TestMethod]
        [DataRow(24)]
        [DataRow("UU0000001", "Manager", "LOB000004", "BM0000001", "MCD000001", 24)]
        public void Returns_ValidCount(List<LastPerformedDto> model, int count)
        {
            //auditMgr.Setup(m => m.GetLastPerformCheckListDetails(null)).Returns(new ChecklistLastPerformedDetailsDto[count].ToList());
           // var audit = new AuditController(auditMgr.Object, config.Object).GetLastPerformCheckListDetails(new List<LastPerformedDto>() { new LastPerformedDto() { } });
           // Assert.AreEqual(audit.Count, count);
        }
    }
}
