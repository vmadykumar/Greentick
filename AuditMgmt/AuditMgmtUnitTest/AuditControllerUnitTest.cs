using AuditMgmt.BusinessLayer.BusinessImplementationLayer;
using AuditMgmt.BusinessLayer.BusinessInterfaceLayer;
using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using AuditMgmt.Controllers;
using AuditMgmt.DataLayer;
using AuditMgmt.DataLayer.DataImplementationLayer;
using AuditMgmt.DataLayer.DataInterfaceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AuditMgmtUnitTest
{
    [TestFixture]
    public class AuditControllerUnitTest
    {
        Mock<IAuditManager> _auditManager;
        AuditController controller;

        #region TestMethodsSetup\
        /// <summary>
        /// Test Setup for all test methods
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            _auditManager = new Mock<IAuditManager>();
            controller = new AuditController(_auditManager.Object);
        }
        #endregion

        #region GetAuditsByUserID()
        /// <summary>
        /// Checks the number of audits for a user
        /// </summary>
        [Test]
        public void GetAuditsByUserID_ValidUser_CheckNoOfAudits()
        {
            //Arrange
            List<AuditDto> audits;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Audits.json");
            {
                string json = r.ReadToEnd();
                audits = JsonConvert.DeserializeObject<List<AuditDto>>(json);
            }
            _auditManager.Setup(x => x.GetAuditsByUserID("1")).Returns(audits);

            //Act
            var actual = controller.GetAuditsByUserID("1");
            var auditcount = actual.FindAll(x => x.UserID == "1").Count();

            //Assert
            Assert.AreEqual(2, auditcount);
        }

        /// <summary>
        /// Checks the number of checklists for a user
        /// </summary>
        [Test]
        public void GetAuditsByUserID_ValidUser_CheckNoOfChecklists()
        {
            //Arrange
            List<AuditDto> auditDTO;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Audits.json");
            {
                string json = r.ReadToEnd();
                auditDTO = JsonConvert.DeserializeObject<List<AuditDto>>(json);
            }
            _auditManager.Setup(x => x.GetAuditsByUserID("1")).Returns(auditDTO);

            //Act
            var actual = controller.GetAuditsByUserID("1");
            var audit = actual.FindAll(x => x.UserID == "1");
            var checklistcount = 0;
            for(var i=0;i<audit.Count;i++)
            {
                checklistcount += audit[i].AuditChecklist.Count();
            }

            //Assert
            Assert.AreEqual(4, checklistcount);
        }

        /// <summary>
        /// Checks the number of checks for a user
        /// </summary>
        [Test]
        public void GetAuditsByUserID_ValidUser_CheckNoOfChecks()
        {
            //Arrange
            List<AuditDto> auditDTOs;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Audits.json");
            {
                string json = r.ReadToEnd();
                auditDTOs = JsonConvert.DeserializeObject<List<AuditDto>>(json);
            }
            _auditManager.Setup(x => x.GetAuditsByUserID("1")).Returns(auditDTOs);

            //Act
            var actual = controller.GetAuditsByUserID("1");
            var audit = actual.FindAll(x => x.UserID == "1");
            List<ChecklistDto> checklistDTOs;
            int checkscount = 0;
            for (int i = 0; i < audit.Count; i++)
            {
                checklistDTOs = audit[i].AuditChecklist;
                for (int j = 0; j < checklistDTOs.Count; j++)
                {
                    checkscount += checklistDTOs[j].AuditCheck.Count();
                }
            }

            //Assert
            Assert.AreEqual(13, checkscount);
        }
        #endregion

        #region GetAuditBetweenDates()
        /// <summary>
        /// Gets count of audits for a user between the dates
        /// </summary>
        [Test]
        public void GetAuditBetweenDates_ValidUser_AuditCount()
        {
            //Arrange
            List<AuditDto> auditDTOs;
            DateTime date1 = new DateTime(2018, 07, 31);
            DateTime date2 = new DateTime(2018, 08, 6);
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Audits.json");
            {
                string json = r.ReadToEnd();
                auditDTOs = JsonConvert.DeserializeObject<List<AuditDto>>(json);
            }
            List<AuditDto> audits = auditDTOs.Where(x => x.AuditStartDateTime >= date1)
                .Where(x => x.AuditEndDateTime <= date2).ToList();
            _auditManager.Setup(x => x.GetAuditBetweenDates("1", date1, date2)).Returns(audits);

            //Act
            var actual = controller.GetAuditBetweenDates("1", date1, date2);
            int count = actual.Count();

            //Assert
            Assert.AreEqual(1, count);
        }
        #endregion

        #region TestMethodsTearDown
        /// <summary>
        /// Test Teardown for all the test methods
        /// </summary>
        [TearDown]
        public void TestTeardown()
        {
            _auditManager = null;
            controller = null;
        }
        #endregion
        
    }
}
