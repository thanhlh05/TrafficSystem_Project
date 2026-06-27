namespace TrafficSystem_WinForm
{
    public partial class Form1 : Form
    {
        // Khai báo thời gian mặc định của các pha (khớp với thông số trên UI của bạn)
        private int greenTime = 25;
        private int yellowTime = 3;
        private int redTime = 28;

        // Biến chạy đếm ngược thực tế
        private int countdownValue = 25;

        // Quản lý trạng thái pha đèn hiện tại: 
        // 1: BắcNam_Xanh / ĐôngTây_Đỏ
        // 2: BắcNam_Vàng / ĐôngTây_Đỏ
        // 3: BắcNam_Đỏ / ĐôngTây_Xanh
        // 4: BắcNam_Đỏ / ĐôngTây_Vàng
        private int currentPhase = 1;

        // Chế độ hoạt động hiện tại: "AUTO", "MANUAL", "EMERGENCY"
        private string currentMode = "AUTO";

        public Form1()
        {
            InitializeComponent();
        }
        private void ResetAllLights()
        {
            // Giả sử bạn đặt tên các ô đèn trong cụm Bắc là lblNorthRed, lblNorthYellow, lblNorthGreen...
            Color offColor = Color.Gainsboro;

            // Hướng Bắc
            lblNorthRed.BackColor = offColor; lblNorthYellow.BackColor = offColor; lblNorthGreen.BackColor = offColor;
            // Hướng Nam
            lblSouthRed.BackColor = offColor; lblSouthYellow.BackColor = offColor; lblSouthGreen.BackColor = offColor;
            // Hướng Đông
            lblEastRed.BackColor = offColor; lblEastYellow.BackColor = offColor; lblEastGreen.BackColor = offColor;
            // Hướng Tây
            lblWestRed.BackColor = offColor; lblWestYellow.BackColor = offColor; lblWestGreen.BackColor = offColor;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Nếu không phải chế độ TỰ ĐỘNG thì dừng đếm chu kỳ
            if (currentMode != "AUTO") return;

            countdownValue--;

            // Logic chuyển pha tuần tự
            if (countdownValue < 0)
            {
                if (currentPhase == 1) { currentPhase = 2; countdownValue = yellowTime; }
                else if (currentPhase == 2) { currentPhase = 3; countdownValue = greenTime; }
                else if (currentPhase == 3) { currentPhase = 4; countdownValue = yellowTime; }
                else if (currentPhase == 4) { currentPhase = 1; countdownValue = greenTime; }
            }

            // --- LOGIC TÍNH TOÁN SỐ GIÂY ĐỘC LẬP CHO TỪNG HƯỚNG ---
            int nsDisplay = 0; // Số giây hiển thị hướng Bắc - Nam
            int ewDisplay = 0; // Số giây hiển thị hướng Đông - Tây

            if (currentPhase == 1) // Bắc-Nam XANH / Đông-Tây ĐỎ
            {
                nsDisplay = countdownValue;
                ewDisplay = countdownValue + yellowTime; // Đỏ = Xanh + Vàng
            }
            else if (currentPhase == 2) // Bắc-Nam VÀNG / Đông-Tây ĐỎ
            {
                nsDisplay = countdownValue;
                ewDisplay = countdownValue; // Đỏ chạy nốt số giây vàng còn lại
            }
            else if (currentPhase == 3) // Bắc-Nam ĐỎ / Đông-Tây XANH
            {
                nsDisplay = countdownValue + yellowTime; // Đỏ = Xanh + Vàng
                ewDisplay = countdownValue;
            }
            else if (currentPhase == 4) // Bắc-Nam ĐỎ / Đông-Tây VÀNG
            {
                nsDisplay = countdownValue;
                ewDisplay = countdownValue;
            }

            // Hiển thị số giây lên 4 hướng trên giao diện
            lblCountNorth.Text = nsDisplay.ToString();
            lblCountSouth.Text = nsDisplay.ToString();
            lblCountEast.Text = ewDisplay.ToString();
            lblCountWest.Text = ewDisplay.ToString();

            // --- CẬP NHẬT MÀU ĐÈN THEO PHA ---
            ResetAllLights();
            if (currentPhase == 1)
            {
                lblNorthGreen.BackColor = Color.LimeGreen; lblSouthGreen.BackColor = Color.LimeGreen;
                lblEastRed.BackColor = Color.Red; lblWestRed.BackColor = Color.Red;
            }
            else if (currentPhase == 2)
            {
                lblNorthYellow.BackColor = Color.Yellow; lblSouthYellow.BackColor = Color.Yellow;
                lblEastRed.BackColor = Color.Red; lblWestRed.BackColor = Color.Red;
            }
            else if (currentPhase == 3)
            {
                lblNorthRed.BackColor = Color.Red; lblSouthRed.BackColor = Color.Red;
                lblEastGreen.BackColor = Color.LimeGreen; lblWestGreen.BackColor = Color.LimeGreen;
            }
            else if (currentPhase == 4)
            {
                lblNorthRed.BackColor = Color.Red; lblSouthRed.BackColor = Color.Red;
                lblEastYellow.BackColor = Color.Yellow; lblWestYellow.BackColor = Color.Yellow;
            }
        }

        private void btnModeManual_Click(object sender, EventArgs e)
        {
            currentMode = "MANUAL";
            lblStatusTag.Text = "CHẾ ĐỘ: THỦ CÔNG";
            lblStatusTag.BackColor = Color.LightBlue;
            pnlManualGroup.Enabled = true; // Mở khóa cụm nút gộp Bắc-Nam/Đông-Tây để test bấm tay
        }

        private void btnModeAuto_Click(object sender, EventArgs e)
        {
            currentMode = "AUTO";
            lblStatusTag.Text = "CHẾ ĐỘ: TỰ ĐỘNG";
            lblStatusTag.BackColor = Color.LightGreen;
            pnlManualGroup.Enabled = false; // Khóa cụm nút bấm tay lại
            countdownValue = greenTime;    // Reset lại bộ đếm chu kỳ tự động
            currentPhase = 1;
        }

        private void btnNSG_EWR_Click(object sender, EventArgs e)
        {
            if (currentMode != "MANUAL") return;
            ResetAllLights();
            lblNorthGreen.BackColor = Color.LimeGreen; lblSouthGreen.BackColor = Color.LimeGreen;
            lblEastRed.BackColor = Color.Red; lblWestRed.BackColor = Color.Red;
            SetManualLabels("XANH", "ĐỎ");
        }

        private void btnNSY_EWR_Click(object sender, EventArgs e)
        {
            if (currentMode != "MANUAL") return;
            ResetAllLights();
            lblNorthYellow.BackColor = Color.Yellow; lblSouthYellow.BackColor = Color.Yellow;
            lblEastRed.BackColor = Color.Red; lblWestRed.BackColor = Color.Red;
            SetManualLabels("VÀNG", "ĐỎ");
        }

        private void btnEWG_NSR_Click(object sender, EventArgs e)
        {
            if (currentMode != "MANUAL") return;
            ResetAllLights();
            lblNorthRed.BackColor = Color.Red; lblSouthRed.BackColor = Color.Red;
            lblEastGreen.BackColor = Color.LimeGreen; lblWestGreen.BackColor = Color.LimeGreen;
            SetManualLabels("ĐỎ", "XANH");
        }

        private void btnEWY_NSR_Click(object sender, EventArgs e)
        {
            if (currentMode != "MANUAL") return;
            ResetAllLights();
            lblNorthRed.BackColor = Color.Red; lblSouthRed.BackColor = Color.Red;
            lblEastYellow.BackColor = Color.Yellow; lblWestYellow.BackColor = Color.Yellow;
            SetManualLabels("ĐỎ", "VÀNG");
        }
        private void SetManualLabels(string nsText, string ewText)
        {
            lblCountNorth.Text = nsText; lblCountSouth.Text = nsText;
            lblCountEast.Text = ewText; lblCountWest.Text = ewText;
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các ô nhập số trên giao diện gán vào biến hệ thống
            greenTime = (int)numGreenTime.Value;
            yellowTime = (int)numYellowTime.Value;
            redTime = (int)numRedTime.Value;

            // Áp dụng lập tức chu kỳ mới nếu đang ở chế độ tự động
            if (currentMode == "AUTO")
            {
                countdownValue = greenTime;
                currentPhase = 1;
            }

            MessageBox.Show("Đã cập nhật cấu hình thời gian thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
