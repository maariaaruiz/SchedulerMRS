using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SchedulerNegocio
{
    public class ScheduleCalculator
    {
        private const int DAYS_FOR_WEEK = 7;
        private const int DAY = 1;
        private LanguageConverter language;

        public DateTime? GetResult(ScheduleConfiguration configuration)
        {
            Language languageApplication = configuration != null
                ? configuration.Language_Application : Language.UK;
            language = new(languageApplication);

            this.ValidateConfiguration(configuration);

            if (configuration.Active == false)
            {
                throw new InvalidOperationException(language.GetTraduction("Configuration must be active"));
            }

            DateTime? nextDate = configuration.Ocurrs_type switch
            {
                Types.Once => this.GetNextDateOnce(configuration),
                Types.Recurring => this.GetNextDateRecurring(configuration),
                _ => throw new InvalidOperationException(language.GetTraduction("The type is not recognized")),
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
                throw new InvalidOperationException(language.GetTraduction("Configuration date can not be empty"));
            }

            this.ValidateDateLimits(configuration);

            if (configuration.Start_date > nextDate
              || (configuration.End_date.HasValue
                  && configuration.End_date.Value < nextDate.Value.Date))
            {
                throw new InvalidOperationException(language.GetTraduction("Next execution time exceeds date limits"));
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
                throw new InvalidOperationException(language.GetTraduction("Current date can not be empty"));
            }

            this.ValidateDateLimits(configuration);

            return this.GetNextDateTimeFrecuency(configuration, nextDate);
        }

        private DateTime? GetNextDateTimeFrecuency(ScheduleConfiguration configuration, DateTime? nextDate)
        {
            if (configuration.Frecuency != Frecuencys.Daily &&
                configuration.Frecuency != Frecuencys.Weekly
                && configuration.Frecuency != Frecuencys.Monthly)
            {
                throw new InvalidOperationException(language.GetTraduction("The frequency is not set correctly"));
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
            TimeSpan timeToAdd = this.GetTimeToAdd(configuration, nextDate);

            if (configuration.Last_execution != null
                && configuration.Daily_frecuency == DailyFrencuencys.OnceTime)
            {
                nextDailyDate = nextDailyDate.Value.AddDays(DAY);
            }

            if (!this.InHorary(configuration, timeToAdd)
                && configuration.Daily_frecuency == DailyFrencuencys.EveryTime)
            {
                nextDailyDate = nextDailyDate.Value.AddDays(DAY);
                if (this.InLimitsDate(configuration, nextDailyDate))
                {
                    return nextDailyDate.Value.Add(configuration.Star_time);
                }
                return null;
            }

            nextDailyDate = nextDailyDate.Value.Add(timeToAdd);
            if (this.InLimitsDate(configuration, nextDailyDate))
            {
                return nextDailyDate;
            }
            return null;
        }

        private TimeSpan GetTimeToAdd(ScheduleConfiguration configuration, DateTime? nextDailyDate)
        {
            TimeSpan? theTime;
            if (configuration.Daily_frecuency == DailyFrencuencys.OnceTime)
            {
                theTime = configuration.Time_once_frecuency;
                if (theTime == null)
                {
                    throw new InvalidOperationException(
                        language.GetTraduction("Must be indicate the daily time"));
                }
                return theTime.Value;
            }
            else if (configuration.Daily_frecuency == DailyFrencuencys.EveryTime)
            {
                return this.AddHourlyFrecuency(configuration, nextDailyDate);
            }
            else
            {
                throw new InvalidOperationException(
                    language.GetTraduction("Must to select a daily frequency type 'Once' or 'Every'"));
            }
        }

        private TimeSpan AddHourlyFrecuency(ScheduleConfiguration configuration, DateTime? nextDate)
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
                throw new InvalidOperationException(language.GetTraduction("The hourly frequency must be greater than 0"));
            }
            if (configuration.Time_type != TimeTypes.Hours &&
                configuration.Time_type != TimeTypes.Minutes &&
                configuration.Time_type != TimeTypes.Seconds)
            {
                throw new InvalidOperationException(language.GetTraduction("Must indicate the type of frecuency Hours, Minutes o Seconds correctly"));
            }
            if (configuration.Star_time >= configuration.End_time
                || configuration.Star_time == null
                || configuration.End_time == null)
            {
                throw new InvalidOperationException(language.GetTraduction("The Horary Range is not set correctly"));
            }
        }

        private DateTime? GetNextDateWeekly(ScheduleConfiguration configuration, DateTime? nextDate)
        {
            this.CheckWeeklyConfiguration(configuration);
            TimeSpan timeToAdd = this.GetTimeToAdd(configuration, nextDate.Value.Date);
            DateTime nextWeeklyDate = nextDate.Value.Date.Add(timeToAdd);

            nextWeeklyDate = this.CalculateNextDateWeekly(configuration, nextWeeklyDate);
            if (this.InLimitsDate(configuration, nextWeeklyDate))
            {
                return nextWeeklyDate;
            }
            return null;
        }

        private DateTime CalculateNextDateWeekly(ScheduleConfiguration configuration, DateTime nextDate)
        {
            DateTime nextWeeklyDate = nextDate;
            if (this.InHorary(configuration, nextDate.TimeOfDay) &&
                configuration.Days_active_week.Contains(nextDate.DayOfWeek))
            {
                return nextWeeklyDate;
            }
            if (configuration.Daily_frecuency == DailyFrencuencys.EveryTime)
            {
                nextWeeklyDate = nextDate.Date.Add(configuration.Star_time);
            }

            int nextDay = this.GetNextDayOfWeekInActiveDays(configuration, nextDate);
            int daysToAdd = (((nextDay)) - (this.GetNumberDayOfWeek(nextDate.DayOfWeek))) % 7;
            if (daysToAdd <= 0)
            {
                nextWeeklyDate = this.AddDaysForWeeksToCurrent(configuration, nextWeeklyDate);
            }
            return nextWeeklyDate.AddDays(daysToAdd);
        }

        private int GetNextDayOfWeekInActiveDays(ScheduleConfiguration configuration, DateTime nextDate)
        {
            int day = 0;
            bool isLocatedNext = false;

            DayOfWeek[] activeDays = configuration.Days_active_week.OrderBy(D => this.GetNumberDayOfWeek(D)).ToArray();
            foreach (DayOfWeek ForDay in activeDays)
            {
                if (isLocatedNext == false &&
                    this.GetNumberDayOfWeek(nextDate.DayOfWeek) < this.GetNumberDayOfWeek(ForDay))
                {
                    isLocatedNext = true;
                    day = this.GetNumberDayOfWeek(ForDay);
                }
            }
            if (isLocatedNext == false)
            {
                day = activeDays.Length > 1
                    ? this.GetNumberDayOfWeek(activeDays.Where((dayOfTheWeek)
                        => dayOfTheWeek != nextDate.DayOfWeek).ToArray().Min())
                    : this.GetNumberDayOfWeek(activeDays.Min());
            }
            return day;
        }

        private void CheckWeeklyConfiguration(ScheduleConfiguration configuration)
        {
            if (configuration.Frecuency_weeks <= 0)
            {
                throw new InvalidOperationException(language.GetTraduction("The weekly frequency must be greater than 0"));
            }
            if (configuration.Days_active_week == null
                  || configuration.Days_active_week.Length == 0)
            {
                throw new InvalidOperationException(language.GetTraduction("You must select at least one day of the week"));
            }
        }

        private DateTime AddDaysForWeeksToCurrent(ScheduleConfiguration configuration, DateTime startDate)
        {
            int TotalDaysToAdd = configuration.Frecuency_weeks * DAYS_FOR_WEEK;
            return startDate.AddDays(TotalDaysToAdd);
        }

        private int GetNumberDayOfWeek(DayOfWeek? day)
        {
            return day switch
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
        private int GetDaysToAddMonthly(ScheduleConfiguration configuration)
        {
            return configuration.Actual_week switch
            {
                WeeksInMonth.First => 0,
                WeeksInMonth.Second => 7,
                WeeksInMonth.Third => 14,
                WeeksInMonth.Fourth => 21,
                WeeksInMonth.Last => 35,
                _ => throw new InvalidOperationException(
                    language.GetTraduction("You must select First, Second, Third or Last")),
            };
        }

        private DayOfWeek?[] GetActiveDaysMonthly(ScheduleConfiguration configuration)
        {
            DayOfWeek?[] days;
            switch (configuration.Active_days_monthly)
            {
                case Days_Of_Week_Monthly.Monday:
                    days = new DayOfWeek?[1];
                    days[0] = DayOfWeek.Monday;
                    break;
                case Days_Of_Week_Monthly.Tuesday:
                    days = new DayOfWeek?[1];
                    days[0] = DayOfWeek.Tuesday;
                    break;
                case Days_Of_Week_Monthly.Wednesday:
                    days = new DayOfWeek?[1];
                    days[0] = DayOfWeek.Wednesday;
                    break;
                case Days_Of_Week_Monthly.Thursday:
                    days = new DayOfWeek?[1];
                    days[0] = DayOfWeek.Thursday;
                    break;
                case Days_Of_Week_Monthly.Friday:
                    days = new DayOfWeek?[1];
                    days[0] = DayOfWeek.Friday;
                    break;
                case Days_Of_Week_Monthly.Saturday:
                    days = new DayOfWeek?[1];
                    days[0] = DayOfWeek.Saturday;
                    break;
                case Days_Of_Week_Monthly.Sunday:
                    days = new DayOfWeek?[1];
                    days[0] = DayOfWeek.Sunday;
                    break;
                case Days_Of_Week_Monthly.day:
                    days = new DayOfWeek?[1];
                    days[0] = null;
                    break;
                case Days_Of_Week_Monthly.weekday:
                    days = new DayOfWeek?[5];
                    days[0] = DayOfWeek.Monday;
                    days[1] = DayOfWeek.Tuesday;
                    days[2] = DayOfWeek.Wednesday;
                    days[3] = DayOfWeek.Thursday;
                    days[4] = DayOfWeek.Friday;
                    break;
                case Days_Of_Week_Monthly.weekendday:
                    days = new DayOfWeek?[2];
                    days[0] = DayOfWeek.Saturday;
                    days[1] = DayOfWeek.Sunday;
                    break;
                default:
                    throw new InvalidOperationException(
                        language.GetTraduction("You must select at least one day of the week"));
            }
            return days;
        }

        private DateTime? GetNextDateMonthly(ScheduleConfiguration configuration, DateTime? nextDate)
        {
            TimeSpan TimeToAdd = this.GetTimeToAdd(configuration, nextDate.Value.Date);
            DateTime NextMonthlyDate = nextDate.Value.Date.Add(TimeToAdd);
            var nextDay = this.GetActiveDaysMonthly(configuration);

            if (this.InHorary(configuration, NextMonthlyDate.TimeOfDay)
                && configuration.Last_execution != null)
            {
                if (nextDay.Contains(NextMonthlyDate.DayOfWeek)
                   || nextDay[0] == null)
                {
                    return NextMonthlyDate;
                }
            }

            NextMonthlyDate = this.GetNextCalculateDayMonth(configuration, NextMonthlyDate, nextDay);
            if (this.InLimitsDate(configuration, NextMonthlyDate))
            {
                return NextMonthlyDate;
            }
            return null;
        }

        public DateTime GetNextCalculateDayMonth(ScheduleConfiguration configuration, DateTime nextDate, DayOfWeek?[] nextDay)
        {
            DateTime nextDateMonthly = configuration.Daily_frecuency == DailyFrencuencys.OnceTime
                ? nextDate : nextDate.Date.Add(configuration.Star_time);

            if (nextDay.Count() == 1 && nextDay[0] == null)
            {
                return this.CalculateDay(configuration, nextDateMonthly);
            }
            else if (nextDay.Count() == 1)
            {
                return this.CalculateOneDayInMonth(configuration, nextDateMonthly, nextDay);
            }
            else if (nextDay.Count() == 2)
            {
                return this.CalculateNextWeekendDay(configuration, nextDateMonthly);
            }
            return this.CalculateNextWeekend(configuration, nextDateMonthly);
        }

        private DateTime CalculateDay(ScheduleConfiguration configuration, DateTime nextDate)
        {
            DateTime nextDateMonthly = this.GetFirstDateOfMonth(nextDate);
            switch (configuration.Actual_week)
            {
                case WeeksInMonth.Second:
                    nextDateMonthly = nextDateMonthly.AddDays(1);
                    break;
                case WeeksInMonth.Third:
                    nextDateMonthly = nextDateMonthly.AddDays(2);
                    break;
                case WeeksInMonth.Fourth:
                    nextDateMonthly = nextDateMonthly.AddDays(3);
                    break;
                case WeeksInMonth.Last:
                    nextDateMonthly = this.GetLastDateOfMonth(nextDateMonthly);
                    break;
            }
            if (nextDate >= nextDateMonthly)
            {
                nextDateMonthly = nextDateMonthly.AddMonths(configuration.Frecuency_months);
            }
            if (configuration.Actual_week == WeeksInMonth.Last)
            {
                nextDateMonthly = this.GetLastDateOfMonth(nextDateMonthly);
            }
            return nextDateMonthly;
        }

        public DateTime GetFirstDateOfMonth(DateTime? nextDate)
        {
            DateTime.TryParse(String.Format("{0}/{1}/1 {2}:{3}:{4}", nextDate.Value.Year, nextDate.Value.Month,
              nextDate.Value.Hour, nextDate.Value.Minute, nextDate.Value.Second), out DateTime date);
            return date;
        }
        private DateTime GetLastDateOfMonth(DateTime inDate)
        {
            var daysInMonth = DateTime.DaysInMonth(inDate.Year, inDate.Month);
            return new DateTime(inDate.Year, inDate.Month, daysInMonth, inDate.Hour, inDate.Minute, inDate.Second);
        }

        // Monday, Tuesday, Wednesday, Thursday, Friday, Saturday or Sunday
        private DateTime CalculateOneDayInMonth(ScheduleConfiguration configuration, DateTime nextDate, DayOfWeek?[] nextDay)
        {
            DateTime FirstDate = this.GetFirstDateOfMonth(nextDate);
            int days = this.GetDaysToAddMonthly(configuration);
            if (configuration.Actual_week == WeeksInMonth.Last)
            {
                return this.GetLastOneDayInMonth(configuration, nextDate, nextDay);
            }

            DateTime NextDateMonthly = this.GetNextDayInActiveDays(FirstDate, nextDay, days);
            if (NextDateMonthly <= nextDate && configuration.Last_execution != null ||
                (NextDateMonthly < nextDate && configuration.Last_execution == null))
            {
                NextDateMonthly = this.GetNextDayInActiveDays(
                    FirstDate.AddMonths(configuration.Frecuency_months), nextDay, days);
            }
            return NextDateMonthly;
        }

        private DateTime GetNextDayInActiveDays(DateTime date, DayOfWeek?[] nextDay, int days)
        {
            while (date.DayOfWeek != (DayOfWeek)nextDay[0])
            {
                date = date.AddDays(1);
            }
            return date.AddDays(days);
        }

        private DateTime GetLastOneDayInMonth(ScheduleConfiguration configuration, DateTime nextDate, DayOfWeek?[] nextDay)
        {
            DayOfWeek lastDayOfWeek = (DayOfWeek)nextDay[0];
            if (configuration.Last_execution != null)
            {
                nextDate = nextDate.AddMonths(configuration.Frecuency_months);
            }

            nextDate = this.GetLastEspecificDateOfMonth(nextDate, lastDayOfWeek);
            if (configuration.Current_date > nextDate)
            {
                return this.GetLastEspecificDateOfMonth(nextDate.AddMonths(configuration.Frecuency_months), lastDayOfWeek);
            }
            return nextDate;
        }

        // Saturday or Sunday
        private DateTime CalculateNextWeekendDay(ScheduleConfiguration configuration, DateTime nextDate)
        {
            DateTime firstDateMonth = this.GetFirstDateOfMonth(nextDate);
            int days = this.GetDaysToAddMonthly(configuration);
            if (configuration.Actual_week == WeeksInMonth.Last)
            {
                return this.GeLastWeekendDay(configuration, firstDateMonth);
            }

            DateTime nextDateMonth = this.GetNearWeekendDay(firstDateMonth);
            if (configuration.Last_execution == null)
            {
                nextDateMonth = nextDateMonth.AddDays(days);
                if (nextDateMonth > configuration.Current_date)
                {
                    return nextDateMonth;
                }
            }
            if (nextDate.DayOfWeek == DayOfWeek.Saturday)
            {
                return nextDate.Date.AddDays(1).Add(nextDateMonth.TimeOfDay);
            }

            nextDateMonth = this.GetFirstWeekendDayOfNextMonth(configuration, nextDateMonth);
            if (nextDateMonth.DayOfWeek == DayOfWeek.Sunday)
            {
                DateTime date = nextDateMonth.AddDays(days - 1);
                return date.Month <= nextDate.Month ? nextDateMonth : date;
            }
            return nextDateMonth.AddDays(days);
        }


        public DateTime GetLastEspecificDateOfMonth(DateTime nextDate, DayOfWeek nextDayOfWeek)
        {
            DateTime lastDate = this.GetLastDateOfMonth(nextDate);
            while (lastDate.DayOfWeek != nextDayOfWeek)
            {
                lastDate = lastDate.AddDays(-1);
            }
            return lastDate;
        }

        private DateTime GeLastWeekendDay(ScheduleConfiguration configuration, DateTime nextDate)
        {
            DateTime lastSaturday = this.GetLastEspecificDateOfMonth(nextDate, DayOfWeek.Saturday);
            DateTime lastDateMonth = this.GetLastDateOfMonth(nextDate);

            if (configuration.Last_execution == null)
            {
                return (configuration.Current_date.Value.Date <= lastSaturday.Date)
                    ? lastSaturday
                    : lastSaturday.AddDays(1);
            }
            if (configuration.Last_execution.Value.DayOfWeek == DayOfWeek.Saturday
                 && configuration.Last_execution.Value.Date != lastDateMonth.Date)
            {
                return lastSaturday.AddDays(1);
            }
            return this.GetLastEspecificDateOfMonth(nextDate.AddMonths(configuration.Frecuency_months), DayOfWeek.Saturday);
        }

        private DateTime GetFirstWeekendDayOfNextMonth(ScheduleConfiguration configuration, DateTime nextDate)
        {
            DateTime dateWithMonths = nextDate.AddMonths(configuration.Frecuency_months);
            DateTime.TryParse(String.Format("{0}/{1}/1 {2}:{3}:{4}", dateWithMonths.Year, dateWithMonths.Month,
                dateWithMonths.Hour, dateWithMonths.Minute, dateWithMonths.Second), out DateTime date);
            return this.GetNearWeekendDay(date);
        }

        //Search near saturday or sunday 
        private DateTime GetNearWeekendDay(DateTime nextDate)
        {
            int daysToSaturday = ((this.GetNumberDayOfWeek((DayOfWeek.Saturday)))
                                    - (this.GetNumberDayOfWeek(nextDate.DayOfWeek))) % 7;
            int daysToSunday = ((this.GetNumberDayOfWeek((DayOfWeek.Sunday)))
                                    - (this.GetNumberDayOfWeek(nextDate.DayOfWeek))) % 7;
            if (daysToSaturday > daysToSunday || daysToSaturday < 0)
            {
                return nextDate.AddDays(daysToSunday);
            }
            return nextDate.AddDays(daysToSaturday);
        }

        // All week less Saturday and Sunday
        public DateTime CalculateNextWeekend(ScheduleConfiguration configuration, DateTime nextDate)
        {
            DateTime firstDateMonth = this.GetFirstDateOfMonth(nextDate);
            DateTime lastDateMonth = this.GetLastDateOfMonth(nextDate);
            DateTime nextDateMonth = firstDateMonth;

            if (configuration.Last_execution != null)
            {
                if (nextDate.DayOfWeek == DayOfWeek.Friday || nextDate.Date == lastDateMonth.Date)
                {
                    nextDateMonth = nextDateMonth.AddMonths(configuration.Frecuency_months);
                }
                else
                {
                    return nextDate.Date.AddDays(1).Add(nextDateMonth.TimeOfDay);
                }
            }
            return this.GetNextDateWeekend(configuration, nextDateMonth, firstDateMonth);
        }

        private DateTime GetNextDateWeekend(ScheduleConfiguration configuration, DateTime nextDate, DateTime inicialDate)
        {
            DateTime nextDateMonth = this.AddFrecuencyForWeek(configuration, nextDate);

            int weekAnualCurrentDate = CultureInfo.CurrentUICulture.Calendar
                        .GetWeekOfYear(configuration.Current_date.Value, CalendarWeekRule.FirstDay,
                                       configuration.Current_date.Value.DayOfWeek);

            int weekAnualNextDate = CultureInfo.CurrentUICulture.Calendar
                         .GetWeekOfYear(nextDateMonth, CalendarWeekRule.FirstDay, nextDateMonth.DayOfWeek);

            if (configuration.Current_date > nextDateMonth
                && weekAnualCurrentDate > weekAnualNextDate)
            {
                nextDateMonth = inicialDate.AddMonths(configuration.Frecuency_months);
                return this.AddFrecuencyForWeek(configuration, nextDateMonth);
            }

            if (configuration.Current_date >= nextDateMonth
                && weekAnualCurrentDate == weekAnualNextDate
                && Enumerable.Range(1, 5).Contains(
                    (int)configuration.Current_date.Value.DayOfWeek))
            {
                nextDateMonth = configuration.Current_date.Value.Date
                                .Add(nextDateMonth.TimeOfDay);
            }
            return nextDateMonth;
        }

        private DateTime AddFrecuencyForWeek(ScheduleConfiguration configuration, DateTime nextDate)
        {
            DateTime nextDateMonthly = nextDate;
            switch (configuration.Actual_week)
            {
                case WeeksInMonth.First:
                    nextDateMonthly = nextDate;
                    break;
                case WeeksInMonth.Second:
                    nextDateMonthly = nextDateMonthly.AddDays(7);
                    break;
                case WeeksInMonth.Third:
                    nextDateMonthly = nextDateMonthly.AddDays(14);
                    break;
                case WeeksInMonth.Fourth:
                    nextDateMonthly = nextDateMonthly.AddDays(21);
                    break;
                case WeeksInMonth.Last:
                    nextDateMonthly = this.GetLastEspecificDateOfMonth(nextDateMonthly, DayOfWeek.Monday);
                    break;
            }
            //while date is saturday or sunday +1 day
            while (!Enumerable.Range(1, 5).Contains((int)nextDateMonthly.DayOfWeek))
            {
                nextDateMonthly = nextDateMonthly.AddDays(1);
            }
            //calculate the differemce with the monday and add days if actual week != first
            int days = (((int)DayOfWeek.Monday) - (int)(nextDateMonthly.DayOfWeek)) % 7;
            if (configuration.Actual_week != WeeksInMonth.First)
            {
                nextDateMonthly = nextDateMonthly.AddDays(days);
            }
            return nextDateMonthly;
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
                ? language.GetTraduction("Ocurrs once. ") : DescriptionFrecuency;

            string Time = this.GetTextHourly(configuration);
            string DescriptionTime = this.GetTextTime(configuration, Time);

            return DescriptionOcurrs +
                   language.GetTraduction("Schedule will be used on") +
                   language.GetDateTimeFormatedLanguage(configuration.Last_execution.Value) +
                  DescriptionTime +
                  language.GetTraduction(" starting on ") + language.GetDateFormatedLanguage(configuration.Start_date.Value);
        }

        private string GetTextFrecuency(ScheduleConfiguration configuration)
        {
            if (configuration.Frecuency != null)
            {
                return configuration.Frecuency switch
                {
                    Frecuencys.Daily => language.GetTraduction("Ocurrs every day. "),
                    Frecuencys.Weekly => language.GetTraduction("Ocurrs every ") + configuration.Frecuency_weeks +
                                            language.GetTraduction(" weeks on ") + string.Join(" - ",
                                            language.GetTraductionList(configuration.Days_active_week.ToList())) + ". ",
                    Frecuencys.Monthly => language.GetTraduction("Ocurrs the ") +
                                            language.GetTraduction(configuration.Actual_week.ToString()) + " " +
                                            language.GetTraduction(configuration.Active_days_monthly.ToString()) +
                                            language.GetTraduction(" of every ") +
                                            configuration.Frecuency_months + language.GetTraduction(" months. "),
                    _ => string.Empty,
                };
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
                return language.GetTraduction(" hours ");
            }
            if (configuration.Time_type == TimeTypes.Minutes)
            {
                return language.GetTraduction(" minutes ");
            }
            if (configuration.Time_type == TimeTypes.Seconds)
            {
                return language.GetTraduction(" seconds ");
            }
            return string.Empty;
        }

        private string GetTextTime(ScheduleConfiguration configuration, string textFrecuencyTime)
        {
            return (configuration.Ocurrs_type == Types.Once
                    || configuration.Daily_frecuency == DailyFrencuencys.OnceTime)
                ? "" : language.GetTraduction(" every ") + configuration.Time_frecuency + textFrecuencyTime +
                language.GetTraduction("between ")
                + configuration.Star_time + language.GetTraduction(" and ") + configuration.End_time;
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
                throw new InvalidOperationException(language.GetTraduction("Start date can not be empty"));
            }
            if (configuration.End_date.HasValue &&
                (configuration.Start_date >= configuration.End_date))
            {
                throw new InvalidOperationException(language.GetTraduction("End date cant not be equal start date"));
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
