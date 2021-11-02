using System;
using System.Linq;

namespace SchedulerNegocio
{
    public class ScheduleCalculator
    {
        private readonly ScheduleConfiguration Configuration;
        public DateTime? Next_Execution_Time { get; set; }
        public string Description_Execution { get; set; }
        private const int DAYS_FOR_WEEK = 7;
        private const int DAY = 1;

        public ScheduleCalculator(ScheduleConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void GetResult()
        {
            DateTime? NextDate;

            this.CheckConfiguration();
            if (this.Configuration.Type == (int)Types.Once)
            {
                NextDate = this.Configuration.Configuration_date;
            }
            else
            {
                this.CheckFrecuency();
                NextDate = this.GetNextDateRecurring();
            }
            this.ValidateDateLimits(NextDate);

            this.Next_Execution_Time = NextDate;
            this.Description_Execution = this.GetDescriptionExecution();
        }

        private DateTime? GetNextDateRecurring()
        {
            DateTime? NextDate = Configuration.Current_date;
            if (this.Configuration.Frecuency == Frecuencys.Daily.ToString())
            {
                NextDate = NextDate.Value.AddDays(DAY);
            }
            else if (this.Configuration.Frecuency == Frecuencys.Weekly.ToString())
            {
                NextDate = this.GetNextDateWeekly(NextDate);
            }
            NextDate = this.GetNextDateTimeDailyFrecuency(NextDate);
            return NextDate;
        }

        private DateTime? GetNextDateTimeDailyFrecuency(DateTime? nextDailyDate)
        {
            if (this.Configuration.Daily_frecuency ==
                DailyFrencuencys.OnceTime.ToString())
            {
                TimeSpan? TheTimeOnce = this.Configuration.Time_once_frecuency;
                return nextDailyDate.Value.AddHours(TheTimeOnce.Value.Hours)
                                          .AddMinutes(TheTimeOnce.Value.Minutes)
                                          .AddSeconds(TheTimeOnce.Value.Seconds);
            }
            else
            {
                return this.AddHourlyFrecuency(nextDailyDate);
            }
        }

        private DateTime? AddHourlyFrecuency(DateTime? nextDateHourly)
        {
            nextDateHourly = nextDateHourly.Value
                .AddHours(this.Configuration.Star_time.Hours)
                .AddMinutes(this.Configuration.Star_time.Minutes)
                .AddSeconds(this.Configuration.Star_time.Seconds);

            if (this.Configuration.Time_type == TimeTypes.Hours.ToString())
            {
                return nextDateHourly.Value.AddHours(this.Configuration.Time_frecuency);
            }
            else if (this.Configuration.Time_type == TimeTypes.Minutes.ToString())
            {
                return nextDateHourly.Value.AddMinutes(this.Configuration.Time_frecuency);
            }
            else
            {
                return nextDateHourly.Value.AddSeconds(this.Configuration.Time_frecuency);
            }
        }

        private DateTime? GetNextDateWeekly(DateTime? nextDate)
        {
            DayOfWeek NextDayOfWeek;
            DateTime? NextDateWeekly = nextDate;
            DayOfWeek ActualDayOfWeek = (NextDateWeekly.Value.DayOfWeek);

            bool isLocatedNext = false;
            NextDayOfWeek = ActualDayOfWeek;

            foreach (DayOfWeek ForDay in this.Configuration.Days_active_week)
            {
                if (ForDay > ActualDayOfWeek && isLocatedNext == false)
                {
                    NextDayOfWeek = ForDay;
                    isLocatedNext = true;
                }
            }

            if (isLocatedNext == false)
            {
                NextDayOfWeek = this.Configuration.Days_active_week.Min();
                NextDateWeekly = this.AddDaysForWeeksToCurrent(nextDate);
            }

            int daysToAdd = (((int)NextDayOfWeek) - ((int)ActualDayOfWeek)) % 7;
            return NextDateWeekly.Value.AddDays(daysToAdd);
        }

        public DateTime? AddDaysForWeeksToCurrent(DateTime? startDate)
        {
            int TotalDaysToAdd = this.Configuration.Frecuency_weeks * DAYS_FOR_WEEK;
            return startDate.Value.AddDays(TotalDaysToAdd);
        }

        private string GetDescriptionExecution()
        {
            string DescriptionFrecuency = string.Empty;
            if (this.Configuration.Frecuency != null)
            {
                DescriptionFrecuency = this.Configuration.Frecuency == Frecuencys.Daily.ToString()
                  ? "Ocurrs every day. "
                  : "Ocurrs every " + this.Configuration.Frecuency_weeks + " weeks on "
                      + string.Join(" - ", this.Configuration.Days_active_week) + ". ";
            }
            string DescriptionOcurrs = this.Configuration.Type == (int)Types.Once
                ? "Ocurrs once. " : DescriptionFrecuency;

            string Time;
            if (this.Configuration.Time_type == TimeTypes.Hours.ToString())
            {
                Time = " hours ";
            }
            else if (this.Configuration.Time_type == TimeTypes.Minutes.ToString())
            {
                Time = " minutes ";
            }
            else
            {
                Time = " seconds ";
            }

            string DescriptionTime = (this.Configuration.Type == (int)Types.Once
                    || this.Configuration.Daily_frecuency == DailyFrencuencys.OnceTime.ToString())
                ? " at " + this.Next_Execution_Time.Value.ToLongTimeString()
                : " every " + this.Configuration.Time_frecuency + Time + "between "
                + this.Configuration.Star_time + " and " + this.Configuration.End_time;

            return DescriptionOcurrs +
                   "Schedule will be used on " + this.Next_Execution_Time.Value.ToShortDateString() +
                  DescriptionTime +
                   " starting on " + this.Configuration.Start_date.Value.ToShortDateString();
        }

        public void CheckConfiguration()
        {
            if (this.Configuration.Active == false)
            {
                throw new Exception("Configuration must be active");
            }

            if (this.Configuration.Type != (int)Types.Recurring
               && this.Configuration.Type != (int)Types.Once)
            {
                throw new Exception("The type is not recognized");
            }

            if (this.Configuration.Type == (int)Types.Recurring
              && this.Configuration.Current_date.HasValue == false)
            {
                throw new Exception("Current date can not be empty");
            }

            if (this.Configuration.Type == (int)Types.Once
               && this.Configuration.Configuration_date.HasValue == false)
            {
                throw new Exception("Configuration date can not be empty");
            }

            if (this.Configuration.Start_date.HasValue == false)
            {
                throw new Exception("Start date can not be empty");
            }
        }


        public void CheckFrecuency()
        {

            if (this.Configuration.Frecuency != Frecuencys.Daily.ToString()
                    && this.Configuration.Frecuency != Frecuencys.Weekly.ToString())
            {
                throw new Exception("The frequency is not set correctly");
            }
            if (this.Configuration.Daily_frecuency == null)
            {
                throw new Exception("Must to select a daily frequency type 'Once' or 'Every'");
            }
            if (this.Configuration.Frecuency == Frecuencys.Weekly.ToString())
            {
                if (this.Configuration.Frecuency_weeks <= 0)
                {
                    throw new Exception("The weekly frequency must be greater than 0");
                }

                if (this.Configuration.Days_active_week == null
                    || this.Configuration.Days_active_week.Length == 0)
                {
                    throw new Exception("You must select at least one day of the week");
                }
            }
            if (this.Configuration.Daily_frecuency == DailyFrencuencys.EveryTime.ToString())
            {
                if (this.Configuration.Time_type != TimeTypes.Hours.ToString()
                    && this.Configuration.Time_type != TimeTypes.Minutes.ToString()
                      && this.Configuration.Time_type != TimeTypes.Seconds.ToString())
                {
                    throw new Exception("Must indicate the type of frecuency Hours, Minutes o Seconds correctly");
                }
                if (this.Configuration.Time_frecuency <= 0)
                {
                    throw new Exception("The hourly frequency must be greater than 0");
                }

                this.ValidateTimeLimits();
            }
        }

        public void ValidateDateLimits(DateTime? dateValidate)
        {
            if (this.Configuration.Start_date > dateValidate
              || (this.Configuration.End_date.HasValue
                  && this.Configuration.End_date.Value < dateValidate.Value.Date))
            {
                throw new Exception("Next execution time exceeds date limits");
            }

        }

        public void ValidateTimeLimits()
        {
            DateTime StartTime = new();
            StartTime = StartTime.AddHours(this.Configuration.Star_time.Hours)
                .AddMinutes(this.Configuration.Star_time.Minutes)
                .AddSeconds(this.Configuration.Star_time.Seconds);

            DateTime EndTime = new();
            EndTime = EndTime.AddHours(this.Configuration.End_time.Hours)
                .AddMinutes(this.Configuration.End_time.Minutes)
                .AddSeconds(this.Configuration.End_time.Seconds);

            if (this.Configuration.Time_type == TimeTypes.Hours.ToString())
            {
                StartTime = StartTime.AddHours(this.Configuration.Time_frecuency);
                if (StartTime > EndTime)
                {
                    throw new Exception("The Horary Range is not set correctly");
                }
            }
            else if (this.Configuration.Time_type == TimeTypes.Minutes.ToString())
            {
                StartTime = StartTime.AddMinutes(this.Configuration.Time_frecuency);
                if (StartTime > EndTime)
                {
                    throw new Exception("The Horary Range is not set correctly");
                }
            }
            else
            {
                StartTime = StartTime.AddSeconds(this.Configuration.Time_frecuency);
                if (StartTime > EndTime)
                {
                    throw new Exception("The Horary Range is not set correctly");
                }

            }
        }
    }
}