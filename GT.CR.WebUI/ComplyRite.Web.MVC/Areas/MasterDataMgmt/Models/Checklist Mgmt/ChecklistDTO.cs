using System;
using System.Collections.Generic;

namespace ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.Checklist_Mgmt
{
    public class ChecklistDTO
    {
        public int ChecklistID { get; set; }
        public string ChecklistCode { get; set; }
        public string ChecklistName { get; set; }
        public string ChecklistIcon { get; set; }
        public string ChecklistDescription { get; set; }
        public string Status { get; set; }
        public string ChecklistCategory { get; set; }
        public string ChecklistType { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<AreaChecks> AreaChecks { get; set; }
        public ChecklistDTO()
        {
            AreaChecks = new List<AreaChecks>();
        }
    }
}