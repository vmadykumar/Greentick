using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduledJobs.RemoveDuplicates
{
   public interface IFileUtil
    {
        void RemoveUnUsedFiles(string folderPath, List<string> requiredFiles);
    }
}
