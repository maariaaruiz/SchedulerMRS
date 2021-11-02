using SchedulerNegocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Scheduler
{
    public partial class CalculatorNextDate : Form
    {
        public CalculatorNextDate()
        {
            InitializeComponent();
        }
        private void BtCalcula_Click(object sender, EventArgs e)
        {
            List<DayOfWeek> listDaysOfWeek = new();
            if (this.CkMonday.Checked)
            {
                listDaysOfWeek.Add(DayOfWeek.Monday);
            }
            if (this.CkTuesday.Checked)
            {
                listDaysOfWeek.Add(DayOfWeek.Tuesday);
            }
            if (this.CkWednesday.Checked)
            {
                listDaysOfWeek.Add(DayOfWeek.Wednesday);
            }
            if (this.CkThursday.Checked)
            {
                listDaysOfWeek.Add(DayOfWeek.Thursday);
            }
            if (this.CkFriday.Checked)
            {
                listDaysOfWeek.Add(DayOfWeek.Friday);
            }
            if (this.CkSaturday.Checked)
            {
                listDaysOfWeek.Add(DayOfWeek.Saturday);
            }
            if (this.CkSunday.Checked)
            {
                listDaysOfWeek.Add(DayOfWeek.Sunday);
            }


            ScheduleConfiguration conf = new()
            {
                Current_date = this.DtpCurrentDate.Value.Date,
                Active = this.CkActive.Checked,
                Type = this.CbTypes.SelectedIndex,
                Configuration_date = this.CbTypes.SelectedIndex ==
                     (int)Types.Once ? this.DtpDateTime.Value : null,
                Frecuency = this.CbOcurrs.Text,
                Start_date = this.DtpStarDate.Value.Date,
                End_date = this.DtpEndDate.Value.Date,
                Daily_frecuency = this.CkOnceFrecuency.Checked == true
                    ? DailyFrencuencys.OnceTime.ToString()
                    : (this.CkEveryFrecuency.Checked == true
                        ? DailyFrencuencys.EveryTime.ToString()
                        : null),
                Time_once_frecuency = this.DtpTimeOnceFrecuency.Enabled
                    && this.CkOnceFrecuency.Checked
                        ? this.DtpTimeOnceFrecuency.Value.TimeOfDay
                        : null,
                Time_type = this.CbOcurrsFrecuency.Text,
                Time_frecuency = (int)this.NumFrecuency.Value,
                Star_time = this.DtpStarFrecuency.Value.TimeOfDay,
                End_time = this.DtpEndFrecuency.Value.TimeOfDay,
                Days_active_week = listDaysOfWeek.ToArray(),
                Frecuency_weeks = (int)this.NumWeeks.Value
                
            };

            ScheduleCalculator Calculator = new(conf);
            try
            {
                Calculator.GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
            this.TbNextDate.Text = Calculator.Next_Execution_Time.ToString();
            this.TbDescription.Text = Calculator.Description_Execution;
        }

        private void CbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CbTypes.SelectedIndex)
            {
                case 0:
                    this.CbOcurrs.Enabled = false;
                    this.DtpDateTime.Enabled = true;                  
                    this.PDailyFrecuency.Enabled = false;
                    this.PWeeklyConfiguration.Enabled = false;
                    break;
                case 1:
                    this.CbOcurrs.Enabled = true;                 
                    this.DtpDateTime.Enabled = false;
                    this.PDailyFrecuency.Enabled = true;

                    break;
            }
        }

        private void CbOcurrs_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CbOcurrs.SelectedIndex)
            {
                case 0:
                    this.PWeeklyConfiguration.Enabled = false;
                    break;
                case 1:
                    this.PWeeklyConfiguration.Enabled = true;
                    break;
            }
        }

        private void CkOnceFrecuency_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CkOnceFrecuency.Checked == true)
            {
                this.DtpTimeOnceFrecuency.Enabled = true;
                this.CkEveryFrecuency.Checked = false;

            }
            else if (this.CkOnceFrecuency.Checked == false)
            {
                this.DtpTimeOnceFrecuency.Enabled = false;
            }

        }

        private void CkEveryFrecuency_CheckedChanged(object sender, EventArgs e)
        {

            if (this.CkEveryFrecuency.Checked == true)
            {

                this.NumFrecuency.Enabled = true;
                this.CbOcurrsFrecuency.Enabled = true;
                this.DtpStarFrecuency.Enabled = true;
                this.DtpEndFrecuency.Enabled = true;
                this.CkOnceFrecuency.Checked = false;

            }
            else if (this.CkEveryFrecuency.Checked == false)
            {
                this.NumFrecuency.Enabled = false;
                this.CbOcurrsFrecuency.Enabled = false;
                this.DtpStarFrecuency.Enabled = false;
                this.DtpEndFrecuency.Enabled = false;
            }
        }

    }
}