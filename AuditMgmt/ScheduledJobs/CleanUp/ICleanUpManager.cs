using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduledJobs.CleanUp
{
    interface ICleanUpManager
    {
        void RemoveArchivedFiles();
    }
}
