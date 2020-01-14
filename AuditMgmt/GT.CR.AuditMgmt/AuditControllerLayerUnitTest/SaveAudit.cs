using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
using AuditMgmt.CommonLayer.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic; 

namespace AuditControllerLayerUnitTest
{
    [TestClass]
    public class SaveAudit
    {
        Mock<IAuditManager> auditMgr;
        private Mock<IConfiguration> config;
        public SaveAudit()
        {
            auditMgr = new Mock<IAuditManager>();
            config = new Mock<IConfiguration>();

        }
        //public void Returns_ValidCount(List<Audit> audits)
        //{
        //auditMgr.Setup(m => m.SaveAudit(List < Audit > audits).Returns(true);
        //}
    }
}
