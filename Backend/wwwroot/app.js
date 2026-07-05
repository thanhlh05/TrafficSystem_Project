// Tự động nhận diện địa chỉ và port của Backend đang chạy
const BASE_URL = window.location.origin;

let isConnected = false;
let hubConnection = null;

// Khởi tạo SignalR kết nối đến Hub Endpoint của bạn
hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${BASE_URL}/hubs/traffic`)
    .withAutomaticReconnect()
    .build();

// Đón sự kiện ReceiveStatus từ SignalR bắn về giống WinForm
hubConnection.on("ReceiveStatus", (status) => {
    document.getElementById("timeNorth").innerText = String(status.timeNorth || 0).padStart(2, '0');
    document.getElementById("timeSouth").innerText = String(status.timeSouth || 0).padStart(2, '0');
    document.getElementById("timeEast").innerText = String(status.timeEast || 0).padStart(2, '0');
    document.getElementById("timeWest").innerText = String(status.timeWest || 0).padStart(2, '0');
});

// Sự kiện bấm nút Bật/Tắt hệ thống trên Web
document.getElementById("btnOnOff").addEventListener("click", async () => {
    const btn = document.getElementById("btnOnOff");
    const dashboard = document.getElementById("dashboard");
    const controls = document.getElementById("controls");

    btn.disabled = true;

    if (!isConnected) {
        try {
            await hubConnection.start();
            console.log("Web đã kết nối SignalR thành công!");

            isConnected = true;
            btn.innerText = "NGẮT KẾT NỐI HỆ THỐNG";
            btn.classList.replace("btn-success", "btn-danger");

            dashboard.classList.remove("disabled-overlay");
            controls.classList.remove("disabled-overlay");
        } catch (err) {
            alert("Không thể kết nối tới Backend: " + err.message);
        }
    } else {
        try {
            await sendAction("RESTART"); // Gửi lệnh đóng an toàn
            await hubConnection.stop();

            isConnected = false;
            btn.innerText = "BẬT KẾT NỐI HỆ THỐNG";
            btn.classList.replace("btn-danger", "btn-success");

            dashboard.classList.add("disabled-overlay");
            controls.classList.add("disabled-overlay");
            document.querySelectorAll(".light-box span").forEach(el => el.innerText = "00");
        } catch (err) {
            alert("Lỗi khi ngắt kết nối: " + err.message);
        }
    }
    btn.disabled = false;
});

// Hàm bắn API điều khiển
async function sendAction(actionName) {
    try {
        const response = await fetch(`${BASE_URL}/api/Control`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Action: actionName })
        });
        if (!response.ok) throw new Error("Lỗi phản hồi từ API");
        console.log(`Đã thực thi lệnh: ${actionName}`);
    } catch (error) {
        console.error("Lỗi điều khiển:", error);
    }
}