using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ComplyRite.Web.MVC.Areas.MasterDataMgmt.Models.CheckMgmt
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileSizeAttribute : ValidationAttribute, IClientValidatable
    {
        public int? MaxBytes { get; set; }

        public FileSizeAttribute(int maxBytes)
            : base("Please upload a supported file.")
        {
            MaxBytes = maxBytes;
            if (MaxBytes.HasValue)
            {
                ErrorMessage = "Please upload a file of less than " + ConvertBytesToMegabytes(MaxBytes.Value) + " MB.";
            }
        }

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                bool result = true;

                if (MaxBytes.HasValue)
                {
                    result &= (file.ContentLength < MaxBytes.Value);
                }

                return result;
            }

            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metaData, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "filesize",
                ErrorMessage = FormatErrorMessage(metaData.DisplayName)
            };
            rule.ValidationParameters["maxbytes"] = MaxBytes;
            yield return rule;
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}