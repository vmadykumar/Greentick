using AuditMgmt.CommonLayer.Models.Entities;
using AuditMgmt.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace UnitTestAuditMgmt
{
    [TestClass]
    public class SaveAudit
    {

        [TestMethod]

        public void SaveAudit_returns_true_value()
        {
            var auditObj = JsonConvert.DeserializeObject<List<Audit>>(string.Join("", File.ReadAllText("../netcoreapp2.1/AddJson.json")));
            new AuditController(TestConfiguration.AuditManager, TestConfiguration.Configuration).SaveAudit(auditObj);

        }
    }
}
