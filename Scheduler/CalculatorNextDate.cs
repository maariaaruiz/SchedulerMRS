using SchedulerNegocio;
using System;
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
            ScheduleConfiguration conf = new()
            {
                Current_Date = this.DtpCurrentDate.Value,
                Active = this.CkActive.Checked,
                Type = this.CbTypes.SelectedIndex,
                Configuration_Date = this.CbTypes.SelectedIndex ==
                     (int)Types.Once ? this.DtpDateTime.Value : null,
                Frecuency = this.CbOcurrs.Text,
                Frecuency_Days = (double)this.NumDays.Value,
                Start_Date = this.DtpStarDate.Value,
                End_Date = this.DtpEndDate.Value
            };
            ScheduleCalculator Calculator = new(conf);

            try
            {
                Calculator.GetNextDateExecution();
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
                    this.NumDays.Enabled = false;
                    this.NumDays.Value = 0;
                    break;
                case 1:
                    this.CbOcurrs.Enabled = true;
                    this.NumDays.Enabled = true;
                    this.DtpDateTime.Enabled = false;
                    break;
            }
        }
    }
}