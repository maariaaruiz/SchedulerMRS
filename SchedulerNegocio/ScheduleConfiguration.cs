using System;


namespace SchedulerNegocio
{
    public class ScheduleConfiguration
    {
        public ScheduleConfiguration()
        {
        }

        public DateTime? Current_date { get; set; }
        public bool Active { get; set; }
        public Types? Ocurrs_type { get; set; }
        public DateTime? Configuration_date { get; set; }
        public Frecuencys? Frecuency { get; set; }
        public DateTime? Start_date { get; set; }
        public DateTime? End_date { get; set; }
        public DailyFrencuencys? Daily_frecuency { get; set; }
        public TimeSpan? Time_once_frecuency { get; set; }
        public TimeTypes? Time_type { get; set; }
        public int Time_frecuency { get; set; }
        public TimeSpan Star_time { get; set; }
        public TimeSpan End_time { get; set; }
        public int Frecuency_weeks { get; set; }
        public  DayOfWeek[] Days_active_week { get; set; }
        public int Frecuency_months { get; set; }
        public WeeksInMonth? Actual_week { get; set; }
        public Days_Of_Week_Monthly? Active_days_monthly { get; set; }

        public DateTime? Last_execution { get; set; }

    }
}