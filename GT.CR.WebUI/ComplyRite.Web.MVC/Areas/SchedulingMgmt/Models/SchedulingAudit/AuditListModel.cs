using ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.SchedulingAudit
{
    public class AuditListModel
    {
        public int auditInfoID { get; set; }
        public string auditName { get; set; }
        public string auditCode { get; set; }
        public string AuditFBO { get; set; }

        public string assignedToUserName { get; set; }
        public string assignedToUserUUID { get; set; }
        public string auditLocation { get; set; }
        public string auditLocationCode { get; set; }
        public string auditCity { get; set; }
        public string auditStatus { get; set; }

        public string auditStartDate { get; set; }
        public string auditStartTime { get; set; }

        public string auditEndDate { get; set; }
        public string auditEndTime { get; set; }
        public string auditExpiryDate { get; set; }
        public string auditExpiryTime { get; set; }

        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string createdTime { get; set; }


        public DateTime auditScheduledStartDateTime
        {

            set
            {
                if (value!= null)
                {
                    DateTime dt = value.ToLocalTime();
                    this.auditStartTime = DateTime.Parse(dt.ToString()).ToString("hh:mm tt");
                    this.auditStartDate = DateTime.Parse(dt.ToString()).ToString("dd MMM yyy");
                }
            }
        }
        public DateTime auditScheduledEndDateTime
        {

            set
            {
                if (value != null)
                {
                    DateTime dt = value.ToLocalTime();
                    this.auditEndTime = DateTime.Parse(dt.ToString()).ToString("hh:mm tt");
                    this.auditEndDate = DateTime.Parse(dt.ToString()).ToString("dd MMM yyy");
                }
            }
        }

        public DateTime auditExpiryDateTime
        {

            set
            {
                if (value != null)
                {
                    DateTime dt = value.ToLocalTime();
                    this.auditExpiryTime = DateTime.Parse(dt.ToString()).ToString("hh:mm tt");
                    this.auditExpiryDate = DateTime.Parse(dt.ToString()).ToString("dd MMM yyy");
                }
            }
        }
        public DateTime createdDateTime
        {

            set
            {
                if (value != null)
                {
                    DateTime dt = value.ToLocalTime();
                    this.createdTime = DateTime.Parse(dt.ToString()).ToString("hh:mm tt");
                    this.createdDate = DateTime.Parse(dt.ToString()).ToString("dd MMM yyy");
                }
            }
        }

        public List<Checklist> checklistDetails { get; set; }
    }
}