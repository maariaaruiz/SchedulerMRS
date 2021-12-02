using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerNegocio
{
    public class ScheduleCalculator
    {
        private const int DAYS_FOR_WEEK = 7;
        private const int DAY = 1;

        public DateTime? GetResult(ScheduleConfiguration configuration)
        {
            this.ValidateConfiguration(configuration);

            if (configuration.Active == false)
            {
                throw new InvalidOperationException("Configuration must be active");
            }         
           
            DateTime? nextDate = configuration.Ocurrs_type switch
            {
                Types.Once => this.GetNextDateOnce(configuration),
                Types.Recurring => this.GetNextDateRecurring(configuration),
                _ => throw new InvalidOperationException("The type is not recognized"),
            };
            if (nextDate != null)
            {
                configuration.Last_execution = nextDate;
            }
            return nextDate;
        }

        private DateTime? GetNextDateOnce(ScheduleConfiguration configuration)
        {
            DateTime? nextDate = configuration.Configuration_date;
            if (nextDate.HasValue == false)
            {
                throw new InvalidOperationException("Configuration date can not be empty");
            }

            this.ValidateDateLimits(configuration);

            if (configuration.Start_date > nextDate
              || (configuration.End_date.HasValue
                  && configuration.End_date.Value < nextDate.Value.Date))
            {
                throw new InvalidOperationException("Next execution time exceeds date limits");
            }
            return nextDate;
        }

        private DateTime? GetNextDateRecurring(ScheduleConfiguration configuration)
        {
            DateTime? nextDate = configuration.Last_execution == null
                ? configuration.Current_date
                : configuration.Last_execution;

            if (nextDate == null)
            {
                throw new InvalidOperationException("Current date can not be empty");
            }

            this.ValidateDateLimits(configuration);

            return this.GetNextDateTimeFrecuency(configuration, nextDate);
        }

        private DateTime? GetNextDateTimeFrecuency(ScheduleConfiguration configuration, DateTime? nextDate)
        {
            if (configuration.Frecuency != Frecuencys.Daily &&
                configuration.Frecuency != Frecuencys.Weekly)
            {
                throw new InvalidOperationException("The frequency is not set correctly");
            }
            if (configuration.Frecuency == Frecuencys.Weekly)
            {
                return this.GetNextDateWeekly(configuration, nextDate);
            }
            else if (configuration.Frecuency == Frecuencys.Monthly)
            {
                return this.GetNextDateMonthly(configuration, nextDate);
            }
            return this.GetNextDateDaily(configuration, nextDate);
        }

        public DateTime? GetNextDateDaily(ScheduleConfiguration configuration, DateTime? nextDate)
        {
            DateTime? nextDailyDate = nextDate.Value.Date;
            TimeSpan? timeToAdd = this.GetTimeToAdd(configuration, nextDate);

            //si time to add se pasa del rango horario
            if (!this.InHorary(configuration, timeToAdd)
                && configuration.Daily_frecuency != DailyFrencuencys.OnceTime)
            {
                nextDailyDate = nextDailyDate.Value.AddDays(DAY);
                if (this.InLimitsDate(configuration, nextDailyDate))
                {
                    return nextDailyDate.Value.Add(configuration.Star_time);
                }
                return null;
            }

            //si no se pasa del rango
            nextDailyDate = nextDailyDate.Value.Add(timeToAdd.Value);
            if (configuration.Last_execution != null && configuration.Daily_frecuency == DailyFrencuencys.OnceTime)
            {
                nextDailyDate = nextDailyDate.Value.AddDays(DAY);
            }

            if (this.InLimitsDate(configuration, nextDailyDate))
            {
                return nextDailyDate;
            }
            return null;
        }

        private TimeSpan? GetTimeToAdd(ScheduleConfiguration configuration, DateTime? nextDailyDate)
        {
            TimeSpan? theTime;
            if (configuration.Daily_frecuency == DailyFrencuencys.OnceTime)
            {
                theTime = configuration.Time_once_frecuency;
                if (theTime == null)
                {
                    throw new InvalidOperationException("Must be indicate the daily time");
                }
                return theTime.Value;
            }
            else if (configuration.Daily_frecuency == DailyFrencuencys.EveryTime)
            {
                return this.AddHourlyFrecuency(configuration, nextDailyDate);
            }
            else
            {
                throw new InvalidOperationException("Must to select a daily frequency type 'Once' or 'Every'");
            }
        }

        private TimeSpan? AddHourlyFrecuency(ScheduleConfiguration configuration, DateTime? nextDate)
        {
            this.CheckHourlyConfiguration(configuration);

            TimeSpan? nextTime = configuration.Last_execution == null
                ? configuration.Star_time
                : configuration.Last_execution.Value.TimeOfDay;

            DateTime? initialDateTime = nextDate.Value.Date.Add(nextTime.Value);
            if (configuration.Last_execution == null)
            {
                return initialDateTime.Value.TimeOfDay;
            }

            nextDate = configuration.Time_type switch
            {
                TimeTypes.Hours => initialDateTime.Value.AddHours(configuration.Time_frecuency),
                TimeTypes.Minutes => initialDateTime.Value.AddMinutes(configuration.Time_frecuency),
                _ => initialDateTime.Value.AddSeconds(configuration.Time_frecuency)
            };
            return nextDate.Value.TimeOfDay;
        }

        private void CheckHourlyConfiguration(ScheduleConfiguration configuration)
        {
            if (configuration.Time_frecuency <= 0)
            {
                throw new InvalidOperationException("The hourly frequency must be greater than 0");
            }
            if (configuration.Time_type != TimeTypes.Hours &&
                configuration.Time_type != TimeTypes.Minutes &&
                configuration.Time_type != TimeTypes.Seconds)
            {
                throw new InvalidOperationException("Must indicate the type of frecuency Hours, Minutes o Seconds correctly");
            }
            if (configuration.Star_time >= configuration.End_time
                || configuration.Star_time == null
                || configuration.End_time == null)
            {
                throw new InvalidOperationException("The Horary Range is not set correctly");
            }
        }

        private DateTime? GetNextDateWeekly(ScheduleConfiguration configuration, DateTime? nextDate)
        {
            this.CheckWeeklyConfiguration(configuration);

            TimeSpan? timeToAdd = this.GetTimeToAdd(configuration, nextDate.Value.Date);
            DateTime? NextWeeklyDate = nextDate.Value.Date.Add(timeToAdd.Value);
            NextWeeklyDate = this.CalculateNextDateWeekly(configuration, NextWeeklyDate);

            if (this.InLimitsDate(configuration, NextWeeklyDate))
            {
                return NextWeeklyDate;
            }
            return null;
        }

        private DateTime? CalculateNextDateWeekly(ScheduleConfiguration configuration, DateTime? NextDailyDate)
        {
            DayOfWeek actualDayOfWeek = (NextDailyDate.Value.DayOfWeek);

            if (this.InHorary(configuration, NextDailyDate.Value.TimeOfDay))
            {
                if (configuration.Days_active_week.Contains(actualDayOfWeek))
                {
                    return NextDailyDate;
                }
            }

            bool isLocatedNext = false;
            int day = 0;
            DayOfWeek[] activeDays = configuration.Days_active_week.OrderBy(D => this.GetNumberDayOfWeek(D)).ToArray();

            DateTime? NextWeeklyDate = NextDailyDate;
            if (configuration.Daily_frecuency == DailyFrencuencys.EveryTime)
            {
                DateTime? newDate = NextDailyDate.Value.Date;
                NextWeeklyDate = newDate.Value.Add(configuration.Star_time);
            }

            foreach (DayOfWeek ForDay in activeDays)
            {
                if (isLocatedNext == false &&
                    this.GetNumberDayOfWeek(actualDayOfWeek) < this.GetNumberDayOfWeek(ForDay))
                {
                    isLocatedNext = true;
                    day = this.GetNumberDayOfWeek(ForDay);
                }
            }

            if (isLocatedNext == false)
            {
                day = activeDays.Length > 1
                    ? this.GetNumberDayOfWeek(activeDays.Where((dayOfTheWeek) => dayOfTheWeek != actualDayOfWeek).ToArray().Min())
                    : this.GetNumberDayOfWeek(activeDays.Min());
            }

            int daysToAdd = (((day)) - (this.GetNumberDayOfWeek(actualDayOfWeek))) % 7;
            if (daysToAdd <= 0)
            {
                NextWeeklyDate = this.AddDaysForWeeksToCurrent(configuration, NextWeeklyDate);
            }
            return NextWeeklyDate.Value.AddDays(daysToAdd);
        }

        private int GetNumberDayOfWeek(DayOfWeek d)
        {
            return d switch
            {
                DayOfWeek.Monday => 1,
                DayOfWeek.Tuesday => 2,
                DayOfWeek.Wednesday => 3,
                DayOfWeek.Thursday => 4,
                DayOfWeek.Friday => 5,
                DayOfWeek.Saturday => 6,
                DayOfWeek.Sunday => 7,
                _ => 1,
            };
        }

        private DateTime? GetNextDateMonthly(ScheduleConfiguration configuration, DateTime? nextDate)
        {
            return null;
        }


        private void CheckWeeklyConfiguration(ScheduleConfiguration configuration)
        {
            if (configuration.Frecuency_weeks <= 0)
            {
                throw new InvalidOperationException("The weekly frequency must be greater than 0");
            }
            if (configuration.Days_active_week == null
                  || configuration.Days_active_week.Length == 0)
            {
                throw new InvalidOperationException("You must select at least one day of the week");
            }
        }

        private DateTime? AddDaysForWeeksToCurrent(ScheduleConfiguration configuration, DateTime? startDate)
        {
            int TotalDaysToAdd = configuration.Frecuency_weeks * DAYS_FOR_WEEK;
            return startDate.Value.AddDays(TotalDaysToAdd);
        }

        public bool InHorary(ScheduleConfiguration configuration, TimeSpan? nextTime)
        {
            if (configuration.Star_time <= nextTime
                && configuration.End_time >= nextTime)
            {
                return true;
            }
            return false;
        }

        public bool InLimitsDate(ScheduleConfiguration configuration, DateTime? nextDate)
        {
            if (configuration.Start_date <= nextDate
                && (configuration.End_date.HasValue == false || configuration.End_date >= nextDate))
            {
                return true;
            }
            return false;
        }

        public string GetDescriptionExecution(ScheduleConfiguration configuration)
        {
            if (configuration.Last_execution == null)
            {
                return string.Empty;
            }
            string DescriptionFrecuency = this.GetTextFrecuency(configuration);
            string DescriptionOcurrs = configuration.Ocurrs_type == Types.Once
                ? "Ocurrs once. " : DescriptionFrecuency;

            string Time = this.GetTextHourly(configuration);
            string DescriptionTime = this.GetTextTime(configuration, Time);

            return DescriptionOcurrs +
                   "Schedule will be used" +
                  DescriptionTime +
                   " starting on " + configuration.Start_date.Value.ToShortDateString();
        }

        private string GetTextFrecuency(ScheduleConfiguration configuration)
        {
            if (configuration.Frecuency != null)
            {
                return configuration.Frecuency == Frecuencys.Daily
                      ? "Ocurrs every day. "
                      : "Ocurrs every " + configuration.Frecuency_weeks + " weeks on "
                          + string.Join(" - ", configuration.Days_active_week) + ". ";
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetTextHourly(ScheduleConfiguration configuration)
        {
            if (configuration.Time_type == TimeTypes.Hours)
            {
                return " hours ";
            }
            if (configuration.Time_type == TimeTypes.Minutes)
            {
                return " minutes ";
            }
            if (configuration.Time_type == TimeTypes.Seconds)
            {
                return " seconds ";
            }
            return string.Empty;
        }

        private string GetTextTime(ScheduleConfiguration configuration, string textFrecuencyTime)
        {
            return (configuration.Ocurrs_type == Types.Once
                    || configuration.Daily_frecuency == DailyFrencuencys.OnceTime)
                ? " at " + configuration.Last_execution.Value.ToLongTimeString()
                : " every " + configuration.Time_frecuency + textFrecuencyTime + "between "
                + configuration.Star_time + " and " + configuration.End_time;
        }

        public void ValidateConfiguration(ScheduleConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new InvalidOperationException("Scheduler configuration can not be empty");
            }
        }
        private void ValidateDateLimits(ScheduleConfiguration configuration)
        {
            if (configuration.Start_date.HasValue == false)
            {
                throw new InvalidOperationException("Start date can not be empty");
            }
            if (configuration.End_date.HasValue &&
                (configuration.Start_date >= configuration.End_date))
            {
                throw new InvalidOperationException("End date cant not be equal start date");
            }
        }

        public IList<DateTime?> CalculateSerie(ScheduleConfiguration configuration, int numericSeries)
        {
            List<DateTime?> recurrenceDays = new();
            for (int i = 0; i < numericSeries; i++)
            {
                DateTime? NextDate = this.GetResult(configuration);
                if (NextDate != null)
                {
                    recurrenceDays.Add(NextDate);
                }
                else
                {
                    break;
                }
            }
            return recurrenceDays;
        }
    }
}

