
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
            this.lStarDate = new System.Windows.Forms.Label();
            this.lEndDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LInput = new System.Windows.Forms.Label();
            this.PInput = new System.Windows.Forms.Panel();
            this.DtpCurrentDate = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lConfiguration = new System.Windows.Forms.Label();
            this.PConfiguration = new System.Windows.Forms.Panel();
            this.DtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.panel5 = new System.Windows.Forms.Panel();
            this.llimits = new System.Windows.Forms.Label();
            this.PLimits = new System.Windows.Forms.Panel();
            this.DtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.DtpStarDate = new System.Windows.Forms.DateTimePicker();
            this.POutput = new System.Windows.Forms.Panel();
            this.TbDescription = new System.Windows.Forms.TextBox();
            this.lDescription = new System.Windows.Forms.Label();
            this.TbNextDate = new System.Windows.Forms.TextBox();
            this.lNextDate = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lOutput = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.LWeeklyConfiguration = new System.Windows.Forms.Label();
            this.PWeeklyConfiguration = new System.Windows.Forms.Panel();
            this.CkSunday = new System.Windows.Forms.CheckBox();
            this.CkSaturday = new System.Windows.Forms.CheckBox();
            this.CkFriday = new System.Windows.Forms.CheckBox();
            this.CkThursday = new System.Windows.Forms.CheckBox();
            this.CkWednesday = new System.Windows.Forms.CheckBox();
            this.CkTuesday = new System.Windows.Forms.CheckBox();
            this.CkMonday = new System.Windows.Forms.CheckBox();
            this.Lweek = new System.Windows.Forms.Label();
            this.NumWeeks = new System.Windows.Forms.NumericUpDown();
            this.LEvery = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PDailyFrecuency = new System.Windows.Forms.Panel();
            this.CbOcurrsFrecuency = new System.Windows.Forms.ComboBox();
            this.DtpEndFrecuency = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DtpStarFrecuency = new System.Windows.Forms.DateTimePicker();
            this.DtpTimeOnceFrecuency = new System.Windows.Forms.DateTimePicker();
            this.NumFrecuency = new System.Windows.Forms.NumericUpDown();
            this.CkEveryFrecuency = new System.Windows.Forms.CheckBox();
            this.CkOnceFrecuency = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.PInput.SuspendLayout();
            this.panel3.SuspendLayout();
            this.PConfiguration.SuspendLayout();
            this.panel5.SuspendLayout();
            this.PLimits.SuspendLayout();
            this.POutput.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.PWeeklyConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumWeeks)).BeginInit();
            this.panel11.SuspendLayout();
            this.PDailyFrecuency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumFrecuency)).BeginInit();
            this.SuspendLayout();
            // 
            // BtCalcula
            // 
            this.BtCalcula.Location = new System.Drawing.Point(221, 9);
            this.BtCalcula.Name = "BtCalcula";
            this.BtCalcula.Size = new System.Drawing.Size(275, 19);
            this.BtCalcula.TabIndex = 5;
            this.BtCalcula.Text = "Calculate next date";
            this.BtCalcula.UseVisualStyleBackColor = true;
            this.BtCalcula.Click += new System.EventHandler(this.BtCalcula_Click);
            // 
            // LCurrentDate
            // 
            this.LCurrentDate.AutoSize = true;
            this.LCurrentDate.Location = new System.Drawing.Point(2, 11);
            this.LCurrentDate.Name = "LCurrentDate";
            this.LCurrentDate.Size = new System.Drawing.Size(65, 13);
            this.LCurrentDate.TabIndex = 3;
            this.LCurrentDate.Text = "Current date";
            // 
            // LType
            // 
            this.LType.AutoSize = true;
            this.LType.Location = new System.Drawing.Point(2, 6);
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
            this.CbTypes.Location = new System.Drawing.Point(88, 4);
            this.CbTypes.Name = "CbTypes";
            this.CbTypes.Size = new System.Drawing.Size(127, 21);
            this.CbTypes.TabIndex = 7;
            this.CbTypes.Text = "Once";
            this.CbTypes.SelectedIndexChanged += new System.EventHandler(this.CbTypes_SelectedIndexChanged);
            // 
            // CkActive
            // 
            this.CkActive.AutoSize = true;
            this.CkActive.Checked = true;
            this.CkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CkActive.Location = new System.Drawing.Point(221, 6);
            this.CkActive.Name = "CkActive";
            this.CkActive.Size = new System.Drawing.Size(65, 17);
            this.CkActive.TabIndex = 11;
            this.CkActive.Text = "Enabled";
            this.CkActive.UseVisualStyleBackColor = true;
            // 
            // lDateTime
            // 
            this.lDateTime.AutoSize = true;
            this.lDateTime.Location = new System.Drawing.Point(2, 32);
            this.lDateTime.Name = "lDateTime";
            this.lDateTime.Size = new System.Drawing.Size(67, 13);
            this.lDateTime.TabIndex = 12;
            this.lDateTime.Text = "Once time at";
            // 
            // lOcurrs
            // 
            this.lOcurrs.AutoSize = true;
            this.lOcurrs.Location = new System.Drawing.Point(2, 55);
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
            "Daily",
            "Weekly"});
            this.CbOcurrs.Location = new System.Drawing.Point(88, 51);
            this.CbOcurrs.MaxDropDownItems = 1;
            this.CbOcurrs.Name = "CbOcurrs";
            this.CbOcurrs.Size = new System.Drawing.Size(408, 21);
            this.CbOcurrs.TabIndex = 15;
            this.CbOcurrs.Text = "Daily";
            this.CbOcurrs.SelectedIndexChanged += new System.EventHandler(this.CbOcurrs_SelectedIndexChanged);
            // 
            // lStarDate
            // 
            this.lStarDate.AutoSize = true;
            this.lStarDate.Location = new System.Drawing.Point(2, 7);
            this.lStarDate.Name = "lStarDate";
            this.lStarDate.Size = new System.Drawing.Size(53, 13);
            this.lStarDate.TabIndex = 22;
            this.lStarDate.Text = "Start date";
            // 
            // lEndDate
            // 
            this.lEndDate.AutoSize = true;
            this.lEndDate.Location = new System.Drawing.Point(242, 7);
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
            this.panel1.Location = new System.Drawing.Point(12, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(504, 18);
            this.panel1.TabIndex = 1;
            // 
            // LInput
            // 
            this.LInput.AutoSize = true;
            this.LInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LInput.Location = new System.Drawing.Point(4, -1);
            this.LInput.Name = "LInput";
            this.LInput.Size = new System.Drawing.Size(39, 15);
            this.LInput.TabIndex = 0;
            this.LInput.Text = "Input";
            // 
            // PInput
            // 
            this.PInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PInput.Controls.Add(this.DtpCurrentDate);
            this.PInput.Controls.Add(this.BtCalcula);
            this.PInput.Controls.Add(this.LCurrentDate);
            this.PInput.Location = new System.Drawing.Point(12, 25);
            this.PInput.Name = "PInput";
            this.PInput.Size = new System.Drawing.Size(504, 37);
            this.PInput.TabIndex = 2;
            // 
            // DtpCurrentDate
            // 
            this.DtpCurrentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpCurrentDate.Location = new System.Drawing.Point(88, 7);
            this.DtpCurrentDate.Name = "DtpCurrentDate";
            this.DtpCurrentDate.Size = new System.Drawing.Size(127, 20);
            this.DtpCurrentDate.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lConfiguration);
            this.panel3.Location = new System.Drawing.Point(12, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(504, 18);
            this.panel3.TabIndex = 6;
            // 
            // lConfiguration
            // 
            this.lConfiguration.AutoSize = true;
            this.lConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lConfiguration.Location = new System.Drawing.Point(4, 0);
            this.lConfiguration.Name = "lConfiguration";
            this.lConfiguration.Size = new System.Drawing.Size(93, 15);
            this.lConfiguration.TabIndex = 7;
            this.lConfiguration.Text = "Configuration";
            // 
            // PConfiguration
            // 
            this.PConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PConfiguration.Controls.Add(this.DtpDateTime);
            this.PConfiguration.Controls.Add(this.LType);
            this.PConfiguration.Controls.Add(this.CbTypes);
            this.PConfiguration.Controls.Add(this.CkActive);
            this.PConfiguration.Controls.Add(this.lDateTime);
            this.PConfiguration.Controls.Add(this.lOcurrs);
            this.PConfiguration.Controls.Add(this.CbOcurrs);
            this.PConfiguration.Location = new System.Drawing.Point(12, 91);
            this.PConfiguration.Name = "PConfiguration";
            this.PConfiguration.Size = new System.Drawing.Size(504, 78);
            this.PConfiguration.TabIndex = 8;
            // 
            // DtpDateTime
            // 
            this.DtpDateTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.DtpDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDateTime.Location = new System.Drawing.Point(88, 28);
            this.DtpDateTime.Name = "DtpDateTime";
            this.DtpDateTime.Size = new System.Drawing.Size(408, 20);
            this.DtpDateTime.TabIndex = 13;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.llimits);
            this.panel5.Location = new System.Drawing.Point(12, 356);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(504, 18);
            this.panel5.TabIndex = 19;
            // 
            // llimits
            // 
            this.llimits.AutoSize = true;
            this.llimits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llimits.Location = new System.Drawing.Point(4, 0);
            this.llimits.Name = "llimits";
            this.llimits.Size = new System.Drawing.Size(46, 15);
            this.llimits.TabIndex = 20;
            this.llimits.Text = "Limits";
            // 
            // PLimits
            // 
            this.PLimits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PLimits.Controls.Add(this.DtpEndDate);
            this.PLimits.Controls.Add(this.DtpStarDate);
            this.PLimits.Controls.Add(this.lStarDate);
            this.PLimits.Controls.Add(this.lEndDate);
            this.PLimits.Location = new System.Drawing.Point(12, 376);
            this.PLimits.Name = "PLimits";
            this.PLimits.Size = new System.Drawing.Size(504, 29);
            this.PLimits.TabIndex = 21;
            // 
            // DtpEndDate
            // 
            this.DtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpEndDate.Location = new System.Drawing.Point(318, 4);
            this.DtpEndDate.Name = "DtpEndDate";
            this.DtpEndDate.Size = new System.Drawing.Size(104, 20);
            this.DtpEndDate.TabIndex = 25;
            // 
            // DtpStarDate
            // 
            this.DtpStarDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpStarDate.Location = new System.Drawing.Point(111, 4);
            this.DtpStarDate.Name = "DtpStarDate";
            this.DtpStarDate.Size = new System.Drawing.Size(104, 20);
            this.DtpStarDate.TabIndex = 23;
            // 
            // POutput
            // 
            this.POutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.POutput.Controls.Add(this.TbDescription);
            this.POutput.Controls.Add(this.lDescription);
            this.POutput.Controls.Add(this.TbNextDate);
            this.POutput.Controls.Add(this.lNextDate);
            this.POutput.Location = new System.Drawing.Point(12, 431);
            this.POutput.Name = "POutput";
            this.POutput.Size = new System.Drawing.Size(504, 111);
            this.POutput.TabIndex = 28;
            // 
            // TbDescription
            // 
            this.TbDescription.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TbDescription.Location = new System.Drawing.Point(3, 57);
            this.TbDescription.Multiline = true;
            this.TbDescription.Name = "TbDescription";
            this.TbDescription.ReadOnly = true;
            this.TbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TbDescription.Size = new System.Drawing.Size(493, 49);
            this.TbDescription.TabIndex = 32;
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.Location = new System.Drawing.Point(2, 42);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(60, 13);
            this.lDescription.TabIndex = 31;
            this.lDescription.Text = "Description";
            // 
            // TbNextDate
            // 
            this.TbNextDate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TbNextDate.Location = new System.Drawing.Point(3, 17);
            this.TbNextDate.Name = "TbNextDate";
            this.TbNextDate.ReadOnly = true;
            this.TbNextDate.Size = new System.Drawing.Size(493, 20);
            this.TbNextDate.TabIndex = 30;
            // 
            // lNextDate
            // 
            this.lNextDate.AutoSize = true;
            this.lNextDate.Location = new System.Drawing.Point(2, 2);
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
            this.panel8.Location = new System.Drawing.Point(12, 409);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(504, 18);
            this.panel8.TabIndex = 26;
            // 
            // lOutput
            // 
            this.lOutput.AutoSize = true;
            this.lOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lOutput.Location = new System.Drawing.Point(4, 0);
            this.lOutput.Name = "lOutput";
            this.lOutput.Size = new System.Drawing.Size(49, 15);
            this.lOutput.TabIndex = 27;
            this.lOutput.Text = "Output";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.LWeeklyConfiguration);
            this.panel9.Location = new System.Drawing.Point(12, 175);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(504, 18);
            this.panel9.TabIndex = 32;
            // 
            // LWeeklyConfiguration
            // 
            this.LWeeklyConfiguration.AutoSize = true;
            this.LWeeklyConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LWeeklyConfiguration.Location = new System.Drawing.Point(4, 0);
            this.LWeeklyConfiguration.Name = "LWeeklyConfiguration";
            this.LWeeklyConfiguration.Size = new System.Drawing.Size(142, 15);
            this.LWeeklyConfiguration.TabIndex = 7;
            this.LWeeklyConfiguration.Text = "Weekly Configuration";
            // 
            // PWeeklyConfiguration
            // 
            this.PWeeklyConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PWeeklyConfiguration.Controls.Add(this.CkSunday);
            this.PWeeklyConfiguration.Controls.Add(this.CkSaturday);
            this.PWeeklyConfiguration.Controls.Add(this.CkFriday);
            this.PWeeklyConfiguration.Controls.Add(this.CkThursday);
            this.PWeeklyConfiguration.Controls.Add(this.CkWednesday);
            this.PWeeklyConfiguration.Controls.Add(this.CkTuesday);
            this.PWeeklyConfiguration.Controls.Add(this.CkMonday);
            this.PWeeklyConfiguration.Controls.Add(this.Lweek);
            this.PWeeklyConfiguration.Controls.Add(this.NumWeeks);
            this.PWeeklyConfiguration.Controls.Add(this.LEvery);
            this.PWeeklyConfiguration.Enabled = false;
            this.PWeeklyConfiguration.Location = new System.Drawing.Point(11, 196);
            this.PWeeklyConfiguration.Name = "PWeeklyConfiguration";
            this.PWeeklyConfiguration.Size = new System.Drawing.Size(506, 49);
            this.PWeeklyConfiguration.TabIndex = 42;
            // 
            // CkSunday
            // 
            this.CkSunday.AutoSize = true;
            this.CkSunday.Location = new System.Drawing.Point(439, 27);
            this.CkSunday.Name = "CkSunday";
            this.CkSunday.Size = new System.Drawing.Size(62, 17);
            this.CkSunday.TabIndex = 51;
            this.CkSunday.Text = "Sunday";
            this.CkSunday.UseVisualStyleBackColor = true;
            // 
            // CkSaturday
            // 
            this.CkSaturday.AutoSize = true;
            this.CkSaturday.Location = new System.Drawing.Point(367, 27);
            this.CkSaturday.Name = "CkSaturday";
            this.CkSaturday.Size = new System.Drawing.Size(68, 17);
            this.CkSaturday.TabIndex = 50;
            this.CkSaturday.Text = "Saturday";
            this.CkSaturday.UseVisualStyleBackColor = true;
            // 
            // CkFriday
            // 
            this.CkFriday.AutoSize = true;
            this.CkFriday.Location = new System.Drawing.Point(307, 27);
            this.CkFriday.Name = "CkFriday";
            this.CkFriday.Size = new System.Drawing.Size(54, 17);
            this.CkFriday.TabIndex = 49;
            this.CkFriday.Text = "Friday";
            this.CkFriday.UseVisualStyleBackColor = true;
            // 
            // CkThursday
            // 
            this.CkThursday.AutoSize = true;
            this.CkThursday.Location = new System.Drawing.Point(232, 27);
            this.CkThursday.Name = "CkThursday";
            this.CkThursday.Size = new System.Drawing.Size(70, 17);
            this.CkThursday.TabIndex = 48;
            this.CkThursday.Text = "Thursday";
            this.CkThursday.UseVisualStyleBackColor = true;
            // 
            // CkWednesday
            // 
            this.CkWednesday.AutoSize = true;
            this.CkWednesday.Location = new System.Drawing.Point(145, 27);
            this.CkWednesday.Name = "CkWednesday";
            this.CkWednesday.Size = new System.Drawing.Size(83, 17);
            this.CkWednesday.TabIndex = 47;
            this.CkWednesday.Text = "Wednesday";
            this.CkWednesday.UseVisualStyleBackColor = true;
            // 
            // CkTuesday
            // 
            this.CkTuesday.AutoSize = true;
            this.CkTuesday.Location = new System.Drawing.Point(77, 27);
            this.CkTuesday.Name = "CkTuesday";
            this.CkTuesday.Size = new System.Drawing.Size(67, 17);
            this.CkTuesday.TabIndex = 46;
            this.CkTuesday.Text = "Tuesday";
            this.CkTuesday.UseVisualStyleBackColor = true;
            // 
            // CkMonday
            // 
            this.CkMonday.AutoSize = true;
            this.CkMonday.Location = new System.Drawing.Point(5, 27);
            this.CkMonday.Name = "CkMonday";
            this.CkMonday.Size = new System.Drawing.Size(64, 17);
            this.CkMonday.TabIndex = 45;
            this.CkMonday.Text = "Monday";
            this.CkMonday.UseVisualStyleBackColor = true;
            // 
            // Lweek
            // 
            this.Lweek.AutoSize = true;
            this.Lweek.Location = new System.Drawing.Point(229, 6);
            this.Lweek.Name = "Lweek";
            this.Lweek.Size = new System.Drawing.Size(44, 13);
            this.Lweek.TabIndex = 44;
            this.Lweek.Text = "week(s)";
            // 
            // NumWeeks
            // 
            this.NumWeeks.Location = new System.Drawing.Point(89, 3);
            this.NumWeeks.Name = "NumWeeks";
            this.NumWeeks.Size = new System.Drawing.Size(127, 20);
            this.NumWeeks.TabIndex = 43;
            // 
            // LEvery
            // 
            this.LEvery.AutoSize = true;
            this.LEvery.Location = new System.Drawing.Point(2, 6);
            this.LEvery.Name = "LEvery";
            this.LEvery.Size = new System.Drawing.Size(34, 13);
            this.LEvery.TabIndex = 42;
            this.LEvery.Text = "Every";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.label1);
            this.panel11.Location = new System.Drawing.Point(11, 250);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(504, 18);
            this.panel11.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Daily Frecuency";
            // 
            // PDailyFrecuency
            // 
            this.PDailyFrecuency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PDailyFrecuency.Controls.Add(this.CbOcurrsFrecuency);
            this.PDailyFrecuency.Controls.Add(this.DtpEndFrecuency);
            this.PDailyFrecuency.Controls.Add(this.label2);
            this.PDailyFrecuency.Controls.Add(this.DtpStarFrecuency);
            this.PDailyFrecuency.Controls.Add(this.DtpTimeOnceFrecuency);
            this.PDailyFrecuency.Controls.Add(this.NumFrecuency);
            this.PDailyFrecuency.Controls.Add(this.CkEveryFrecuency);
            this.PDailyFrecuency.Controls.Add(this.CkOnceFrecuency);
            this.PDailyFrecuency.Controls.Add(this.label3);
            this.PDailyFrecuency.Enabled = false;
            this.PDailyFrecuency.Location = new System.Drawing.Point(11, 272);
            this.PDailyFrecuency.Name = "PDailyFrecuency";
            this.PDailyFrecuency.Size = new System.Drawing.Size(506, 82);
            this.PDailyFrecuency.TabIndex = 44;
            // 
            // CbOcurrsFrecuency
            // 
            this.CbOcurrsFrecuency.Enabled = false;
            this.CbOcurrsFrecuency.FormattingEnabled = true;
            this.CbOcurrsFrecuency.ItemHeight = 13;
            this.CbOcurrsFrecuency.Items.AddRange(new object[] {
            "Hours",
            "Minutes",
            "Seconds"});
            this.CbOcurrsFrecuency.Location = new System.Drawing.Point(246, 33);
            this.CbOcurrsFrecuency.Name = "CbOcurrsFrecuency";
            this.CbOcurrsFrecuency.Size = new System.Drawing.Size(177, 21);
            this.CbOcurrsFrecuency.TabIndex = 52;
            this.CbOcurrsFrecuency.Text = "Hours";
            // 
            // DtpEndFrecuency
            // 
            this.DtpEndFrecuency.CustomFormat = "HH:mm:ss";
            this.DtpEndFrecuency.Enabled = false;
            this.DtpEndFrecuency.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpEndFrecuency.Location = new System.Drawing.Point(319, 57);
            this.DtpEndFrecuency.Name = "DtpEndFrecuency";
            this.DtpEndFrecuency.ShowUpDown = true;
            this.DtpEndFrecuency.Size = new System.Drawing.Size(104, 20);
            this.DtpEndFrecuency.TabIndex = 51;
            this.DtpEndFrecuency.Value = new System.DateTime(2021, 10, 13, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "End at";
            // 
            // DtpStarFrecuency
            // 
            this.DtpStarFrecuency.CustomFormat = "HH:mm:ss";
            this.DtpStarFrecuency.Enabled = false;
            this.DtpStarFrecuency.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpStarFrecuency.Location = new System.Drawing.Point(112, 57);
            this.DtpStarFrecuency.Name = "DtpStarFrecuency";
            this.DtpStarFrecuency.ShowUpDown = true;
            this.DtpStarFrecuency.Size = new System.Drawing.Size(104, 20);
            this.DtpStarFrecuency.TabIndex = 49;
            this.DtpStarFrecuency.Value = new System.DateTime(2021, 10, 13, 0, 0, 0, 0);
            // 
            // DtpTimeOnceFrecuency
            // 
            this.DtpTimeOnceFrecuency.CustomFormat = "HH:mm:ss";
            this.DtpTimeOnceFrecuency.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTimeOnceFrecuency.Location = new System.Drawing.Point(112, 8);
            this.DtpTimeOnceFrecuency.Name = "DtpTimeOnceFrecuency";
            this.DtpTimeOnceFrecuency.ShowUpDown = true;
            this.DtpTimeOnceFrecuency.Size = new System.Drawing.Size(104, 20);
            this.DtpTimeOnceFrecuency.TabIndex = 48;
            this.DtpTimeOnceFrecuency.Value = new System.DateTime(2021, 10, 13, 0, 0, 0, 0);
            // 
            // NumFrecuency
            // 
            this.NumFrecuency.Enabled = false;
            this.NumFrecuency.Location = new System.Drawing.Point(112, 32);
            this.NumFrecuency.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NumFrecuency.Name = "NumFrecuency";
            this.NumFrecuency.Size = new System.Drawing.Size(104, 20);
            this.NumFrecuency.TabIndex = 47;
            // 
            // CkEveryFrecuency
            // 
            this.CkEveryFrecuency.AutoSize = true;
            this.CkEveryFrecuency.Location = new System.Drawing.Point(5, 34);
            this.CkEveryFrecuency.Name = "CkEveryFrecuency";
            this.CkEveryFrecuency.Size = new System.Drawing.Size(86, 17);
            this.CkEveryFrecuency.TabIndex = 46;
            this.CkEveryFrecuency.Text = "Ocurrs every";
            this.CkEveryFrecuency.UseVisualStyleBackColor = true;
            this.CkEveryFrecuency.CheckedChanged += new System.EventHandler(this.CkEveryFrecuency_CheckedChanged);
            // 
            // CkOnceFrecuency
            // 
            this.CkOnceFrecuency.AutoSize = true;
            this.CkOnceFrecuency.Checked = true;
            this.CkOnceFrecuency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CkOnceFrecuency.Location = new System.Drawing.Point(5, 11);
            this.CkOnceFrecuency.Name = "CkOnceFrecuency";
            this.CkOnceFrecuency.Size = new System.Drawing.Size(96, 17);
            this.CkOnceFrecuency.TabIndex = 45;
            this.CkOnceFrecuency.Text = "Ocurrs once at";
            this.CkOnceFrecuency.UseVisualStyleBackColor = true;
            this.CkOnceFrecuency.CheckedChanged += new System.EventHandler(this.CkOnceFrecuency_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Starting at ";
            // 
            // CalculatorNextDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(521, 543);
            this.Controls.Add(this.PDailyFrecuency);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.PWeeklyConfiguration);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.POutput);
            this.Controls.Add(this.PLimits);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.PConfiguration);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.PInput);
            this.Controls.Add(this.panel1);
            this.Name = "CalculatorNextDate";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scheduler";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PInput.ResumeLayout(false);
            this.PInput.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.PConfiguration.ResumeLayout(false);
            this.PConfiguration.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.PLimits.ResumeLayout(false);
            this.PLimits.PerformLayout();
            this.POutput.ResumeLayout(false);
            this.POutput.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.PWeeklyConfiguration.ResumeLayout(false);
            this.PWeeklyConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumWeeks)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.PDailyFrecuency.ResumeLayout(false);
            this.PDailyFrecuency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumFrecuency)).EndInit();
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
        private System.Windows.Forms.Label lStarDate;
        private System.Windows.Forms.Label lEndDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LInput;
        private System.Windows.Forms.Panel PInput;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lConfiguration;
        private System.Windows.Forms.Panel PConfiguration;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label llimits;
        private System.Windows.Forms.Panel PLimits;
        private System.Windows.Forms.Panel POutput;
        private System.Windows.Forms.TextBox TbDescription;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.TextBox TbNextDate;
        private System.Windows.Forms.Label lNextDate;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lOutput;
        private System.Windows.Forms.DateTimePicker DtpCurrentDate;
        private System.Windows.Forms.DateTimePicker DtpEndDate;
        private System.Windows.Forms.DateTimePicker DtpStarDate;
        private System.Windows.Forms.DateTimePicker DtpDateTime;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label LWeeklyConfiguration;
        private System.Windows.Forms.Panel PWeeklyConfiguration;
        private System.Windows.Forms.Label Lweek;
        private System.Windows.Forms.NumericUpDown NumWeeks;
        private System.Windows.Forms.Label LEvery;
        private System.Windows.Forms.CheckBox CkMonday;
        private System.Windows.Forms.CheckBox CkSunday;
        private System.Windows.Forms.CheckBox CkSaturday;
        private System.Windows.Forms.CheckBox CkFriday;
        private System.Windows.Forms.CheckBox CkThursday;
        private System.Windows.Forms.CheckBox CkWednesday;
        private System.Windows.Forms.CheckBox CkTuesday;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PDailyFrecuency;
        private System.Windows.Forms.CheckBox CkEveryFrecuency;
        private System.Windows.Forms.CheckBox CkOnceFrecuency;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NumFrecuency;
        private System.Windows.Forms.DateTimePicker DtpTimeOnceFrecuency;
        private System.Windows.Forms.DateTimePicker DtpEndFrecuency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DtpStarFrecuency;
        private System.Windows.Forms.ComboBox CbOcurrsFrecuency;
    }
}

