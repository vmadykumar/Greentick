using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.ReportMgmt.Models
{
    public class Section
    {
        public Section()
        {
            AuditAreas = new List<AuditArea>();
        }
        public string SectionName { get; set; }
        public List<AuditArea> AuditAreas { get; set; }

        public float Round(double value)
        {
            double decimalpoints = Math.Abs(value - Math.Floor(value));
            if (decimalpoints >= 0.5)
                return (float)Math.Round(value);
            else
                return (float)Math.Floor(value);
        }

        public float RoundDecimal(decimal val)
        {
            decimal decimalVal = decimal.Round(val, 1, MidpointRounding.AwayFromZero);
            return (float)decimal.Round(decimalVal, MidpointRounding.AwayFromZero);
        }
    }
}