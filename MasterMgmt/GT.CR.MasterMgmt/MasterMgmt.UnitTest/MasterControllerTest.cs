using MasterMgmt.BusinessLayer.BusinessImplementationLayer;
using MasterMgmt.BusinessLayer.BusinessInterfaceLayer;
using MasterMgmt.CommonLayer.Models.DTO;
using MasterMgmt.CommonLayer.Models.Entities;
using MasterMgmt.Controllers;
using MasterMgmt.DataLayer;
using MasterMgmt.DataLayer.DataImplementationLayer;
using MasterMgmt.DataLayer.DataInterfaceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace MasterMgmt.UnitTest
{
    [TestFixture]
    public class MasterControllerTest
    {
        Mock<IMasterManager> _masterManager;
        MasterController masterController;

        #region TestMethodsSetup
        /// <summary>
        /// Test Setup for all test methods
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            _masterManager = new Mock<IMasterManager>();
           // masterController = new MasterController(_masterManager.Object);
        }
        #endregion

        #region GetAllChecklist()
        /// <summary>
        /// Returns true if checklist is not null
        /// </summary>
        [Test]
        public void GetAllChecklist_ReturnNotNull()
        {
            //Arrange
            List<Checklist> checklists;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Checklists.json");
            {
                string json = r.ReadToEnd();
                checklists = JsonConvert.DeserializeObject<List<Checklist>>(json);
            }
            _masterManager.Setup(x => x.GetAllChecklist()).Returns(checklists);

            //Act
            var actual = masterController.GetAllChecklist();

            //Assert
            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Returns the total number of checklists
        /// </summary>
        [Test]
        public void GetAllChecklist_ReturnsChecklistCount()
        {
            //Arrange
            List<Checklist> checklists;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Checklists.json");
            {
                string json = r.ReadToEnd();
                checklists = JsonConvert.DeserializeObject<List<Checklist>>(json);
            }
            _masterManager.Setup(x => x.GetAllChecklist()).Returns(checklists);

            //Act
            var actual = masterController.GetAllChecklist();
            int count = actual.Count;

            //Assert
            Assert.AreEqual(2,count);
        }

        /// <summary>
        /// Returns total number of checks for a particular checklist
        /// </summary>
        [Test]
        public void GetAllChecklist_ReturnsTotalNoOfChecksInAChecklist()
        {
            //Arrange
            List<Checklist> checklists;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Checklists.json");
            {
                string json = r.ReadToEnd();
                checklists = JsonConvert.DeserializeObject<List<Checklist>>(json);
            }
            _masterManager.Setup(x => x.GetAllChecklist()).Returns(checklists);

            //Act
            var actual = masterController.GetAllChecklist();
            var checklist = actual.Find(x => x.ChecklistID == 1);
            double checkscount = checklist.TotalNoOfChecks;

            //Assert
            Assert.AreEqual(2, checklist.TotalNoOfChecks);
        }
        #endregion

        #region GetAllChecks()
        /// <summary>
        /// Returns true if checklist is not null
        /// </summary>
        [Test]
        public void GetAllChecks_ReturnNotNull()
        {
            //Arrange
            List<Check> checks;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Checks.json");
            {
                string json = r.ReadToEnd();
                checks = JsonConvert.DeserializeObject<List<Check>>(json);
            }
            _masterManager.Setup(x => x.GetAllChecks()).Returns(checks);

            //Act
            var actual = masterController.GetAllChecks();

            //Assert
            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Returns the total number of checks
        /// </summary>
        [Test]
        public void GetAllChecks_ReturnsChecksCount()
        {
            //Arrange
            List<Check> checks;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Checks.json");
            {
                string json = r.ReadToEnd();
                checks = JsonConvert.DeserializeObject<List<Check>>(json);
            }
            _masterManager.Setup(x => x.GetAllChecks()).Returns(checks);

            //Act
            var actual = masterController.GetAllChecks();
            int count = actual.Count;

            //Assert
            Assert.AreEqual(4, count);
        }

        /// <summary>
        /// Returns count of checks created by a user
        /// </summary>
        [Test]
        public void GetAllChecks_ReturnCheckCountCreatedByUser()
        {
            //Arrange
            List<Check> checks;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Checks.json");
            {
                string json = r.ReadToEnd();
                checks = JsonConvert.DeserializeObject<List<Check>>(json);
            }
            _masterManager.Setup(x => x.GetAllChecks()).Returns(checks);

            //Act
            var actual = masterController.GetAllChecks();
            int count = actual.FindAll(x => x.CreatedBy == "Vinaya").Count;

            //Assert
            Assert.AreEqual(2,count);
        }
        #endregion

        #region SaveChecklist()
        /// <summary>
        /// Returns true if a checklist is saved
        /// </summary>
        [Test]
        public void SaveChecklist_SuccessfullySaves_ReturnsTrue()
        {
            //Arrange
            List<Checklist> checklists;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\NewChecklists.json");
            {
                string json = r.ReadToEnd();
                checklists = JsonConvert.DeserializeObject<List<Checklist>>(json);
            }
            _masterManager.Setup(x => x.SaveChecklist(checklists)).Returns(true);

            //Act
            var result = masterController.SaveChecklist(checklists);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Returns false if a checklist is not saved
        /// </summary>
        [Test]
        public void SaveChecklist_Unsuccessful_ReturnsFalse()
        {
            //Arrange
            List<Checklist> checklists;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\NewChecklists.json");
            {
                string json = r.ReadToEnd();
                checklists = JsonConvert.DeserializeObject<List<Checklist>>(json);
            }
            _masterManager.Setup(x => x.SaveChecklist(checklists)).Returns(false);

            //Act
            var result = masterController.SaveChecklist(checklists);

            //Assert
            Assert.IsFalse(result);
        }
        #endregion

        #region SaveChecks()
        /// <summary>
        /// Returns true if a check is saved
        /// </summary>
        [Test]
        public void SaveChecks_SuccessfullySaves_ReturnsTrue()
        {
            //Arrange
            Check check;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\NewCheck.json");
            {
                string json = r.ReadToEnd();
                check = JsonConvert.DeserializeObject<Check>(json);
            }
           // _masterManager.Setup(x => x.SaveChecks(check)).Returns(true);

            //Act
           // var result = masterController.SaveChecks(check);

            //Assert
           // Assert.IsTrue(result);
        }

        /// <summary>
        /// Returns false if a check is not saved
        /// </summary>
        [Test]
        public void SaveChecks_Unsuccessfully_ReturnsFalse()
        {
            //Arrange
            Check check;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\NewCheck.json");
            {
                string json = r.ReadToEnd();
                check = JsonConvert.DeserializeObject<Check>(json);
            }
           // _masterManager.Setup(x => x.SaveChecks(check)).Returns(false);

            //Act
           // var result = masterController.SaveChecks(check);

            //Assert
           // Assert.IsFalse(result);
        }
        #endregion

        #region GetChecklistWithChecksByChecklistID()
        /// <summary>
        /// Tests for the number of checks in a checklist got by ChecklistID
        /// </summary>
        [Test]
        public void GetChecklistWithChecksByChecklistID_ReturnsNoOfChecksInChecklist()
        {
            //Arrange
            List<ChecklistDTO> checklists;
            StreamReader r = new StreamReader(@"..\..\..\JSONFiles\Checklists.json");
            {
                string json = r.ReadToEnd();
                checklists = JsonConvert.DeserializeObject<List<ChecklistDTO>>(json);
            }
            ChecklistDTO checklistDTO = checklists.Find(x => x.ChecklistID == 1);
           // _masterManager.Setup(x => x.GetChecklistWithChecksByChecklistID(1)).Returns(checklistDTO);

            //Act
            var actual = masterController.GetChecklistWithChecksByChecklistID(1);
            var noOfChecks = actual.TotalNoOfChecks;

            //Assert
            Assert.AreEqual(2, noOfChecks);
        }
        #endregion

        #region TestMethodsTearDown
        [TearDown]
        public void TestTearDown()
        {
            _masterManager = null;
            masterController = null;
        }
        #endregion
    }
}