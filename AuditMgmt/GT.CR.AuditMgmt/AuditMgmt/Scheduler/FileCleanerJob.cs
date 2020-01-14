using AuditMgmt.BusinessLayer.FileManager;
using Quartz;
using ScheduledJobs.RemoveDuplicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditMgmt.Scheduler
{
    public class FileCleanerJob : IJob
    {
        IFileManager fileManager;
        public FileCleanerJob(IFileManager _fileManager)
        {
            fileManager = _fileManager;
        }
        public FileCleanerJob()
        {
            fileManager = new FileManager();
        }
        public Task Execute(IJobExecutionContext context)
        {
            fileManager.RemovedUnNecessaryFiles();
            return Task.FromResult(0);
        }
    }
}
