
namespace Scheduler
{
    partial class CalculatorNextDate
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtCalcula = new System.Windows.Forms.Button();
            this.LCurrentDate = new System.Windows.Forms.Label();
            this.LType = new System.Windows.Forms.Label();
            this.CbTypes = new System.Windows.Forms.ComboBox();
            this.CkActive = new System.Windows.Forms.CheckBox();
            this.lDateTime = new System.Windows.Forms.Label();
            this.lOcurrs = new System.Windows.Forms.Label();
            this.CbOcurrs = new System.Windows.Forms.ComboBox();
            this.lEvery = new System.Windows.Forms.Label();
            this.lStarDate = new System.Windows.Forms.Label();
            this.lEndDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LInput = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DtpCurrentDate = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lConfiguration = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.DtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.lday = new System.Windows.Forms.Label();
            this.NumDays = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.llimits = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.DtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.DtpStarDate = new System.Windows.Forms.DateTimePicker();
            this.panel7 = new System.Windows.Forms.Panel();
            this.TbDescription = new System.Windows.Forms.TextBox();
            this.lDescription = new System.Windows.Forms.Label();
            this.TbNextDate = new System.Windows.Forms.TextBox();
            this.lNextDate = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lOutput = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumDays)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtCalcula
            // 
            this.BtCalcula.Location = new System.Drawing.Point(239, 13);
            this.BtCalcula.Name = "BtCalcula";
            this.BtCalcula.Size = new System.Drawing.Size(234, 19);
            this.BtCalcula.TabIndex = 5;
            this.BtCalcula.Text = "Calculate next date";
            this.BtCalcula.UseVisualStyleBackColor = true;
            this.BtCalcula.Click += new System.EventHandler(this.BtCalcula_Click);
            // 
            // LCurrentDate
            // 
            this.LCurrentDate.AutoSize = true;
            this.LCurrentDate.Location = new System.Drawing.Point(2, 16);
            this.LCurrentDate.Name = "LCurrentDate";
            this.LCurrentDate.Size = new System.Drawing.Size(65, 13);
            this.LCurrentDate.TabIndex = 3;
            this.LCurrentDate.Text = "Current date";
            // 
            // LType
            // 
            this.LType.AutoSize = true;
            this.LType.Location = new System.Drawing.Point(2, 15);
            this.LType.Name = "LType";
            this.LType.Size = new System.Drawing.Size(31, 13);
            this.LType.TabIndex = 9;
            this.LType.Text = "Type";
            // 
            // CbTypes
            // 
            this.CbTypes.FormattingEnabled = true;
            this.CbTypes.ItemHeight = 13;
            this.CbTypes.Items.AddRange(new object[] {
            "Once",
            "Recurring"});
            this.CbTypes.Location = new System.Drawing.Point(109, 8);
            this.CbTypes.Name = "CbTypes";
            this.CbTypes.Size = new System.Drawing.Size(124, 21);
            this.CbTypes.TabIndex = 7;
            this.CbTypes.Text = "Once";
            this.CbTypes.SelectedIndexChanged += new System.EventHandler(this.CbTypes_SelectedIndexChanged);
            // 
            // CkActive
            // 
            this.CkActive.AutoSize = true;
            this.CkActive.Checked = true;
            this.CkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CkActive.Location = new System.Drawing.Point(243, 10);
            this.CkActive.Name = "CkActive";
            this.CkActive.Size = new System.Drawing.Size(65, 17);
            this.CkActive.TabIndex = 11;
            this.CkActive.Text = "Enabled";
            this.CkActive.UseVisualStyleBackColor = true;
            // 
            // lDateTime
            // 
            this.lDateTime.AutoSize = true;
            this.lDateTime.Location = new System.Drawing.Point(2, 50);
            this.lDateTime.Name = "lDateTime";
            this.lDateTime.Size = new System.Drawing.Size(53, 13);
            this.lDateTime.TabIndex = 12;
            this.lDateTime.Text = "DateTime";
            // 
            // lOcurrs
            // 
            this.lOcurrs.AutoSize = true;
            this.lOcurrs.Location = new System.Drawing.Point(4, 82);
            this.lOcurrs.Name = "lOcurrs";
            this.lOcurrs.Size = new System.Drawing.Size(38, 13);
            this.lOcurrs.TabIndex = 14;
            this.lOcurrs.Text = "Ocurrs";
            // 
            // CbOcurrs
            // 
            this.CbOcurrs.DisplayMember = "Daily";
            this.CbOcurrs.Enabled = false;
            this.CbOcurrs.FormattingEnabled = true;
            this.CbOcurrs.Items.AddRange(new object[] {
            "Daily"});
            this.CbOcurrs.Location = new System.Drawing.Point(109, 78);
            this.CbOcurrs.MaxDropDownItems = 1;
            this.CbOcurrs.Name = "CbOcurrs";
            this.CbOcurrs.Size = new System.Drawing.Size(124, 21);
            this.CbOcurrs.TabIndex = 15;
            this.CbOcurrs.Text = "Daily";
            // 
            // lEvery
            // 
            this.lEvery.AutoSize = true;
            this.lEvery.Location = new System.Drawing.Point(240, 84);
            this.lEvery.Name = "lEvery";
            this.lEvery.Size = new System.Drawing.Size(34, 13);
            this.lEvery.TabIndex = 16;
            this.lEvery.Text = "Every";
            // 
            // lStarDate
            // 
            this.lStarDate.AutoSize = true;
            this.lStarDate.Location = new System.Drawing.Point(0, 16);
            this.lStarDate.Name = "lStarDate";
            this.lStarDate.Size = new System.Drawing.Size(53, 13);
            this.lStarDate.TabIndex = 22;
            this.lStarDate.Text = "Start date";
            // 
            // lEndDate
            // 
            this.lEndDate.AutoSize = true;
            this.lEndDate.Location = new System.Drawing.Point(240, 16);
            this.lEndDate.Name = "lEndDate";
            this.lEndDate.Size = new System.Drawing.Size(50, 13);
            this.lEndDate.TabIndex = 24;
            this.lEndDate.Text = "End date";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LInput);
            this.panel1.Location = new System.Drawing.Point(12, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 22);
            this.panel1.TabIndex = 1;
            // 
            // LInput
            // 
            this.LInput.AutoSize = true;
            this.LInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LInput.Location = new System.Drawing.Point(4, 3);
            this.LInput.Name = "LInput";
            this.LInput.Size = new System.Drawing.Size(39, 15);
            this.LInput.TabIndex = 0;
            this.LInput.Text = "Input";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.DtpCurrentDate);
            this.panel2.Controls.Add(this.BtCalcula);
            this.panel2.Controls.Add(this.LCurrentDate);
            this.panel2.Location = new System.Drawing.Point(12, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(478, 48);
            this.panel2.TabIndex = 2;
            // 
            // DtpCurrentDate
            // 
            this.DtpCurrentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpCurrentDate.Location = new System.Drawing.Point(109, 13);
            this.DtpCurrentDate.Name = "DtpCurrentDate";
            this.DtpCurrentDate.Size = new System.Drawing.Size(124, 20);
            this.DtpCurrentDate.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lConfiguration);
            this.panel3.Location = new System.Drawing.Point(12, 93);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(478, 22);
            this.panel3.TabIndex = 6;
            // 
            // lConfiguration
            // 
            this.lConfiguration.AutoSize = true;
            this.lConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lConfiguration.Location = new System.Drawing.Point(4, 3);
            this.lConfiguration.Name = "lConfiguration";
            this.lConfiguration.Size = new System.Drawing.Size(93, 15);
            this.lConfiguration.TabIndex = 7;
            this.lConfiguration.Text = "Configuration";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.DtpDateTime);
            this.panel4.Controls.Add(this.lday);
            this.panel4.Controls.Add(this.NumDays);
            this.panel4.Controls.Add(this.LType);
            this.panel4.Controls.Add(this.CbTypes);
            this.panel4.Controls.Add(this.CkActive);
            this.panel4.Controls.Add(this.lDateTime);
            this.panel4.Controls.Add(this.lOcurrs);
            this.panel4.Controls.Add(this.CbOcurrs);
            this.panel4.Controls.Add(this.lEvery);
            this.panel4.Location = new System.Drawing.Point(12, 117);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(478, 110);
            this.panel4.TabIndex = 8;
            // 
            // DtpDateTime
            // 
            this.DtpDateTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.DtpDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDateTime.Location = new System.Drawing.Point(109, 44);
            this.DtpDateTime.Name = "DtpDateTime";
            this.DtpDateTime.Size = new System.Drawing.Size(364, 20);
            this.DtpDateTime.TabIndex = 13;
            // 
            // lday
            // 
            this.lday.AutoSize = true;
            this.lday.Location = new System.Drawing.Point(352, 84);
            this.lday.Name = "lday";
            this.lday.Size = new System.Drawing.Size(35, 13);
            this.lday.TabIndex = 18;
            this.lday.Text = "day(s)";
            // 
            // NumDays
            // 
            this.NumDays.Enabled = false;
            this.NumDays.Location = new System.Drawing.Point(316, 80);
            this.NumDays.Name = "NumDays";
            this.NumDays.Size = new System.Drawing.Size(30, 20);
            this.NumDays.TabIndex = 17;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.llimits);
            this.panel5.Location = new System.Drawing.Point(12, 237);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(478, 22);
            this.panel5.TabIndex = 19;
            // 
            // llimits
            // 
            this.llimits.AutoSize = true;
            this.llimits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llimits.Location = new System.Drawing.Point(4, 3);
            this.llimits.Name = "llimits";
            this.llimits.Size = new System.Drawing.Size(46, 15);
            this.llimits.TabIndex = 20;
            this.llimits.Text = "Limits";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.DtpEndDate);
            this.panel6.Controls.Add(this.DtpStarDate);
            this.panel6.Controls.Add(this.lStarDate);
            this.panel6.Controls.Add(this.lEndDate);
            this.panel6.Location = new System.Drawing.Point(12, 261);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(478, 47);
            this.panel6.TabIndex = 21;
            // 
            // DtpEndDate
            // 
            this.DtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpEndDate.Location = new System.Drawing.Point(316, 13);
            this.DtpEndDate.Name = "DtpEndDate";
            this.DtpEndDate.Size = new System.Drawing.Size(124, 20);
            this.DtpEndDate.TabIndex = 25;
            // 
            // DtpStarDate
            // 
            this.DtpStarDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpStarDate.Location = new System.Drawing.Point(109, 13);
            this.DtpStarDate.Name = "DtpStarDate";
            this.DtpStarDate.Size = new System.Drawing.Size(124, 20);
            this.DtpStarDate.TabIndex = 23;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.TbDescription);
            this.panel7.Controls.Add(this.lDescription);
            this.panel7.Controls.Add(this.TbNextDate);
            this.panel7.Controls.Add(this.lNextDate);
            this.panel7.Location = new System.Drawing.Point(12, 341);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(478, 157);
            this.panel7.TabIndex = 28;
            // 
            // TbDescription
            // 
            this.TbDescription.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TbDescription.Location = new System.Drawing.Point(3, 73);
            this.TbDescription.Multiline = true;
            this.TbDescription.Name = "TbDescription";
            this.TbDescription.ReadOnly = true;
            this.TbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TbDescription.Size = new System.Drawing.Size(470, 78);
            this.TbDescription.TabIndex = 32;
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(4, 57);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(60, 13);
            this.lDescription.TabIndex = 31;
            this.lDescription.Text = "Description";
            // 
            // TbNextDate
            // 
            this.TbNextDate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TbNextDate.Location = new System.Drawing.Point(3, 31);
            this.TbNextDate.Name = "TbNextDate";
            this.TbNextDate.ReadOnly = true;
            this.TbNextDate.Size = new System.Drawing.Size(470, 20);
            this.TbNextDate.TabIndex = 30;
            // 
            // lNextDate
            // 
            this.lNextDate.AutoSize = true;
            this.lNextDate.Location = new System.Drawing.Point(2, 10);
            this.lNextDate.Name = "lNextDate";
            this.lNextDate.Size = new System.Drawing.Size(100, 13);
            this.lNextDate.TabIndex = 29;
            this.lNextDate.Text = "Next execution time";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.lOutput);
            this.panel8.Location = new System.Drawing.Point(12, 317);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(478, 22);
            this.panel8.TabIndex = 26;
            // 
            // lOutput
            // 
            this.lOutput.AutoSize = true;
            this.lOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lOutput.Location = new System.Drawing.Point(4, 3);
            this.lOutput.Name = "lOutput";
            this.lOutput.Size = new System.Drawing.Size(49, 15);
            this.lOutput.TabIndex = 27;
            this.lOutput.Text = "Output";
            // 
            // CalculatorNextDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(498, 499);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CalculatorNextDate";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scheduler";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumDays)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtCalcula;
        private System.Windows.Forms.Label LCurrentDate;
        private System.Windows.Forms.Label LType;
        private System.Windows.Forms.ComboBox CbTypes;
        private System.Windows.Forms.CheckBox CkActive;
        private System.Windows.Forms.Label lDateTime;
        private System.Windows.Forms.Label lOcurrs;
        private System.Windows.Forms.ComboBox CbOcurrs;
        private System.Windows.Forms.Label lEvery;
        private System.Windows.Forms.Label lStarDate;
        private System.Windows.Forms.Label lEndDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LInput;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lConfiguration;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label llimits;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox TbDescription;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.TextBox TbNextDate;
        private System.Windows.Forms.Label lNextDate;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lOutput;
        private System.Windows.Forms.Label lday;
        private System.Windows.Forms.NumericUpDown NumDays;
        private System.Windows.Forms.DateTimePicker DtpCurrentDate;
        private System.Windows.Forms.DateTimePicker DtpEndDate;
        private System.Windows.Forms.DateTimePicker DtpStarDate;
        private System.Windows.Forms.DateTimePicker DtpDateTime;
    }
}

