using AuditMgmt.DataLayer;
using AuditMgmt.DataLayer.DataImplementationLayer;
using AuditMgmt.DataLayer.DataInterfaceLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScheduledJobs.RemoveDuplicates;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.BusinessLayer.FileManager
{
    public class FileManager : IFileManager
    {
        //readonly IFileUtil fileUtil = null;
        //readonly IAuditRepository _auditRepository = null;
        readonly string attachmentsBasepath = null;
        //public FileManager(IFileUtil _fileUtil)
        //{
        //    fileUtil = _fileUtil;
        //}


        //attachmentsBasepath = _Configuration["FilePath"];

        public void RemovedUnNecessaryFiles()
        {
            var fileUtil = new FileUtil();
            var optionsBuilder = new DbContextOptionsBuilder<AuditContext>();
            optionsBuilder.UseSqlServer("Server=172.30.11.3\\psql;Database=GT_CR_AuditMgmt_V1_Dev;User ID =greentick ;Password=password@123;");
            var _auditRepository = new AuditRepository(new AuditContext(optionsBuilder.Options), null, null);
            var folderPath = attachmentsBasepath + "D:\\CI\\CHECK//";
            fileUtil.RemoveUnUsedFiles(folderPath, _auditRepository.GetAllAttachementNames());
        }
    }
}
