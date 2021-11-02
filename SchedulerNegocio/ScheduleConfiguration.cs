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
        public int Type { get; set; }
        public DateTime? Configuration_date { get; set; }
        public string Frecuency { get; set; }
        public DateTime? Start_date { get; set; }
        public DateTime? End_date { get; set; }
        public string Daily_frecuency { get; set; }
        public TimeSpan? Time_once_frecuency { get; set; }
        public string Time_type { get; set; }
        public int Time_frecuency { get; set; }
        public TimeSpan Star_time { get; set; }
        public TimeSpan End_time { get; set; }
        public int Frecuency_weeks { get; set; }
        public  DayOfWeek[] Days_active_week { get; set; }

    }
}