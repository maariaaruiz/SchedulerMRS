using System;

namespace SchedulerNegocio
{
    public class ScheduleConfiguration
    {
        public ScheduleConfiguration()
        {
        }

        public DateTime? Current_Date { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public DateTime? Configuration_Date { get; set; }
        public string Frecuency { get; set; }
        public double Frecuency_Days { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
    }
}