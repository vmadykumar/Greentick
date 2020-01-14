using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace ScheduledJobs.RemoveDuplicates
{
    public class FileUtil : IFileUtil
    { 
        List<string> findFilesNotInUse(string folderPath, List<string> requiredFiles)
        {
            DirectoryInfo dir = new DirectoryInfo(folderPath); 
            var filesNotRequired = dir.GetFiles().Where(f => !requiredFiles.Contains(f.Name)).Select(f => f.FullName).ToList();
            return filesNotRequired;
        } 

        public void RemoveUnUsedFiles(string folderPath,List<string> requiredFiles)
        {
            List<string> unUsedFiles = findFilesNotInUse(folderPath, requiredFiles);
            unUsedFiles.ForEach(f => File.Delete(f));
        }
    }
}
