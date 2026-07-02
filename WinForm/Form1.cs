using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace TrafficSystem_WinForm // <-- BẠN CÓ THỂ ĐỔI THÀNH NAMESPACE ĐÚNG CỦA DỰ ÁN BẠN NẾU BỊ BÁO LỖI
{
    public partial class Form1 : Form
    {
        // --- 1. CÁC BIẾN HỆ THỐNG & KẾT NỐI ---
        private HubConnection _hubConnection;
        private string currentMode = "AUTO";
        private int greenTime = 25;
        private int yellowTime = 3;
        private int redTime = 28;

        public Form1()
        {
            InitializeComponent();
        }

        // --- 2. SỰ KIỆN PHÁT SINH KHI FORM KHỞI CHẠY ---
        private async void Form1_Load(object sender, EventArgs e)
        {

            // Cấu hình và khởi chạy kết nối SignalR
            SetupSignalR();

            try
            {
                await _hubConnection.StartAsync();
                MessageBox.Show("Đã kết nối SignalR thành công tới trung tâm điều khiển Backend!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa thể kết nối tới Backend. Vui lòng đảm bảo Backend API đang chạy!\nChi tiết: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- 3. CẤU HÌNH KẾT NỐI TRUYỀN DỮ LIỆU SIGNALR ---
        private void SetupSignalR()
        {
            // Khởi tạo kết nối tới Hub của Backend (Thay đổi Port 5000 cho đúng với Port Backend của bạn B)
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5010/hubs/traffic")
                .WithAutomaticReconnect()
                .Build();

            // Lắng nghe cổng sự kiện "ReceiveStatus" trả về từ Server
            _hubConnection.On<TrafficStatus>("ReceiveStatus", (status) =>
            {
                // Bắt buộc dùng Invoke để đẩy dữ liệu từ luồng ngầm (Background Thread) về luồng giao diện chính (UI Thread)
                Invoke(() =>
                {
                    UpdateRealTimeUI(status);
                });
            });
        }

        // --- 4. HÀM CẬP NHẬT GIAO DIỆN REAL-TIME TỪ DỮ LIỆU THẬT ---
        private void UpdateRealTimeUI(TrafficStatus status)
        {
            try
            {
                if (status == null) return;

                // Debug dữ liệu nhận từ Arduino
                System.Diagnostics.Debug.WriteLine(
                    $"---> TEST DỮ LIỆU: Bắc [{status.NorthLight}] | Đông [{status.EastLight}] | Giây: {status.RemainingTime}");

                // Cập nhật chế độ hiện tại
                currentMode = status.Mode?.ToUpper();

                switch (currentMode)
                {
                    case "AUTO":
                        lblStatusTag.Text = "CHẾ ĐỘ: TỰ ĐỘNG";
                        lblStatusTag.BackColor = SystemColors.ActiveCaption;
                        break;

                    case "MANUAL":
                        lblStatusTag.Text = "CHẾ ĐỘ: THỦ CÔNG";
                        lblStatusTag.BackColor = Color.LightPink;
                        break;

                    case "EMERGENCY":
                        lblStatusTag.Text = "CHẾ ĐỘ: KHẨN CẤP";
                        lblStatusTag.BackColor = Color.OrangeRed;
                        break;

                    default:
                        lblStatusTag.Text = "CHẾ ĐỘ: KHÔNG XÁC ĐỊNH";
                        lblStatusTag.BackColor = Color.Gray;
                        break;
                }

                // Chỉ cho phép điều khiển tay khi ở MANUAL
                pnlManualGroup.Enabled = (currentMode == "MANUAL");

                // ===========================
                // Cập nhật thời gian đếm ngược
                // ===========================

                int currentTime = status.RemainingTime;
                int timeNS = currentTime;
                int timeEW = currentTime;

                // Thời gian đèn vàng (nếu sau này có API cấu hình thì thay bằng giá trị cấu hình)
                int yellowTime = 3;

                if (currentMode == "AUTO")
                {
                    if (status.NorthLight == "GREEN")
                    {
                        // Bắc - Nam xanh
                        timeNS = currentTime;
                        timeEW = currentTime + yellowTime;
                    }
                    else if (status.EastLight == "GREEN")
                    {
                        // Đông - Tây xanh
                        timeEW = currentTime;
                        timeNS = currentTime + yellowTime;
                    }
                    else if (status.NorthLight == "YELLOW")
                    {
                        // Bắc - Nam vàng
                        timeNS = currentTime;
                        timeEW = currentTime;
                    }
                    else if (status.EastLight == "YELLOW")
                    {
                        // Đông - Tây vàng
                        timeNS = currentTime;
                        timeEW = currentTime;
                    }
                }

                // Hiển thị số giây cho từng trục
                lblCountNorth.Text = timeNS.ToString();
                lblCountSouth.Text = timeNS.ToString();
                lblCountEast.Text = timeEW.ToString();
                lblCountWest.Text = timeEW.ToString();

                // ===========================
                // Cập nhật màu đèn
                // ===========================

                SetLightColor(lblNorthRed, lblNorthYellow, lblNorthGreen, status.NorthLight);
                SetLightColor(lblSouthRed, lblSouthYellow, lblSouthGreen, status.SouthLight);
                SetLightColor(lblEastRed, lblEastYellow, lblEastGreen, status.EastLight);
                SetLightColor(lblWestRed, lblWestYellow, lblWestGreen, status.WestLight);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Bắt được lỗi ngầm: {ex.Message}",
                    "Lỗi Giao Diện",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetLightColor(Label redLabel, Label yellowLabel, Label greenLabel, string colorStatus)
        {
            // 1. Mặc định tắt đèn về xám
            redLabel.BackColor = Color.LightGray;
            yellowLabel.BackColor = Color.LightGray;
            greenLabel.BackColor = Color.LightGray;

            // 2. Nếu dữ liệu rỗng hoặc null thì dừng luôn
            if (string.IsNullOrWhiteSpace(colorStatus)) return;

            // 3. Dọn dẹp khoảng trắng dư thừa và in hoa toàn bộ
            string normalizedColor = colorStatus.Trim().ToUpper();

            // 4. Bọc lót cả trường hợp nhận chữ (RED) lẫn nhận chuỗi số ("0")
            switch (normalizedColor)
            {
                case "RED":
                case "0":
                    redLabel.BackColor = Color.Red;
                    break;

                case "GREEN":
                case "1":
                    greenLabel.BackColor = Color.Green;
                    break;

                case "YELLOW":
                case "2":
                    yellowLabel.BackColor = Color.Yellow;
                    break;
            }
        }

        // Hàm này giúp WinForms ra lệnh đổi màu cho 1 hướng cụ thể
        private async Task SendLightCommand(string direction, string colorValue)
        {
            try
            {
                var requestData = new
                {
                    Action = "SET_LIGHT",
                    Direction = direction,
                    Light = colorValue // Gửi số dạng chuỗi: "1" (Đỏ), "2" (Vàng), "3" (Xanh)
                };

                string jsonPayload = System.Text.Json.JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    // Sửa port 5010 cho khớp với máy bạn
                    await client.PostAsync("http://localhost:5010/api/control", content);
                }
            }
            catch { /* Tạm ẩn lỗi để không gián đoạn thao tác bấm */ }
        }

        // Hàm Reset nhanh trạng thái toàn bộ 12 đèn về màu xám tắt
        private void ResetAllLights()
        {
            lblNorthRed.BackColor = Color.LightGray; lblNorthYellow.BackColor = Color.LightGray; lblNorthGreen.BackColor = Color.LightGray;
            lblSouthRed.BackColor = Color.LightGray; lblSouthYellow.BackColor = Color.LightGray; lblSouthGreen.BackColor = Color.LightGray;
            lblEastRed.BackColor = Color.LightGray; lblEastYellow.BackColor = Color.LightGray; lblEastGreen.BackColor = Color.LightGray;
            lblWestRed.BackColor = Color.LightGray; lblWestYellow.BackColor = Color.LightGray; lblWestGreen.BackColor = Color.LightGray;
        }

        // --- 5. LOGIC XỬ LÝ CÁC NÚT ĐIỀU KHIỂN CHẾ ĐỘ ---
        private async void btnModeAuto_Click(object sender, EventArgs e)
        {
            // 1. Giao diện local chuyển trạng thái sang Tự động
            currentMode = "AUTO";
            lblStatusTag.Text = "CHẾ ĐỘ: TỰ ĐỘNG";
            lblStatusTag.BackColor = SystemColors.ActiveCaption;
            pnlManualGroup.Enabled = false; // Khóa cụm nút bấm tay lại

            // 2. ĐÓNG GÓI DỮ LIỆU ĐỂ BẮN TRAO ĐỔI VỚI BACKEND
            var requestData = new
            {
                Action = "CHANGE_MODE",
                Mode = "AUTO"
            };

            string jsonPayload = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // 3. GỬI LỆNH LÊN CONTROL CONTROLLER
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync("http://localhost:5010/api/control", content);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Backend nhận lệnh chuyển AUTO nhưng mạch không phản hồi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối tới API Control: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModeManual_Click(object sender, EventArgs e)
        {
            // 1. Giao diện local chuyển trạng thái trước để mượt mà
            currentMode = "MANUAL";
            lblStatusTag.Text = "CHẾ ĐỘ: THỦ CÔNG";
            lblStatusTag.BackColor = Color.LightPink;
            pnlManualGroup.Enabled = true;

            // 2. ĐÓNG GÓI DỮ LIỆU ĐÚNG THEO YÊU CẦU CỦA CONTROLREQUEST
            var requestData = new
            {
                Action = "CHANGE_MODE",
                Mode = "MANUAL"
            };

            // Chuyển đối tượng thành chuỗi JSON
            string jsonPayload = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // 3. GỬI HTTP POST SANG BACKEND
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Thay port 5010 cho đúng với port Backend hiện tại của bạn
                    // Route mặc định là /api/control theo cấu hình [Route("api/[controller]")]
                    var response = await client.PostAsync("http://localhost:5010/api/control", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Backend nhận lệnh nhưng mạch Proteus không phản hồi hoặc xử lý lỗi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối tới API Control: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 6. LOGIC ĐIỀU KHIỂN ĐÈN THỦ CÔNG KHI TRONG CHẾ ĐỘ THỦ CÔNG ---
        private async void btnNSG_EWR_Click(object sender, EventArgs e)
        {
            // Ép hướng Bắc, Nam thành Xanh; hướng Đông, Tây thành Đỏ
            await SendLightCommand("NORTH", "GREEN");
            await SendLightCommand("SOUTH", "GREEN");
            await SendLightCommand("EAST", "RED");
            await SendLightCommand("WEST", "RED");
        }

        private async void btnNSY_EWR_Click(object sender, EventArgs e)
        {
            await SendLightCommand("NORTH", "YELLOW");
            await SendLightCommand("SOUTH", "YELLOW");
            await SendLightCommand("EAST", "RED");
            await SendLightCommand("WEST", "RED");
        }

        private async void btnEWG_NSR_Click(object sender, EventArgs e)
        {
            await SendLightCommand("NORTH", "RED");
            await SendLightCommand("SOUTH", "RED");
            await SendLightCommand("EAST", "GREEN");
            await SendLightCommand("WEST", "GREEN");
        }

        private async void btnEWY_NSR_Click(object sender, EventArgs e)
        {
            await SendLightCommand("NORTH", "RED");
            await SendLightCommand("SOUTH", "RED");
            await SendLightCommand("EAST", "YELLOW");
            await SendLightCommand("WEST", "YELLOW");
        }

        // --- 7. LƯU VÀ ÁP DỤNG CẤU HÌNH THỜI GIAN ĐÈN MỚI ---
        private async void btnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy thời gian từ giao diện
                int greenTime = (int)nudGreen.Value;
                int yellowTime = (int)nudYellow.Value;

                // Tự động tính toán lại đèn Đỏ cho chuẩn logic (Đỏ = Xanh + Vàng)
                int redTime = greenTime + yellowTime;
                nudRed.Value = redTime; // Cập nhật lại lên UI cho người dùng thấy

                // 2. Đóng gói dữ liệu thành JSON để gửi lên Backend
                var configData = new
                {
                    NsGreenTime = greenTime,   // Gán ô nhập vào trục Bắc - Nam
                    NsYellowTime = yellowTime,
                    EwGreenTime = greenTime,   // Vì dùng chung cấu hình nên trục Đông - Tây nhận giá trị y hệt
                    EwYellowTime = yellowTime,
                    Mode = currentMode ?? "AUTO"
                };

                string jsonPayload = System.Text.Json.JsonSerializer.Serialize(configData);
                var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                // 3. Gọi API PUT /api/config
                using (HttpClient client = new HttpClient())
                {
                    // Sửa port 5010 cho khớp với Backend của bạn
                    HttpResponseMessage response = await client.PutAsync("http://localhost:5010/api/config", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Đã lưu và áp dụng cấu hình thời gian mới xuống hệ thống!",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi lưu cấu hình. Mã lỗi: " + response.StatusCode,
                                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối tới Backend: {ex.Message}",
                                "Lỗi Mạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModeEmergency_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Vô hiệu hóa nút tạm thời để tránh spam click
                btnModeEmergency.Enabled = false;

                // 2. Chuẩn bị dữ liệu JSON đúng với format của ControlRequest trên Backend
                var requestData = new
                {
                    Action = "CHANGE_MODE",
                    Mode = "EMERGENCY"
                };

                string jsonPayload = System.Text.Json.JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                // 3. Gửi Request lên Backend (Dựa theo hình Swagger của bạn, port là 7135)
                // Lưu ý: Nếu URL gốc của bạn được khai báo dùng chung toàn cục thì thay thế dòng này
                string apiUrl = "https://localhost:7135/api/Control";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // 4. Cập nhật giao diện khi thành công
                        MessageBox.Show("Hệ thống đã chuyển sang chế độ KHẨN CẤP!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // Gợi ý: Bạn có thể cập nhật Label hiển thị chế độ trên UI cho sinh động
                        // Ví dụ: lblCurrentMode.Text = "CHẾ ĐỘ: KHẨN CẤP";
                        // lblCurrentMode.BackColor = Color.Red;
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"API từ chối lệnh. Mã lỗi: {response.StatusCode}\nChi tiết: {errorResponse}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối đến Backend: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Mở khóa lại nút sau khi xử lý xong
                btnModeEmergency.Enabled = true;
            }
        }
    }
}