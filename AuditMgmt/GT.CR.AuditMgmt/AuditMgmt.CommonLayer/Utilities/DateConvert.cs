using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.Utilities
{
    public static class DateConvert
    {
        public static DateTime GetLocalDatefromUTC(this DateTime date, string timeZone = "India Standard Time") => TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        public static string GetLocalShortDatefromUTC(this DateTime date, string timeZone = "India Standard Time") => date.GetLocalDatefromUTC(timeZone).ToShortDateString();
    }
}
