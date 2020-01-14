using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMgmt.CommonLayer.Models.DTO
{
    public class AuditCheckViewModel
    {
        public int CheckID { get; set; }
        public string CheckCode { get; set; }
    }

    public class AuditChecklistViewModel
    {
        public AuditChecklistViewModel()
        {
           //AuditCheckDto = new List<AuditCheckViewModel>();
        }
        public int ChecklistID { get; set; }
        public string ChecklistCode { get; set; }
        public DateTime checklistScheduledStartDateTime { get; set; }
        public DateTime checklistScheduledEndDateTime { get; set; }
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
        public string SubAreaName { get; set; }
        public string SubAreaCode { get; set; }
       //public List<AuditCheckViewModel> AuditCheckDto { get; set; }
    }

    public class ScheduleDate
    {
        public DateTime ScheduleStartDateTime { get; set; }
        public DateTime ScheduleEndDateTime { get; set; }
        public DateTime ScheduleExpiryDateTime { get; set; }
        public DateTime ScheduleRepeatEndDateTime { get; set; }
    }

    public class AuditViewModel
    {
        public AuditViewModel()
        {
            AuditChecklistDto = new List<AuditChecklistViewModel>();
            //ScheduleDates = new List<ScheduleDate>();
        }
        public int AuditID { get; set; }
        public int AuditInfoID { get; set; }
        public string AuditName { get; set; }
        public string AuditImage { get; set; }
        public string AuditCode { get; set; }
        public string AssignedToUUID { get; set; }
        public string AuditLocation { get; set; } //Name
        public string AuditFBO { get; set; } //Account Alias
        public string FBOCode { get; set; } // LOB Code
        public string AuditLocationCode { get; set; }
        public string AuditCity { get; set; } // 
        public string AssignedToUserUUID { get; set; }
        public string AssignedToUserName { get; set; }

        public DateTime? AuditScheduledStartDateTime { get; set; }
        public DateTime? AuditScheduledEndDateTime { get; set; }
        public DateTime? AuditExpiryDateTime { get; set; }
        public string AuditType { get; set; } //External / Internal
        public string AuditCategory { get; set; } //External / Internal
        public string AuditStatus { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public List<AuditChecklistViewModel> AuditChecklistDto { get; set; }
        //public List<ScheduleDate> ScheduleDates { get; set; }
    }

 
   
}
