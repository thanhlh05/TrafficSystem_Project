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
            lblCountdown = new Label();
            lblStatusTag = new Label();
            grpLightNorth = new GroupBox();
            grpLightWest = new GroupBox();
            grpLightSouth = new GroupBox();
            grpLightEast = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            grpControl = new GroupBox();
            btnModeAuto = new Button();
            btnModeManual = new Button();
            btnModeEmergency = new Button();
            pnlManualGroup = new Panel();
            btnNSG_EWR = new Button();
            btnNSY_EWR = new Button();
            btnEWG_NSR = new Button();
            btnEWY_NSR = new Button();
            grpConfig = new GroupBox();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            numGreenTime = new NumericUpDown();
            numYellowTime = new NumericUpDown();
            numRedTime = new NumericUpDown();
            btnSaveConfig = new Button();
            grpLogs = new GroupBox();
            dgvLogs = new DataGridView();
            colTime = new DataGridViewTextBoxColumn();
            colEvent = new DataGridViewTextBoxColumn();
            colMode = new DataGridViewTextBoxColumn();
            colDetail = new DataGridViewTextBoxColumn();
            pnlSaBan.SuspendLayout();
            grpLightNorth.SuspendLayout();
            grpLightWest.SuspendLayout();
            grpLightSouth.SuspendLayout();
            grpLightEast.SuspendLayout();
            grpControl.SuspendLayout();
            pnlManualGroup.SuspendLayout();
            grpConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numGreenTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYellowTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRedTime).BeginInit();
            grpLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // pnlSaBan
            // 
            pnlSaBan.BackColor = Color.White;
            pnlSaBan.Controls.Add(grpLightEast);
            pnlSaBan.Controls.Add(grpLightWest);
            pnlSaBan.Controls.Add(grpLightSouth);
            pnlSaBan.Controls.Add(grpLightNorth);
            pnlSaBan.Controls.Add(lblStatusTag);
            pnlSaBan.Controls.Add(lblCountdown);
            pnlSaBan.Location = new Point(15, 15);
            pnlSaBan.Name = "pnlSaBan";
            pnlSaBan.Size = new Size(550, 480);
            pnlSaBan.TabIndex = 0;
            // 
            // lblCountdown
            // 
            lblCountdown.Font = new Font("Consolas", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCountdown.ForeColor = Color.DimGray;
            lblCountdown.Location = new Point(215, 220);
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Size = new Size(120, 50);
            lblCountdown.TabIndex = 0;
            lblCountdown.Text = "25";
            lblCountdown.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatusTag
            // 
            lblStatusTag.BackColor = SystemColors.ActiveCaption;
            lblStatusTag.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatusTag.ForeColor = Color.Black;
            lblStatusTag.Location = new Point(175, 175);
            lblStatusTag.Name = "lblStatusTag";
            lblStatusTag.Size = new Size(200, 30);
            lblStatusTag.TabIndex = 1;
            lblStatusTag.Text = "CHÊ ĐỘ: TỰ ĐỘNG";
            lblStatusTag.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpLightNorth
            // 
            grpLightNorth.Controls.Add(label3);
            grpLightNorth.Controls.Add(label1);
            grpLightNorth.Controls.Add(label2);
            grpLightNorth.Location = new Point(235, 20);
            grpLightNorth.Name = "grpLightNorth";
            grpLightNorth.Size = new Size(80, 130);
            grpLightNorth.TabIndex = 2;
            grpLightNorth.TabStop = false;
            grpLightNorth.Text = "HƯỚNG BẮC";
            // 
            // grpLightWest
            // 
            grpLightWest.Controls.Add(label10);
            grpLightWest.Controls.Add(label11);
            grpLightWest.Controls.Add(label12);
            grpLightWest.Location = new Point(40, 185);
            grpLightWest.Name = "grpLightWest";
            grpLightWest.Size = new Size(120, 85);
            grpLightWest.TabIndex = 3;
            grpLightWest.TabStop = false;
            grpLightWest.Text = "HƯỚNG TÂY";
            // 
            // grpLightSouth
            // 
            grpLightSouth.Controls.Add(label4);
            grpLightSouth.Controls.Add(label5);
            grpLightSouth.Controls.Add(label6);
            grpLightSouth.Location = new Point(235, 330);
            grpLightSouth.Name = "grpLightSouth";
            grpLightSouth.Size = new Size(80, 130);
            grpLightSouth.TabIndex = 3;
            grpLightSouth.TabStop = false;
            grpLightSouth.Text = "HƯỚNG NAM";
            // 
            // grpLightEast
            // 
            grpLightEast.Controls.Add(label9);
            grpLightEast.Controls.Add(label8);
            grpLightEast.Controls.Add(label7);
            grpLightEast.Location = new Point(390, 185);
            grpLightEast.Name = "grpLightEast";
            grpLightEast.Size = new Size(145, 85);
            grpLightEast.TabIndex = 4;
            grpLightEast.TabStop = false;
            grpLightEast.Text = "HƯỚNG ĐÔNG";
            // 
            // label1
            // 
            label1.BackColor = Color.Gainsboro;
            label1.Location = new Point(27, 49);
            label1.Name = "label1";
            label1.Size = new Size(22, 22);
            label1.TabIndex = 5;
            // 
            // label2
            // 
            label2.BackColor = Color.Gainsboro;
            label2.Location = new Point(27, 100);
            label2.Name = "label2";
            label2.Size = new Size(22, 22);
            label2.TabIndex = 6;
            // 
            // label3
            // 
            label3.BackColor = Color.Gainsboro;
            label3.Location = new Point(27, 75);
            label3.Name = "label3";
            label3.Size = new Size(22, 22);
            label3.TabIndex = 7;
            // 
            // label4
            // 
            label4.BackColor = Color.Gainsboro;
            label4.Location = new Point(27, 76);
            label4.Name = "label4";
            label4.Size = new Size(22, 22);
            label4.TabIndex = 10;
            // 
            // label5
            // 
            label5.BackColor = Color.Gainsboro;
            label5.Location = new Point(27, 50);
            label5.Name = "label5";
            label5.Size = new Size(22, 22);
            label5.TabIndex = 8;
            // 
            // label6
            // 
            label6.BackColor = Color.Gainsboro;
            label6.Location = new Point(27, 101);
            label6.Name = "label6";
            label6.Size = new Size(22, 22);
            label6.TabIndex = 9;
            // 
            // label7
            // 
            label7.BackColor = Color.Gainsboro;
            label7.Location = new Point(30, 35);
            label7.Name = "label7";
            label7.Size = new Size(22, 22);
            label7.TabIndex = 8;
            // 
            // label8
            // 
            label8.BackColor = Color.Gainsboro;
            label8.Location = new Point(86, 35);
            label8.Name = "label8";
            label8.Size = new Size(22, 22);
            label8.TabIndex = 9;
            // 
            // label9
            // 
            label9.BackColor = Color.Gainsboro;
            label9.Location = new Point(58, 35);
            label9.Name = "label9";
            label9.Size = new Size(22, 22);
            label9.TabIndex = 10;
            // 
            // label10
            // 
            label10.BackColor = Color.Gainsboro;
            label10.Location = new Point(47, 35);
            label10.Name = "label10";
            label10.Size = new Size(22, 22);
            label10.TabIndex = 13;
            // 
            // label11
            // 
            label11.BackColor = Color.Gainsboro;
            label11.Location = new Point(75, 35);
            label11.Name = "label11";
            label11.Size = new Size(22, 22);
            label11.TabIndex = 12;
            // 
            // label12
            // 
            label12.BackColor = Color.Gainsboro;
            label12.Location = new Point(19, 35);
            label12.Name = "label12";
            label12.Size = new Size(22, 22);
            label12.TabIndex = 11;
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
            // 
            // grpConfig
            // 
            grpConfig.Controls.Add(btnSaveConfig);
            grpConfig.Controls.Add(numRedTime);
            grpConfig.Controls.Add(numYellowTime);
            grpConfig.Controls.Add(numGreenTime);
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
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(5, 20);
            label13.Name = "label13";
            label13.Size = new Size(163, 23);
            label13.TabIndex = 0;
            label13.Text = "Thời gian đèn Xanh:";
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
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(5, 100);
            label15.Name = "label15";
            label15.Size = new Size(146, 23);
            label15.TabIndex = 2;
            label15.Text = "Thời gian đèn Đỏ:";
            // 
            // numGreenTime
            // 
            numGreenTime.Location = new Point(170, 20);
            numGreenTime.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            numGreenTime.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numGreenTime.Name = "numGreenTime";
            numGreenTime.Size = new Size(150, 30);
            numGreenTime.TabIndex = 3;
            numGreenTime.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // numYellowTime
            // 
            numYellowTime.Location = new Point(170, 60);
            numYellowTime.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numYellowTime.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numYellowTime.Name = "numYellowTime";
            numYellowTime.Size = new Size(150, 30);
            numYellowTime.TabIndex = 4;
            numYellowTime.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // numRedTime
            // 
            numRedTime.Location = new Point(170, 100);
            numRedTime.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            numRedTime.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numRedTime.Name = "numRedTime";
            numRedTime.Size = new Size(150, 30);
            numRedTime.TabIndex = 5;
            numRedTime.Value = new decimal(new int[] { 28, 0, 0, 0 });
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
            pnlSaBan.ResumeLayout(false);
            grpLightNorth.ResumeLayout(false);
            grpLightWest.ResumeLayout(false);
            grpLightSouth.ResumeLayout(false);
            grpLightEast.ResumeLayout(false);
            grpControl.ResumeLayout(false);
            pnlManualGroup.ResumeLayout(false);
            grpConfig.ResumeLayout(false);
            grpConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numGreenTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYellowTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRedTime).EndInit();
            grpLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSaBan;
        private Label lblStatusTag;
        private Label lblCountdown;
        private Label label2;
        private Label label1;
        private GroupBox grpLightEast;
        private GroupBox grpLightWest;
        private GroupBox grpLightSouth;
        private GroupBox grpLightNorth;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label3;
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
        private NumericUpDown numRedTime;
        private NumericUpDown numYellowTime;
        private NumericUpDown numGreenTime;
        private Button btnSaveConfig;
        private GroupBox grpLogs;
        private DataGridView dgvLogs;
        private DataGridViewTextBoxColumn colTime;
        private DataGridViewTextBoxColumn colEvent;
        private DataGridViewTextBoxColumn colMode;
        private DataGridViewTextBoxColumn colDetail;
    }
}
