using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.CheckMgmt
{
    public class CheckModel_New
    {
        [Display(Name = "ID")]
        public int CheckID { get; set; }
        [Display(Name = "Code")]
        public string CheckCode { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Remote("ValidateDuplicateCheckTitle", "CheckManagement", AdditionalFields = "CheckID", ErrorMessage = "{0} already exists")]
        [Display(Name = "Title")]
        public string CheckTitle { get; set; }
        [Display(Name = "Description")]
        public string CheckDescription { get; set; }
        [Required]
        [Display(Name = "Answer")]
        public string CheckAnswer { get; set; }
        public string CheckImage { get; set; }
        [FileType("JPG,JPEG,PNG,GIF,PPM,PNM,TIFF,BMP")]
        [FileSize(10485760)]
        [Display(Name = "Image")]
        public HttpPostedFileBase CheckImageFile { get; set; }
        [Required]
        [Display(Name = "Score")]
        public string Score { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }
        [StringLength(50, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Type")]
        public string CheckType { get; set; }
        [Display(Name = "Media")]
        public string Medias { get; set; }
        public bool DeleteImage { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}