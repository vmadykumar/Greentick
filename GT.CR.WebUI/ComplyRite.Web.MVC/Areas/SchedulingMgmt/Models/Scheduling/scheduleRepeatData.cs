using ComplyRite.Web.MVC.Helpers;
using MasterMgmt.CommonLayer.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling
{
    public class ScheduleRepeatData
    {
        public string RepeatType { get; set; }
        public DateTime StartDateTime{ get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime ExpiryDateTime { get; set; }
        public DateTime RepeatEndDateTime { get; set; }

        public List<AuditViewModel> GetRepeatingSchedules(AuditViewModel schedule, ScheduleRepeatData repeatData)
        {
            List<AuditViewModel> schedules = new List<AuditViewModel>();
            List<ScheduleDate> scheduleDates = repeatData.GetRepeatingDatesForScheduling();
            foreach (var scheduleDate in scheduleDates)
            {
                AuditViewModel newSchedule = Utility.CloneObject(schedule) as AuditViewModel;
                newSchedule.AuditScheduledStartDateTime = scheduleDate.ScheduleStartDateTime;
                newSchedule.AuditScheduledEndDateTime = scheduleDate.ScheduleRepeatEndDateTime;
                newSchedule.AuditExpiryDateTime = scheduleDate.ScheduleExpiryDateTime;
                schedules.Add(newSchedule);
            }
            return schedules;
        }

        private int IsDateValidForRepeat(DateTime startDateTime, DateTime repeatEndDateTime)
        {
            return DateTime.Compare(startDateTime, repeatEndDateTime);
        }

        private List<ScheduleDate> GetRepeatingDatesForScheduling()
        {
            List<ScheduleDate> scheduleDates = new List<ScheduleDate>();
            switch (RepeatType)
            {
                case "daily": scheduleDates = GetDatesForDaily(); break;
                case "weekdays": scheduleDates = GetDatesForWeekdays(); break;
                case "weekly": scheduleDates = GetDatesForWeekly(); break;
                case "monthly": scheduleDates = GetDatesForMonthly(); break;
                case "annualy": scheduleDates = GetDatesForAnnualy(); break;
                case "firstDay": scheduleDates = GetDatesForFirstDay(); break;
                case "LastDay": scheduleDates = GetDatesForLastDay(); break;
                default:
                    break;
            }
            return scheduleDates;
        }

        private List<ScheduleDate> GetDatesForDaily()
        {
            List<ScheduleDate> scheduleDates = new List<ScheduleDate>();
            TimeSpan scheduleDuration = EndDateTime - StartDateTime;
            TimeSpan expiryDuration = ExpiryDateTime - EndDateTime;

            while (IsDateValidForRepeat(StartDateTime, RepeatEndDateTime) < 1)
            {
                scheduleDates.Add(new ScheduleDate
                {
                    ScheduleStartDateTime = StartDateTime,
                    ScheduleEndDateTime = EndDateTime,
                    ScheduleExpiryDateTime = ExpiryDateTime
                });
                StartDateTime = StartDateTime.AddDays(1);
                EndDateTime = StartDateTime.Add(scheduleDuration);
                ExpiryDateTime = EndDateTime.Add(expiryDuration);
            } 
            return scheduleDates;
        }
        private List<ScheduleDate> GetDatesForWeekdays()
        {
            List<ScheduleDate> scheduleDates = new List<ScheduleDate>();
            TimeSpan scheduleDuration = EndDateTime - StartDateTime;
            TimeSpan expiryDuration = ExpiryDateTime - EndDateTime;

            while (IsDateValidForRepeat(StartDateTime, RepeatEndDateTime) < 1)
            {
                if(!(StartDateTime.DayOfWeek == DayOfWeek.Saturday || StartDateTime.DayOfWeek == DayOfWeek.Sunday))
                {
                    scheduleDates.Add(new ScheduleDate
                    {
                        ScheduleStartDateTime = StartDateTime,
                        ScheduleEndDateTime = EndDateTime,
                        ScheduleExpiryDateTime = ExpiryDateTime
                    });
                }
                StartDateTime = StartDateTime.AddDays(1);
                EndDateTime = StartDateTime.Add(scheduleDuration);
                ExpiryDateTime = EndDateTime.Add(expiryDuration);
            }
            return scheduleDates;
        }
        private List<ScheduleDate> GetDatesForWeekly()
        {
            throw new NotImplementedException();
        }
        private List<ScheduleDate> GetDatesForMonthly()
        {
            throw new NotImplementedException();
        }
        private List<ScheduleDate> GetDatesForAnnualy()
        {
            throw new NotImplementedException();
        }
        private List<ScheduleDate> GetDatesForFirstDay()
        {
            throw new NotImplementedException();
        }
        private List<ScheduleDate> GetDatesForLastDay()
        {
            throw new NotImplementedException();
        }
                                          
    }
}