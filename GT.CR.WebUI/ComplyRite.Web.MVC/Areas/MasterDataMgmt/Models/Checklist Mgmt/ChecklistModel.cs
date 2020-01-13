using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.ChecklistMgmt
{
    public class ChecklistModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]    
        public string Description { get; set; }

        [Display(Name = "No. Of Checks")]
        public string ChecksCount { get; set; }

        [Display(Name = "No. Of Checks")]
        public string ChecksMaxScore { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Score")]
        public int Score { get; set; }

        [Display(Name = "Check Type")]
        public string CheckType { get; set; }

        [Display(Name = "Correct Response")]
        public string CorrectResponse { get; set; }

        [Display(Name = "Added By")]
        public string AddedBy { get; set; }

        [Display(Name = "Added On")]
        public string AddedOn { get; set; }

        [Display(Name = "Applicable From")]
        public string ApplicableFrom { get; set; }

        [Display(Name = "Description")]
        public string TutorialDesc { get; set; }
        public string Reason { get; set; }

        public string InactivationReason { get; set; }
        public string Status { get; set; }
    }
}