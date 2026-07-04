using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace TrafficSystem_WinForm
{
    public partial class Form1 : Form
    {
        // --- 1. CÁC BIẾN HỆ THỐNG & KẾT NỐI ---
        private HubConnection _hubConnection;
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "http://localhost:5010";

        private string currentMode = "AUTO";
        private int greenTime = 25;
        private int yellowTime = 3;
        private int redTime = 28;

        // BIẾN LƯU TRẠNG THÁI CŨ ĐỂ THEO DÕI LỊCH SỬ THAY ĐỔI PHA ĐÈN
        private string _lastMode = "";
        private string _lastNorthLight = "";
        private string _lastEastLight = "";

        public Form1()
        {
            InitializeComponent();

            // BẬT TÍNH NĂNG ĐỆM KÉP ĐỂ WINFORMS KHÔNG BỊ TRỄ KHI NHẬN LOG LIÊN TỤC
            this.DoubleBuffered = true;

            // Nếu dgvLogs làm giao diện bị giật, ép nó chạy mượt bằng cách này:
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dgvLogs, new object[] { true });
        }

        // --- 2. SỰ KIỆN PHÁT SINH KHI FORM KHỞI CHẠY ---
        private async void Form1_Load(object sender, EventArgs e)
        {
            SetupSignalR();
            // Khóa sạch các nút điều khiển, bắt người dùng phải bấm "BẬT KẾT NỐI" trước
            SetControlButtonsState(false);

            // Ghi log khởi động
            AddLog("Hệ thống", "KHỞI ĐỘNG", "Bắt đầu kết nối phần mềm giám sát.");

            try
            {
                //await _hubConnection.StartAsync();
                AddLog("Mạng", "KẾT NỐI", "Đã kết nối SignalR thành công.");
            }
            catch (Exception ex)
            {
                AddLog("Mạng", "LỖI", $"Không thể kết nối SignalR: {ex.Message}");
                MessageBox.Show("Chưa thể kết nối tới Backend. Vui lòng đảm bảo Backend API đang chạy!\nChi tiết: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- 3. HÀM THÊM LỊCH SỬ VÀO DATAGRIDVIEW ---
        // Đẩy thông tin vào dgvLogs. Hàm này an toàn với đa luồng (Thread-safe)
        private void AddLog(string eventPhase, string mode, string details)
        {
            // Đảm bảo thao tác trên UI Thread
            if (dgvLogs.InvokeRequired)
            {
                dgvLogs.Invoke(new Action(() => AddLog(eventPhase, mode, details)));
                return;
            }

            try
            {
                // Lấy thời gian hiện tại
                string timeStr = DateTime.Now.ToString("HH:mm:ss dd/MM");

                // Thêm một dòng mới lên đầu danh sách (Index = 0)
                // Thứ tự cột: Thời gian, Sự kiện/Pha, Chế độ, Chi tiết lệnh
                dgvLogs.Rows.Insert(0, timeStr, eventPhase, mode, details);

                // Giữ lại tối đa 100 dòng gần nhất để tránh phần mềm bị nặng và lag
                if (dgvLogs.Rows.Count > 100)
                {
                    dgvLogs.Rows.RemoveAt(dgvLogs.Rows.Count - 1);
                }
            }
            catch { /* Bỏ qua nếu DataGridView chưa khởi tạo kịp */ }
        }

        // --- 4. CẤU HÌNH KẾT NỐI TRUYỀN DỮ LIỆU SIGNALR ---
        private void SetupSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{_baseUrl}/hubs/traffic")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<TrafficStatus>("ReceiveStatus", (status) =>
            {
                Invoke(() =>
                {
                    UpdateRealTimeUI(status);
                });
            });
        }

        // --- 5. HÀM CẬP NHẬT GIAO DIỆN REAL-TIME TỪ DỮ LIỆU THẬT ---
        private void UpdateRealTimeUI(TrafficStatus status)
        {
            try
            {
                if (status == null) return;

                currentMode = status.Mode?.ToUpper();

                // GHI LOG: Phát hiện chuyển chế độ từ Backend
                if (!string.IsNullOrEmpty(currentMode) && currentMode != _lastMode)
                {
                    if (_lastMode != "") // Bỏ qua lần load dữ liệu đầu tiên
                    {
                        AddLog("Chuyển chế độ", currentMode, $"Hệ thống chuyển từ {_lastMode} sang {currentMode}");
                    }
                    _lastMode = currentMode;
                }

                // GHI LOG: Phát hiện chuyển pha đèn (Chỉ ở chế độ AUTO để tránh spam quá nhiều)
                if (currentMode == "AUTO")
                {
                    if (status.NorthLight != _lastNorthLight || status.EastLight != _lastEastLight)
                    {
                        if (!string.IsNullOrEmpty(status.NorthLight) && !string.IsNullOrEmpty(status.EastLight))
                        {
                            AddLog("Chuyển pha", "AUTO", $"Đèn Bắc/Nam: {status.NorthLight} | Đèn Đông/Tây: {status.EastLight}");
                            _lastNorthLight = status.NorthLight;
                            _lastEastLight = status.EastLight;
                        }
                    }
                }

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

                pnlManualGroup.Enabled = (currentMode == "MANUAL");

                int currentTime = status.RemainingTime;
                int timeNS = currentTime;
                int timeEW = currentTime;

                if (currentMode == "AUTO")
                {
                    if (status.NorthLight == "GREEN")
                    {
                        timeNS = currentTime;
                        timeEW = currentTime + yellowTime;
                    }
                    else if (status.EastLight == "GREEN")
                    {
                        timeEW = currentTime;
                        timeNS = currentTime + yellowTime;
                    }
                    else if (status.NorthLight == "YELLOW")
                    {
                        timeNS = currentTime;
                        timeEW = currentTime;
                    }
                    else if (status.EastLight == "YELLOW")
                    {
                        timeNS = currentTime;
                        timeEW = currentTime;
                    }
                }

                lblCountNorth.Text = timeNS.ToString();
                lblCountSouth.Text = timeNS.ToString();
                lblCountEast.Text = timeEW.ToString();
                lblCountWest.Text = timeEW.ToString();

                SetLightColor(lblNorthRed, lblNorthYellow, lblNorthGreen, status.NorthLight);
                SetLightColor(lblSouthRed, lblSouthYellow, lblSouthGreen, status.SouthLight);
                SetLightColor(lblEastRed, lblEastYellow, lblEastGreen, status.EastLight);
                SetLightColor(lblWestRed, lblWestYellow, lblWestGreen, status.WestLight);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi UpdateUI: " + ex.Message);
            }
        }

        private void SetLightColor(Label redLabel, Label yellowLabel, Label greenLabel, string colorStatus)
        {
            redLabel.BackColor = Color.LightGray;
            yellowLabel.BackColor = Color.LightGray;
            greenLabel.BackColor = Color.LightGray;

            if (string.IsNullOrWhiteSpace(colorStatus)) return;

            string normalizedColor = colorStatus.Trim().ToUpper();

            switch (normalizedColor)
            {
                case "RED":
                case "0": redLabel.BackColor = Color.Red; break;
                case "GREEN":
                case "1": greenLabel.BackColor = Color.Green; break;
                case "YELLOW":
                case "2": yellowLabel.BackColor = Color.Yellow; break;
            }
        }

        private async Task SendLightCommand(string direction, string colorValue)
        {
            try
            {
                var requestData = new { Action = "SET_LIGHT", Direction = direction, Light = colorValue };
                string jsonPayload = JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                await _httpClient.PostAsync($"{_baseUrl}/api/control", content);
            }
            catch { /* Tạm ẩn lỗi ngầm */ }
        }

        // --- 6. LOGIC XỬ LÝ CÁC NÚT ĐIỀU KHIỂN CHẾ ĐỘ ---
        private async void btnModeAuto_Click(object sender, EventArgs e)
        {
            AddLog("Lệnh người dùng", "AUTO", "Gửi lệnh yêu cầu chế độ Tự Động.");
            pnlManualGroup.Enabled = false;

            var requestData = new { Action = "CHANGE_MODE", Mode = "AUTO" };
            string jsonPayload = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/control", content);
                if (!response.IsSuccessStatusCode)
                    MessageBox.Show("Backend nhận lệnh nhưng mạch không phản hồi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                AddLog("Mạng", "LỖI", "Gửi lệnh AUTO thất bại.");
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModeManual_Click(object sender, EventArgs e)
        {
            AddLog("Lệnh người dùng", "MANUAL", "Gửi lệnh yêu cầu chế độ Thủ Công.");
            pnlManualGroup.Enabled = true;

            var requestData = new { Action = "CHANGE_MODE", Mode = "MANUAL" };
            string jsonPayload = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/control", content);
                if (!response.IsSuccessStatusCode)
                    MessageBox.Show("Backend nhận lệnh nhưng mạch không phản hồi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                AddLog("Mạng", "LỖI", "Gửi lệnh MANUAL thất bại.");
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 7. LOGIC ĐIỀU KHIỂN ĐÈN THỦ CÔNG ---
        private async void btnNSG_EWR_Click(object sender, EventArgs e)
        {
            AddLog("Lệnh người dùng", "MANUAL", "Ép đèn Bắc/Nam (XANH) - Đông/Tây (ĐỎ).");
            await SendLightCommand("NORTH", "GREEN");
            await SendLightCommand("SOUTH", "GREEN");
            await SendLightCommand("EAST", "RED");
            await SendLightCommand("WEST", "RED");
        }

        private async void btnNSY_EWR_Click(object sender, EventArgs e)
        {
            AddLog("Lệnh người dùng", "MANUAL", "Ép đèn Bắc/Nam (VÀNG) - Đông/Tây (ĐỎ).");
            await SendLightCommand("NORTH", "YELLOW");
            await SendLightCommand("SOUTH", "YELLOW");
            await SendLightCommand("EAST", "RED");
            await SendLightCommand("WEST", "RED");
        }

        private async void btnEWG_NSR_Click(object sender, EventArgs e)
        {
            AddLog("Lệnh người dùng", "MANUAL", "Ép đèn Đông/Tây (XANH) - Bắc/Nam (ĐỎ).");
            await SendLightCommand("NORTH", "RED");
            await SendLightCommand("SOUTH", "RED");
            await SendLightCommand("EAST", "GREEN");
            await SendLightCommand("WEST", "GREEN");
        }

        private async void btnEWY_NSR_Click(object sender, EventArgs e)
        {
            AddLog("Lệnh người dùng", "MANUAL", "Ép đèn Đông/Tây (VÀNG) - Bắc/Nam (ĐỎ).");
            await SendLightCommand("NORTH", "RED");
            await SendLightCommand("SOUTH", "RED");
            await SendLightCommand("EAST", "YELLOW");
            await SendLightCommand("WEST", "YELLOW");
        }

        // --- 8. LƯU VÀ ÁP DỤNG CẤU HÌNH THỜI GIAN ĐÈN MỚI ---
        private async void btnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                greenTime = (int)nudGreen.Value;
                yellowTime = (int)nudYellow.Value;
                redTime = greenTime + yellowTime;
                nudRed.Value = redTime;

                var configData = new
                {
                    NsGreenTime = greenTime,
                    NsYellowTime = yellowTime,
                    EwGreenTime = greenTime,
                    EwYellowTime = yellowTime,
                    Mode = currentMode ?? "AUTO"
                };

                AddLog("Cấu hình", currentMode, $"Cập nhật thời gian: Xanh {greenTime}s, Vàng {yellowTime}s, Đỏ {redTime}s.");

                string jsonPayload = JsonSerializer.Serialize(configData);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_baseUrl}/api/config", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Đã lưu và áp dụng cấu hình!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    AddLog("Mạng", "LỖI", $"Lưu cấu hình lỗi mã: {response.StatusCode}");
                    MessageBox.Show("Lỗi khi lưu cấu hình.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                AddLog("Mạng", "LỖI", "Không thể gửi lệnh cấu hình.");
                MessageBox.Show($"Không thể kết nối tới Backend: {ex.Message}", "Lỗi Mạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnModeEmergency_Click(object sender, EventArgs e)
        {
            try
            {
                btnModeEmergency.Enabled = false;
                AddLog("Lệnh người dùng", "EMERGENCY", "Gửi lệnh kích hoạt chế độ KHẨN CẤP.");

                var requestData = new { Action = "CHANGE_MODE", Mode = "EMERGENCY" };
                string jsonPayload = JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/api/control", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Hệ thống đã chuyển sang chế độ KHẨN CẤP!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    AddLog("Mạng", "LỖI", "Backend từ chối lệnh EMERGENCY.");
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"API từ chối lệnh.\nChi tiết: {errorResponse}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                AddLog("Mạng", "LỖI", "Lỗi kết nối khi gửi lệnh KHẨN CẤP.");
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnModeEmergency.Enabled = true;
            }
        }

        private bool isConnected = false; // Đổi tên biến cho rõ nghĩa: Trạng thái kết nối của WinForm

        private async void btnOnOff_Click(object sender, EventArgs e)
        {
            btnOnOff.Enabled = false; // Khóa tạm thời nút master để tránh click double

            if (!isConnected) // TÌNH HUỐNG: NGƯỜI DÙNG ẤN "BẬT KẾT NỐI"
            {
                try
                {
                    // 1. Kết nối SignalR để nhận dữ liệu
                    if (_hubConnection.State == Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Disconnected)
                    {
                        await _hubConnection.StartAsync();
                        AddLog("Mạng", "KẾT NỐI", "WinForm đã kết nối thành công tới hệ thống.");
                    }

                    // 2. MỞ KHÓA toàn bộ các nút bấm điều khiển khác trên màn hình
                    SetControlButtonsState(true);

                    // 3. Thay đổi giao diện nút Master sang màu đỏ (Sẵn sàng NGẮT)
                    isConnected = true;
                    btnOnOff.Text = "NGẮT KẾT NỐI";
                    btnOnOff.BackColor = Color.Red;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể kết nối tới Backend! Vui lòng kiểm tra lại.\nChi tiết: " + ex.Message,
                                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else // TÌNH HUỐNG: NGƯỜI DÙNG ẤN "NGẮT KẾT NỐI"
            {
                try
                {
                    // 1. GỬI LỆNH RESTART QUA API ĐỂ ÉP MẠCH PROTEUS QUAY VỀ TRẠNG THÁI KHỞI TẠO BAN ĐẦU
                    // Lệnh này sẽ giải phóng chế độ thủ công/khẩn cấp và reset thời gian cài đặt về mặc định.
                    var resetData = new
                    {
                        Action = "RESTART"
                    };

                    string jsonPayload = JsonSerializer.Serialize(resetData);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Gọi API đến đầu /api/Control của Backend để ra lệnh cho Proteus
                    await _httpClient.PostAsync($"{_baseUrl}/api/Control", content);

                    AddLog("Hệ thống", "RESET", "Đã gửi lệnh khôi phục trạng thái gốc cho mạch Proteus.");


                    // 2. KHÓA NGAY toàn bộ các nút bấm điều khiển khác trên màn hình WinForm
                    SetControlButtonsState(false);


                    // 3. Ngắt kết nối SignalR của riêng WinForm này để giải phóng băng thông
                    if (_hubConnection.State == Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Connected)
                    {
                        await _hubConnection.StopAsync();
                        AddLog("Mạng", "NGẮT KẾT NỐI", "WinForm đã ngắt kết nối an toàn.");
                    }


                    // 4. Khôi phục giao diện nút Master về trạng thái ban đầu và reset các đồng hồ về 0
                    isConnected = false;
                    btnOnOff.Text = "BẬT KẾT NỐI";
                    btnOnOff.BackColor = Color.Green;

                    lblCountNorth.Text = "0"; lblCountSouth.Text = "0";
                    lblCountEast.Text = "0"; lblCountWest.Text = "0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi khôi phục hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            btnOnOff.Enabled = true; // Mở lại nút Master
        }

        private void SetControlButtonsState(bool isEnabled)
        {
            // Bạn hãy thay các tên nút dưới đây bằng ĐÚNG tên các nút điều khiển trên giao diện của bạn
            // Ví dụ: nút tự động, nút thủ công, nút xác nhận đặt thời gian, các ô nhập dữ liệu...
            btnModeAuto.Enabled = isEnabled;
            btnModeManual.Enabled = isEnabled;
            btnModeEmergency.Enabled = isEnabled;
            btnSaveConfig.Enabled = isEnabled;

            // Nếu có ô nhập thời gian (TextBox) hay ComboBox thì cũng khóa luôn:
            // txtTime.Enabled = isEnabled;
            // cboDirection.Enabled = isEnabled;
        }
    }
}