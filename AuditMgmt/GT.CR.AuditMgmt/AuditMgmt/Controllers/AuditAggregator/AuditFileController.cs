///-----------------------------------------------------------------
///   Namespace:      AuditMgmt.Controllers
///   Class:         AuditController
///   Description:    Controller for Audit data
///   Author:        Keshav M                   Date: 21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------
using AuditMgmt.CommonLayer.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace AuditMgmt.Controllers.AuditAggregator
{
    public partial class AuditController : Controller
    {
        private const string lockObj = "lockObj";
        #region FileManagement

        /// <summary>
        /// Uploads a file
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            var files = Request.Form.Files;
           
            string filesNames = "";
            foreach (var file in files) // Looping inside each file               
            {
                var folderPath = _configuration["UploadFilePath"].ToString();
                string filename = file.FileName;
                var path = Path.Combine(folderPath,filename); // Combines filepath and filename to get the path for the target file
               
                if (System.IO.File.Exists(path))
                {
                    filename = string.Concat(DateTime.Now.ToString(), "_",filename);
                    path = Path.Combine(folderPath,filename);

                }
                filesNames = string.Concat(filesNames, filename ,",");

                using (var stream = new FileStream(path, FileMode.Create)) // Creates a new target file using the path 
                {

                    await file.CopyToAsync(stream); //Copies the uploaded file content to the target file
                }
            }
            filesNames = filesNames.Substring(0, filesNames.Length - 1);
            return files.Count != 0 ? Content( filesNames +" are Uploaded Successfully") : Content("File not selected"); // If file count is not zero, display file added successfully otherwise display file not selected
        }


        /// <summary>
        /// Download   image based on the required Size
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ImageType"></param>
        /// <param name="width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DownloadResizedImage(string filename, string ImageType, int width = 120, int Height = 120)
        {
            try
            {
                var newPath = _configuration["FilePath"] + width.ToString() + Height.ToString() + filename;
                var path = Path.Combine(_configuration["FilePath"] + ImageType?.ToUpper() + @"\\" + filename); // Combines filepath and filename to get the path for the target file

                if (!System.IO.File.Exists(path))
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources//Images//Nocontent.png");
                    newPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources//Images//" + width.ToString() + Height.ToString() + "Nocontent.png");

                }
                lock (lockObj)
                {
                    if (!System.IO.File.Exists(newPath))
                        using (var image = Image.FromFile(path))
                        {
                            using (var t = image.GetThumbnailImage(width, Height, () => false, IntPtr.Zero))
                            {
                                t.Save(newPath);
                                t.Dispose();
                            }
                            image.Dispose();
                        }
                }
                return File(System.IO.File.ReadAllBytes(newPath), "image/jpeg");
            }
            catch (FileNotFoundException)
            {
                return StatusCode(404);
            }

        }


        /// <summary>
        /// Download Image with the Type
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ImageType"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DownloadImage(string filename, string ImageType)
        {
            try
            {
                if (string.IsNullOrEmpty(ImageType))
                {
                    return File(System.IO.File.ReadAllBytes(_configuration["FilePath"] + filename), "image/jpeg");
                }
                else
                {
                    return File(System.IO.File.ReadAllBytes(_configuration["FilePath"] + ImageType.ToUpper() + @"\\" + filename), "image/jpeg");
                }
            }
            catch (FileNotFoundException)
            {
                return StatusCode(404);
            }

        }
        /// <summary>
        /// Downloads a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Download(string filename)
        {
            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes(_configuration["FilePath"].ToString() + filename);
                return File(bytes, "image/jpeg");
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Image not Found");
            }
        }
        #endregion


        /// <summary>
        /// Upload the data for Adding Master Checklist
        /// </summary>
        /// <param name="uploadedFile"></param>
        [HttpPost]
        public void UploadExcel(IFormFile uploadedFile)
        {
            #region Save File Local
            //Create new filename if file Exists and save file Locally
            if (uploadedFile == null)
                throw new FileNotFoundException("File not received to the server.\n Please check and try again.\n if this error persists contact adminstrator");

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, uploadedFile.FileName);
            if (System.IO.File.Exists(filePath))
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMddHHmmssfff") + uploadedFile.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }
            #endregion
            loadData.toTable(filePath, getSheetMappings("ScheduleData"));
        }

        /// <summary>
        /// Creates Mapping from Excel Scheet to Table to be Inserted in DB
        /// </summary>
        /// <param name="MappingKey">to choose between various file</param>
        /// <returns> Sheet Mappings</returns> 
        [HttpPost]
        private List<SheetInfo> getSheetMappings(string MappingKey)
        {
            List<SheetInfo> sheetMappings = new List<SheetInfo>();
            sheetMappings.Add(new SheetInfo() { SheetNumber = 0, TableName = "P_MasterChecklist", Procedure = "SP_LoadChecklist" });
            return sheetMappings;
        }

        /// <summary>
        /// Deletes the attatchment with name
        /// </summary>
        /// <param name="filename"></param>
        [HttpPost]
        public void DeleteAttachement(string filename)
        {
            _auditManager.DeleteAttachement(filename);
        }

        /// <summary>
        /// Add Master checklists for a subarea
        /// </summary>
        /// <param name="uploadedFile"></param>
        [HttpPost]
        public void UploadChecklistWithSubAreaCodes(IFormFile uploadedFile)
        {

            #region Save File Local
            //Create new filename if file Exists and save file Locally
            if (uploadedFile == null)
                throw new FileNotFoundException("File not received to the server.\n Please check and try again.\n if this error persists contact adminstrator");

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, uploadedFile.FileName);
            if (System.IO.File.Exists(filePath))
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMddHHmmssfff") + uploadedFile.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }
            #endregion
            List<SheetInfo> sheetMappings = new List<SheetInfo>();
            sheetMappings.Add(new SheetInfo() { SheetNumber = 0, TableName = "P_MasterChecklist", Procedure = "Sp_AddMasterChecklistandChecks" });
            loadData.toTable(filePath, sheetMappings);
        }

        /// <summary>
        /// Returns the count of all states of audit for manager
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="LOBCode"></param>
        /// <param name="BMCode"></param>
        /// <param name="TeamName"></param>
        /// <returns></returns>
        [HttpPost]
       
        public List<ReportDto> GetAuditSummaryReport(string userID, string LOBCode, string BMCode, string TeamName = "SAVE")
        {
            return _auditManager.GetAuditSummaryReport(userID, LOBCode, BMCode, TeamName);

        }

        /// <summary>
        /// Backup File When User session Ends
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <param name="backUpData"></param>
        [HttpPost]
        public void BackUpUsersData(IFormFile uploadedFile, BackUpDto backUpData)
        {
            string filePath = null;
            if (uploadedFile == null)
            {
                throw new FileNotFoundException("File not received to the server.\n Please check and try again.\n if this error persists contact adminstrator");
            }
            try
            {
                var directory = (_configuration["FilePath"].ToString() + backUpData.LobCode.ToUpper() + "\\" + backUpData.LocationCode.ToUpper() + "\\" + backUpData.UUID.ToUpper() + "\\" + backUpData.Type.ToUpper());
                Directory.CreateDirectory(directory);
                filePath = Path.Combine(directory, uploadedFile.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }
            }
            catch (Exception e)
            {

                throw new Exception(filePath,e);
            }
        }
    }
}

