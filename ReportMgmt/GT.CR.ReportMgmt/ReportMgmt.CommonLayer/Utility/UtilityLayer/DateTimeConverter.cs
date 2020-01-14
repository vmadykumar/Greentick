using System;
using System.Collections.Generic;
using System.Text;

namespace ReportMgmt.CommonLayer.Utility.UtilityLayer
{
   public static class DateTimeConverter
    {
        public static DateTime GetLocalDatefromUTC(this DateTime date, string timeZone = "India Standard Time") => TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
    }
}
