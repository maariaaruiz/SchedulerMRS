using System;

namespace SchedulerNegocio
{
    public class ScheduleCalculator
    {
        private readonly ScheduleConfiguration Configuration;
        public DateTime? Next_Execution_Time { get; set; }
        public string Description_Execution { get; set; }

        public ScheduleCalculator(ScheduleConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void GetNextDateExecution()
        {
            this.CheckConfiguration();
            if (this.Configuration.Type == (int)Types.Once)
            {
                this.Next_Execution_Time = this.GetNextDateOnce();
            }
            else
            {
                this.Next_Execution_Time = this.GetNextDateRecurring();
            }
            this.Description_Execution = this.GetDescriptionExecution();
        }

        private DateTime? GetNextDateOnce()
        {
            DateTime? NextTime = this.Configuration.Configuration_Date;
            this.ValidateLimits(NextTime);
            return NextTime;
        }

        private DateTime? GetNextDateRecurring()
        {
            DateTime? NextDate = this.Configuration.Current_Date.Value
                .AddDays(this.Configuration.Frecuency_Days).Date;
            this.CheckFrecuency();
            this.ValidateLimits(NextDate);
            return NextDate;
        }

        private string GetDescriptionExecution()
        {
            string DescriptionOcurrs = this.Configuration.Type == (int)Types.Once 
                ? "Ocurrs once. " : "Ocurrs every day. ";
            return DescriptionOcurrs +
                   "Schedule will be used on " + this.Next_Execution_Time.Value.ToShortDateString() +
                   " at " + this.Next_Execution_Time.Value.ToShortTimeString() +
                   " starting on " + this.Configuration.Start_Date.Value.ToShortDateString();
        }

        public void CheckConfiguration()
        {
            if (this.Configuration.Active == false)
            {
                throw new Exception("Configuration must be active");
            }
            if (this.Configuration.Type == (int)Types.Once)
            {
                if (this.Configuration.Configuration_Date.HasValue == false)
                {
                    throw new Exception("Configuration date can not be empty");
                }
            }
            else if (this.Configuration.Type == (int)Types.Recurring)
            {
                if (this.Configuration.Current_Date.HasValue == false)
                {
                    throw new Exception("Current date can not be empty");
                }
            }
            else
            {
                throw new Exception("The type is not recognized");
            }

            if (this.Configuration.Start_Date.HasValue == false)
            {
                throw new Exception("Start date can not be empty");
            }
        }

        public void CheckFrecuency()
        {
            if (this.Configuration.Frecuency_Days <= 0)
            {
                throw new Exception("The frequency must be greater than 0");
            }
            if (this.Configuration.Frecuency != Frecuencys.Daily.ToString())
            {
                throw new Exception("The frequency is not set correctly");
            }
        }

        public void ValidateLimits(DateTime? DateValidate)
        {
            if (this.Configuration.Start_Date > DateValidate
              || (this.Configuration.End_Date.HasValue
                  && this.Configuration.End_Date.Value < DateValidate.Value.Date))
            {
                throw new Exception("Next execution time exceeds date limits");
            }

        }
    }
}