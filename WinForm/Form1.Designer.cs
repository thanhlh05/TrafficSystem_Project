namespace TrafficSystem_WinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlSaBan = new Panel();
            lblCountSouth = new Label();
            lblCountEast = new Label();
            lblCountWest = new Label();
            lblCountNorth = new Label();
            picIntersection = new PictureBox();
            grpLightEast = new GroupBox();
            lblEastYellow = new Label();
            lblEastGreen = new Label();
            lblEastRed = new Label();
            grpLightWest = new GroupBox();
            lblWestYellow = new Label();
            lblWestGreen = new Label();
            lblWestRed = new Label();
            grpLightSouth = new GroupBox();
            lblSouthYellow = new Label();
            lblSouthRed = new Label();
            lblSouthGreen = new Label();
            grpLightNorth = new GroupBox();
            lblNorthYellow = new Label();
            lblNorthRed = new Label();
            lblNorthGreen = new Label();
            lblStatusTag = new Label();
            grpControl = new GroupBox();
            pnlManualGroup = new Panel();
            btnEWY_NSR = new Button();
            btnEWG_NSR = new Button();
            btnNSY_EWR = new Button();
            btnNSG_EWR = new Button();
            btnModeEmergency = new Button();
            btnModeManual = new Button();
            btnModeAuto = new Button();
            grpConfig = new GroupBox();
            btnSaveConfig = new Button();
            nudRed = new NumericUpDown();
            nudYellow = new NumericUpDown();
            nudGreen = new NumericUpDown();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            grpLogs = new GroupBox();
            dgvLogs = new DataGridView();
            colTime = new DataGridViewTextBoxColumn();
            colEvent = new DataGridViewTextBoxColumn();
            colMode = new DataGridViewTextBoxColumn();
            colDetail = new DataGridViewTextBoxColumn();
            pnlSaBan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picIntersection).BeginInit();
            grpLightEast.SuspendLayout();
            grpLightWest.SuspendLayout();
            grpLightSouth.SuspendLayout();
            grpLightNorth.SuspendLayout();
            grpControl.SuspendLayout();
            pnlManualGroup.SuspendLayout();
            grpConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudYellow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudGreen).BeginInit();
            grpLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // pnlSaBan
            // 
            pnlSaBan.BackColor = Color.White;
            pnlSaBan.Controls.Add(lblCountSouth);
            pnlSaBan.Controls.Add(lblCountEast);
            pnlSaBan.Controls.Add(lblCountWest);
            pnlSaBan.Controls.Add(lblCountNorth);
            pnlSaBan.Controls.Add(picIntersection);
            pnlSaBan.Controls.Add(grpLightEast);
            pnlSaBan.Controls.Add(grpLightWest);
            pnlSaBan.Controls.Add(grpLightSouth);
            pnlSaBan.Controls.Add(grpLightNorth);
            pnlSaBan.Controls.Add(lblStatusTag);
            pnlSaBan.Location = new Point(15, 15);
            pnlSaBan.Name = "pnlSaBan";
            pnlSaBan.Size = new Size(550, 480);
            pnlSaBan.TabIndex = 0;
            // 
            // lblCountSouth
            // 
            lblCountSouth.AutoSize = true;
            lblCountSouth.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCountSouth.ForeColor = Color.DarkSlateGray;
            lblCountSouth.Location = new Point(321, 397);
            lblCountSouth.Name = "lblCountSouth";
            lblCountSouth.Size = new Size(27, 31);
            lblCountSouth.TabIndex = 9;
            lblCountSouth.Text = "0";
            // 
            // lblCountEast
            // 
            lblCountEast.AutoSize = true;
            lblCountEast.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCountEast.ForeColor = Color.DarkSlateGray;
            lblCountEast.Location = new Point(420, 273);
            lblCountEast.Name = "lblCountEast";
            lblCountEast.Size = new Size(27, 31);
            lblCountEast.TabIndex = 8;
            lblCountEast.Text = "0";
            // 
            // lblCountWest
            // 
            lblCountWest.AutoSize = true;
            lblCountWest.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCountWest.ForeColor = Color.DarkSlateGray;
            lblCountWest.Location = new Point(58, 273);
            lblCountWest.Name = "lblCountWest";
            lblCountWest.Size = new Size(27, 31);
            lblCountWest.TabIndex = 7;
            lblCountWest.Text = "0";
            // 
            // lblCountNorth
            // 
            lblCountNorth.AutoSize = true;
            lblCountNorth.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCountNorth.ForeColor = Color.DarkSlateGray;
            lblCountNorth.Location = new Point(321, 86);
            lblCountNorth.Name = "lblCountNorth";
            lblCountNorth.Size = new Size(27, 31);
            lblCountNorth.TabIndex = 6;
            lblCountNorth.Text = "0";
            // 
            // picIntersection
            // 
            picIntersection.Image = Properties.Resources.intersection_top;
            picIntersection.Location = new Point(196, 160);
            picIntersection.Name = "picIntersection";
            picIntersection.Size = new Size(160, 160);
            picIntersection.SizeMode = PictureBoxSizeMode.Zoom;
            picIntersection.TabIndex = 5;
            picIntersection.TabStop = false;
            // 
            // grpLightEast
            // 
            grpLightEast.Controls.Add(lblEastYellow);
            grpLightEast.Controls.Add(lblEastGreen);
            grpLightEast.Controls.Add(lblEastRed);
            grpLightEast.Location = new Point(390, 185);
            grpLightEast.Name = "grpLightEast";
            grpLightEast.Size = new Size(145, 85);
            grpLightEast.TabIndex = 4;
            grpLightEast.TabStop = false;
            grpLightEast.Text = "ĐÔNG";
            // 
            // lblEastYellow
            // 
            lblEastYellow.BackColor = Color.Gainsboro;
            lblEastYellow.Location = new Point(58, 35);
            lblEastYellow.Name = "lblEastYellow";
            lblEastYellow.Size = new Size(22, 22);
            lblEastYellow.TabIndex = 10;
            // 
            // lblEastGreen
            // 
            lblEastGreen.BackColor = Color.Gainsboro;
            lblEastGreen.Location = new Point(86, 35);
            lblEastGreen.Name = "lblEastGreen";
            lblEastGreen.Size = new Size(22, 22);
            lblEastGreen.TabIndex = 9;
            // 
            // lblEastRed
            // 
            lblEastRed.BackColor = Color.Gainsboro;
            lblEastRed.Location = new Point(30, 35);
            lblEastRed.Name = "lblEastRed";
            lblEastRed.Size = new Size(22, 22);
            lblEastRed.TabIndex = 8;
            // 
            // grpLightWest
            // 
            grpLightWest.Controls.Add(lblWestYellow);
            grpLightWest.Controls.Add(lblWestGreen);
            grpLightWest.Controls.Add(lblWestRed);
            grpLightWest.Location = new Point(40, 185);
            grpLightWest.Name = "grpLightWest";
            grpLightWest.Size = new Size(120, 85);
            grpLightWest.TabIndex = 3;
            grpLightWest.TabStop = false;
            grpLightWest.Text = "TÂY";
            // 
            // lblWestYellow
            // 
            lblWestYellow.BackColor = Color.Gainsboro;
            lblWestYellow.Location = new Point(47, 35);
            lblWestYellow.Name = "lblWestYellow";
            lblWestYellow.Size = new Size(22, 22);
            lblWestYellow.TabIndex = 13;
            // 
            // lblWestGreen
            // 
            lblWestGreen.BackColor = Color.Gainsboro;
            lblWestGreen.Location = new Point(75, 35);
            lblWestGreen.Name = "lblWestGreen";
            lblWestGreen.Size = new Size(22, 22);
            lblWestGreen.TabIndex = 12;
            // 
            // lblWestRed
            // 
            lblWestRed.BackColor = Color.Gainsboro;
            lblWestRed.Location = new Point(19, 35);
            lblWestRed.Name = "lblWestRed";
            lblWestRed.Size = new Size(22, 22);
            lblWestRed.TabIndex = 11;
            // 
            // grpLightSouth
            // 
            grpLightSouth.Controls.Add(lblSouthYellow);
            grpLightSouth.Controls.Add(lblSouthRed);
            grpLightSouth.Controls.Add(lblSouthGreen);
            grpLightSouth.Location = new Point(235, 330);
            grpLightSouth.Name = "grpLightSouth";
            grpLightSouth.Size = new Size(80, 130);
            grpLightSouth.TabIndex = 3;
            grpLightSouth.TabStop = false;
            grpLightSouth.Text = "NAM";
            // 
            // lblSouthYellow
            // 
            lblSouthYellow.BackColor = Color.Gainsboro;
            lblSouthYellow.Location = new Point(27, 76);
            lblSouthYellow.Name = "lblSouthYellow";
            lblSouthYellow.Size = new Size(22, 22);
            lblSouthYellow.TabIndex = 10;
            // 
            // lblSouthRed
            // 
            lblSouthRed.BackColor = Color.Gainsboro;
            lblSouthRed.Location = new Point(27, 50);
            lblSouthRed.Name = "lblSouthRed";
            lblSouthRed.Size = new Size(22, 22);
            lblSouthRed.TabIndex = 8;
            // 
            // lblSouthGreen
            // 
            lblSouthGreen.BackColor = Color.Gainsboro;
            lblSouthGreen.Location = new Point(27, 101);
            lblSouthGreen.Name = "lblSouthGreen";
            lblSouthGreen.Size = new Size(22, 22);
            lblSouthGreen.TabIndex = 9;
            // 
            // grpLightNorth
            // 
            grpLightNorth.Controls.Add(lblNorthYellow);
            grpLightNorth.Controls.Add(lblNorthRed);
            grpLightNorth.Controls.Add(lblNorthGreen);
            grpLightNorth.Location = new Point(235, 20);
            grpLightNorth.Name = "grpLightNorth";
            grpLightNorth.Size = new Size(80, 130);
            grpLightNorth.TabIndex = 2;
            grpLightNorth.TabStop = false;
            grpLightNorth.Text = "BẮC";
            // 
            // lblNorthYellow
            // 
            lblNorthYellow.BackColor = Color.Gainsboro;
            lblNorthYellow.Location = new Point(27, 75);
            lblNorthYellow.Name = "lblNorthYellow";
            lblNorthYellow.Size = new Size(22, 22);
            lblNorthYellow.TabIndex = 7;
            // 
            // lblNorthRed
            // 
            lblNorthRed.BackColor = Color.Gainsboro;
            lblNorthRed.Location = new Point(27, 49);
            lblNorthRed.Name = "lblNorthRed";
            lblNorthRed.Size = new Size(22, 22);
            lblNorthRed.TabIndex = 5;
            // 
            // lblNorthGreen
            // 
            lblNorthGreen.BackColor = Color.Gainsboro;
            lblNorthGreen.Location = new Point(27, 100);
            lblNorthGreen.Name = "lblNorthGreen";
            lblNorthGreen.Size = new Size(22, 22);
            lblNorthGreen.TabIndex = 6;
            // 
            // lblStatusTag
            // 
            lblStatusTag.BackColor = SystemColors.ActiveCaption;
            lblStatusTag.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatusTag.ForeColor = Color.Black;
            lblStatusTag.Location = new Point(350, 0);
            lblStatusTag.Name = "lblStatusTag";
            lblStatusTag.Size = new Size(200, 30);
            lblStatusTag.TabIndex = 1;
            lblStatusTag.Text = "CHÊ ĐỘ: TỰ ĐỘNG";
            lblStatusTag.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpControl
            // 
            grpControl.Controls.Add(pnlManualGroup);
            grpControl.Controls.Add(btnModeEmergency);
            grpControl.Controls.Add(btnModeManual);
            grpControl.Controls.Add(btnModeAuto);
            grpControl.Location = new Point(585, 15);
            grpControl.Name = "grpControl";
            grpControl.Size = new Size(385, 260);
            grpControl.TabIndex = 1;
            grpControl.TabStop = false;
            grpControl.Text = "ĐIỀU KHIỂN HỆ THỐNG";
            // 
            // pnlManualGroup
            // 
            pnlManualGroup.Controls.Add(btnEWY_NSR);
            pnlManualGroup.Controls.Add(btnEWG_NSR);
            pnlManualGroup.Controls.Add(btnNSY_EWR);
            pnlManualGroup.Controls.Add(btnNSG_EWR);
            pnlManualGroup.Enabled = false;
            pnlManualGroup.Location = new Point(20, 90);
            pnlManualGroup.Name = "pnlManualGroup";
            pnlManualGroup.Size = new Size(345, 150);
            pnlManualGroup.TabIndex = 3;
            // 
            // btnEWY_NSR
            // 
            btnEWY_NSR.BackColor = Color.LightGray;
            btnEWY_NSR.FlatStyle = FlatStyle.Flat;
            btnEWY_NSR.Location = new Point(20, 110);
            btnEWY_NSR.Name = "btnEWY_NSR";
            btnEWY_NSR.Size = new Size(310, 30);
            btnEWY_NSR.TabIndex = 3;
            btnEWY_NSR.Text = "Đông - Tây VÀNG / Bắc - Nam ĐỎ";
            btnEWY_NSR.UseVisualStyleBackColor = false;
            btnEWY_NSR.Click += btnEWY_NSR_Click;
            // 
            // btnEWG_NSR
            // 
            btnEWG_NSR.BackColor = Color.LightGray;
            btnEWG_NSR.FlatStyle = FlatStyle.Flat;
            btnEWG_NSR.Location = new Point(20, 75);
            btnEWG_NSR.Name = "btnEWG_NSR";
            btnEWG_NSR.Size = new Size(310, 30);
            btnEWG_NSR.TabIndex = 2;
            btnEWG_NSR.Text = "Đông - Tây XANH / Bắc - Nam ĐỎ";
            btnEWG_NSR.UseVisualStyleBackColor = false;
            btnEWG_NSR.Click += btnEWG_NSR_Click;
            // 
            // btnNSY_EWR
            // 
            btnNSY_EWR.BackColor = Color.LightGray;
            btnNSY_EWR.FlatStyle = FlatStyle.Flat;
            btnNSY_EWR.Location = new Point(20, 40);
            btnNSY_EWR.Name = "btnNSY_EWR";
            btnNSY_EWR.Size = new Size(310, 30);
            btnNSY_EWR.TabIndex = 1;
            btnNSY_EWR.Text = "Bắc - Nam VÀNG / Đông - Tây ĐỎ";
            btnNSY_EWR.UseVisualStyleBackColor = false;
            btnNSY_EWR.Click += btnNSY_EWR_Click;
            // 
            // btnNSG_EWR
            // 
            btnNSG_EWR.BackColor = Color.LightGray;
            btnNSG_EWR.FlatStyle = FlatStyle.Flat;
            btnNSG_EWR.Location = new Point(20, 5);
            btnNSG_EWR.Name = "btnNSG_EWR";
            btnNSG_EWR.Size = new Size(310, 30);
            btnNSG_EWR.TabIndex = 0;
            btnNSG_EWR.Text = "Bắc - Nam XANH / Đông - Tây ĐỎ";
            btnNSG_EWR.UseVisualStyleBackColor = false;
            btnNSG_EWR.Click += btnNSG_EWR_Click;
            // 
            // btnModeEmergency
            // 
            btnModeEmergency.BackColor = Color.FromArgb(231, 76, 60);
            btnModeEmergency.Cursor = Cursors.Cross;
            btnModeEmergency.ForeColor = Color.White;
            btnModeEmergency.Location = new Point(250, 30);
            btnModeEmergency.Name = "btnModeEmergency";
            btnModeEmergency.Size = new Size(115, 45);
            btnModeEmergency.TabIndex = 2;
            btnModeEmergency.Text = "KHẨN CẤP";
            btnModeEmergency.UseVisualStyleBackColor = false;
            // 
            // btnModeManual
            // 
            btnModeManual.BackColor = Color.FromArgb(52, 152, 219);
            btnModeManual.Cursor = Cursors.Cross;
            btnModeManual.ForeColor = Color.White;
            btnModeManual.Location = new Point(135, 30);
            btnModeManual.Name = "btnModeManual";
            btnModeManual.Size = new Size(110, 45);
            btnModeManual.TabIndex = 1;
            btnModeManual.Text = "THỦ CÔNG";
            btnModeManual.UseVisualStyleBackColor = false;
            btnModeManual.Click += btnModeManual_Click;
            // 
            // btnModeAuto
            // 
            btnModeAuto.BackColor = Color.FromArgb(46, 204, 113);
            btnModeAuto.Cursor = Cursors.Cross;
            btnModeAuto.ForeColor = Color.White;
            btnModeAuto.Location = new Point(20, 30);
            btnModeAuto.Name = "btnModeAuto";
            btnModeAuto.Size = new Size(110, 45);
            btnModeAuto.TabIndex = 0;
            btnModeAuto.Text = "TỰ ĐỘNG";
            btnModeAuto.UseVisualStyleBackColor = false;
            btnModeAuto.Click += btnModeAuto_Click;
            // 
            // grpConfig
            // 
            grpConfig.Controls.Add(btnSaveConfig);
            grpConfig.Controls.Add(nudRed);
            grpConfig.Controls.Add(nudYellow);
            grpConfig.Controls.Add(nudGreen);
            grpConfig.Controls.Add(label15);
            grpConfig.Controls.Add(label14);
            grpConfig.Controls.Add(label13);
            grpConfig.Location = new Point(585, 285);
            grpConfig.Name = "grpConfig";
            grpConfig.Size = new Size(385, 210);
            grpConfig.TabIndex = 2;
            grpConfig.TabStop = false;
            grpConfig.Text = "CẤU HÌNH THỜI GIAN";
            // 
            // btnSaveConfig
            // 
            btnSaveConfig.BackColor = Color.DarkSlateGray;
            btnSaveConfig.FlatStyle = FlatStyle.Flat;
            btnSaveConfig.ForeColor = Color.White;
            btnSaveConfig.Location = new Point(20, 160);
            btnSaveConfig.Name = "btnSaveConfig";
            btnSaveConfig.Size = new Size(345, 40);
            btnSaveConfig.TabIndex = 6;
            btnSaveConfig.Text = "LƯU VÀ ÁP DỤNG CẤU HÌNH";
            btnSaveConfig.UseVisualStyleBackColor = false;
            btnSaveConfig.Click += btnSaveConfig_Click;
            // 
            // nudRed
            // 
            nudRed.Location = new Point(170, 100);
            nudRed.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            nudRed.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudRed.Name = "nudRed";
            nudRed.Size = new Size(150, 30);
            nudRed.TabIndex = 5;
            nudRed.Value = new decimal(new int[] { 28, 0, 0, 0 });
            // 
            // nudYellow
            // 
            nudYellow.Location = new Point(170, 60);
            nudYellow.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudYellow.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudYellow.Name = "nudYellow";
            nudYellow.Size = new Size(150, 30);
            nudYellow.TabIndex = 4;
            nudYellow.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // nudGreen
            // 
            nudGreen.Location = new Point(170, 20);
            nudGreen.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            nudGreen.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudGreen.Name = "nudGreen";
            nudGreen.Size = new Size(150, 30);
            nudGreen.TabIndex = 3;
            nudGreen.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(5, 100);
            label15.Name = "label15";
            label15.Size = new Size(146, 23);
            label15.TabIndex = 2;
            label15.Text = "Thời gian đèn Đỏ:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(5, 60);
            label14.Name = "label14";
            label14.Size = new Size(163, 23);
            label14.TabIndex = 1;
            label14.Text = "Thời gian đèn Vàng:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(5, 20);
            label13.Name = "label13";
            label13.Size = new Size(163, 23);
            label13.TabIndex = 0;
            label13.Text = "Thời gian đèn Xanh:";
            // 
            // grpLogs
            // 
            grpLogs.Controls.Add(dgvLogs);
            grpLogs.Location = new Point(15, 510);
            grpLogs.Name = "grpLogs";
            grpLogs.Size = new Size(955, 135);
            grpLogs.TabIndex = 3;
            grpLogs.TabStop = false;
            grpLogs.Text = "LỊCH SỬ HOẠT ĐỘNG HỆ THỐNG";
            // 
            // dgvLogs
            // 
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.BackgroundColor = Color.White;
            dgvLogs.BorderStyle = BorderStyle.None;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Columns.AddRange(new DataGridViewColumn[] { colTime, colEvent, colMode, colDetail });
            dgvLogs.Dock = DockStyle.Fill;
            dgvLogs.GridColor = Color.LightGray;
            dgvLogs.Location = new Point(3, 26);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.RowHeadersWidth = 51;
            dgvLogs.Size = new Size(949, 106);
            dgvLogs.TabIndex = 0;
            // 
            // colTime
            // 
            colTime.HeaderText = "Thời gian";
            colTime.MinimumWidth = 6;
            colTime.Name = "colTime";
            // 
            // colEvent
            // 
            colEvent.HeaderText = "Sự kiện / Pha";
            colEvent.MinimumWidth = 6;
            colEvent.Name = "colEvent";
            // 
            // colMode
            // 
            colMode.HeaderText = "Chế độ";
            colMode.MinimumWidth = 6;
            colMode.Name = "colMode";
            // 
            // colDetail
            // 
            colDetail.HeaderText = "Chi tiết lệnh";
            colDetail.MinimumWidth = 6;
            colDetail.Name = "colDetail";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 249);
            ClientSize = new Size(982, 653);
            Controls.Add(grpLogs);
            Controls.Add(grpConfig);
            Controls.Add(grpControl);
            Controls.Add(pnlSaBan);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(33, 33, 33);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HỆ THỐNG ĐIỀU KHIỂN & GIÁM SÁT GIAO THÔNG THÔNG MINH";
            Load += Form1_Load;
            pnlSaBan.ResumeLayout(false);
            pnlSaBan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picIntersection).EndInit();
            grpLightEast.ResumeLayout(false);
            grpLightWest.ResumeLayout(false);
            grpLightSouth.ResumeLayout(false);
            grpLightNorth.ResumeLayout(false);
            grpControl.ResumeLayout(false);
            pnlManualGroup.ResumeLayout(false);
            grpConfig.ResumeLayout(false);
            grpConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRed).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudYellow).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudGreen).EndInit();
            grpLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSaBan;
        private Label lblStatusTag;
        private Label lblNorthGreen;
        private Label lblNorthRed;
        private GroupBox grpLightEast;
        private GroupBox grpLightWest;
        private GroupBox grpLightSouth;
        private GroupBox grpLightNorth;
        private Label lblEastYellow;
        private Label lblEastGreen;
        private Label lblEastRed;
        private Label lblWestYellow;
        private Label lblWestGreen;
        private Label lblWestRed;
        private Label lblSouthYellow;
        private Label lblSouthRed;
        private Label lblSouthGreen;
        private Label lblNorthYellow;
        private GroupBox grpControl;
        private Button btnModeEmergency;
        private Button btnModeManual;
        private Button btnModeAuto;
        private Panel pnlManualGroup;
        private Button btnEWY_NSR;
        private Button btnEWG_NSR;
        private Button btnNSY_EWR;
        private Button btnNSG_EWR;
        private GroupBox grpConfig;
        private Label label15;
        private Label label14;
        private Label label13;
        private NumericUpDown nudRed;
        private NumericUpDown nudYellow;
        private NumericUpDown nudGreen;
        private Button btnSaveConfig;
        private GroupBox grpLogs;
        private DataGridView dgvLogs;
        private DataGridViewTextBoxColumn colTime;
        private DataGridViewTextBoxColumn colEvent;
        private DataGridViewTextBoxColumn colMode;
        private DataGridViewTextBoxColumn colDetail;
        private PictureBox picIntersection;
        private Label lblCountSouth;
        private Label lblCountEast;
        private Label lblCountWest;
        private Label lblCountNorth;
    }
}
