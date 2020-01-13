using ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.CheckMgmt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.Checklist_Mgmt
{
    public class ChecklistModel_New
    {
        public int ChecklistID { get; set; }
        [Display(Name = "Code")]
        public string ChecklistCode { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Remote("ValidateDuplicateChecklistTitle", "ChecklistManagement", AdditionalFields = "ChecklistID,SubAreaCode", ErrorMessage = "{0} already exists")]
        [Display(Name = "Name")]     
        public string ChecklistName { get; set; }
        public string ChecklistIcon { get; set; }
        [FileType("JPG,JPEG,PNG,GIF,PPM,PNM,TIFF,BMP")]
        [FileSize(10485760)]
        [Display(Name = "Icon")]
        public HttpPostedFileBase ChecklistIconFile { get; set; }
        [Display(Name = "Description")]
        public string ChecklistDescription { get; set; }
        private string total;
        [Display(Name = "Total Checks")]
        public string TotalNoOfChecks
        {
            get { return total; }
            set { total = value.IndexOf(".") == -1 ? value : value.Substring(0, value.IndexOf(".")); }
        }
        [Display(Name = "Max Score")]
        public string MaxScore { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Category")]
        public string ChecklistCategory { get; set; }
        [Display(Name = "Type")]
        [StringLength(50, ErrorMessage = "{0} can have a maximum of {1} characters")]
        public string ChecklistType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        [Display(Name = "Checks")]
        public List<CheckModel_New> Checks { get; set; }

        [Required]
       // [Remote("ValidateDuplicateChecklistChecks", "ChecklistManagement", AdditionalFields = "SubAreaCode,ChecklistID", ErrorMessage = "{0} already exists")]
        [Display(Name = "Checks")]
        public int[] SelectedChecks { get; set; }
       
        [Display(Name = "Location")]
        public string LocationName { get; set; }
        
        [Display(Name = "Area")]
        public string AreaName { get; set; }
       
        [Display(Name = "Sub Area")]
        public string SubAreaName { get; set; }
        [Required]
        public string SubAreaCode { get; set; }
        [Required]
        public string AreaCode { get; set; }
        [Required]
        public string LocationCode { get; set; }
        public bool DeleteImage { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}