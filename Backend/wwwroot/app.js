const BASE_URL = window.location.origin;
let isConnected = false;
let hubConnection = null;

let lastPhase = "";
let lastMode = "";

hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${BASE_URL}/hubs/traffic`)
    .withAutomaticReconnect()
    .build();

// --- CHỨC NĂNG 1: LOG HOẠT ĐỘNG ---
function addLogToTable(eventPha, cheDo, chiTiet) {
    const tbody = document.querySelector("#logTable tbody");
    if (!tbody) return;

    const now = new Date();
    const timeStr = now.toTimeString().split(' ')[0];

    const tr = document.createElement("tr");
    tr.innerHTML = `
        <td><strong>${timeStr}</strong></td>
        <td>${eventPha}</td>
        <td><span class="badge ${cheDo === 'AUTO' ? 'bg-success' : cheDo === 'MANUAL' ? 'bg-primary' : 'bg-danger'}">${cheDo}</span></td>
        <td><small class="text-muted">${chiTiet}</small></td>
    `;
    tbody.insertBefore(tr, tbody.firstChild);
}

// --- CHỨC NĂNG 2: NHẬN DỮ LIỆU TỪ SIGNALR ---
hubConnection.on("ReceiveStatus", (status) => {
    const currentMode = (status.mode || status.Mode || 'AUTO').toUpperCase();
    const currentPhase = (status.phase || status.Phase || 'UNKNOWN').toUpperCase();

    const sysModeEl = document.getElementById("systemMode");
    if (sysModeEl) sysModeEl.innerText = `CHẾ ĐỘ: ${currentMode}`;

    const nLight = (status.north || status.North || "").toUpperCase();
    const sLight = (status.south || status.South || "").toUpperCase();
    const eLight = (status.east || status.East || "").toUpperCase();
    const wLight = (status.west || status.West || "").toUpperCase();
    const rTime = status.remainingTime !== undefined ? status.remainingTime : (status.RemainingTime || 0);

    // Bắt sự kiện đổi pha / đổi chế độ để ghi nhận Log
    if (currentMode !== lastMode || currentPhase !== lastPhase || (currentMode === "MANUAL" && nLight + eLight !== lastPhase)) {
        let eventText = "Đổi Chu Kỳ Đèn";
        let detailText = `Bắc-Nam: ${nLight} | Đông-Tây: ${eLight}`;

        if (currentMode !== lastMode) {
            eventText = `Đổi Chế Độ -> ${currentMode}`;
            detailText = `Hệ thống chuyển hoạt động thành công`;
        } else if (currentMode === "AUTO") {
            if (nLight === "GREEN") eventText = "Pha 1: Bắc Nam XANH";
            else if (nLight === "YELLOW") eventText = "Pha 2: Bắc Nam VÀNG";
            else if (eLight === "GREEN") eventText = "Pha 3: Đông Tây XANH";
            else if (eLight === "YELLOW") eventText = "Pha 4: Đông Tây VÀNG";
        } else if (currentMode === "MANUAL") {
            eventText = "Thao tác Thủ Công";
            detailText = `Yêu cầu sáng đèn trục ${nLight === 'GREEN' ? 'Bắc-Nam' : 'Đông-Tây'}`;
        }

        addLogToTable(eventText, currentMode, detailText);
        lastMode = currentMode;
        lastPhase = currentMode === "MANUAL" ? (nLight + eLight) : currentPhase;
    }

    // --- ĐẾM GIÂY ĐÈN ĐỎ NỘI SUY THÔNG MINH ---
    let displayNS = String(rTime).padStart(2, '0');
    let displayEW = String(rTime).padStart(2, '0');

    if (currentMode === 'MANUAL') {
        displayNS = '--';
        displayEW = '--';
    } else if (currentMode === 'EMERGENCY') {
        displayNS = '00';
        displayEW = '00';
    } else if (currentMode === 'AUTO') {
        const timeYellowInput = document.getElementById("timeYellow");
        const timeYellowConfig = (timeYellowInput && parseInt(timeYellowInput.value)) ? parseInt(timeYellowInput.value) : 3;

        if (nLight === "GREEN" || nLight === "YELLOW") {
            displayNS = String(rTime).padStart(2, '0');
            displayEW = String(rTime + (nLight === "GREEN" ? timeYellowConfig : 0)).padStart(2, '0');
        } else {
            displayEW = String(rTime).padStart(2, '0');
            displayNS = String(rTime + (eLight === "GREEN" ? timeYellowConfig : 0)).padStart(2, '0');
        }
    }

    if (document.getElementById("timerNorth")) document.getElementById("timerNorth").innerText = displayNS;
    if (document.getElementById("timerSouth")) document.getElementById("timerSouth").innerText = displayNS;
    if (document.getElementById("timerEast")) document.getElementById("timerEast").innerText = displayEW;
    if (document.getElementById("timerWest")) document.getElementById("timerWest").innerText = displayEW;

    updateLampColor("north", nLight);
    updateLampColor("south", sLight);
    updateLampColor("east", eLight);
    updateLampColor("west", wLight);
});

// --- CHỨC NĂNG 3: CẬP NHẬT GIAO DIỆN MÀU ĐÈN TƯƠNG TÁC ---
function updateLampColor(direction, color) {
    const redLamp = document.getElementById(`${direction}-red`);
    const yellowLamp = document.getElementById(`${direction}-yellow`);
    const greenLamp = document.getElementById(`${direction}-green`);

    if (redLamp) redLamp.classList.remove('active');
    if (yellowLamp) yellowLamp.classList.remove('active');
    if (greenLamp) greenLamp.classList.remove('active');

    if (color === "RED" && redLamp) redLamp.classList.add('active');
    if (color === "YELLOW" && yellowLamp) yellowLamp.classList.add('active');
    if (color === "GREEN" && greenLamp) greenLamp.classList.add('active');
}

// --- CHỨC NĂNG 4: HÀM GỌI API CHUNG TỚI ENDPOINT CONTROL ---
async function sendControlAPI(payloadData) {
    try {
        const response = await fetch(`${BASE_URL}/api/Control`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payloadData)
        });
        return response.ok;
    } catch (error) {
        console.error("Lỗi gọi API:", error);
        return false;
    }
}

// --- CHỨC NĂNG 5: SỰ KIỆN NÚT BẬT/TẮT HỆ THỐNG ---
document.addEventListener("DOMContentLoaded", () => {
    const btnOnOff = document.getElementById("btnOnOff");
    if (btnOnOff) {
        btnOnOff.addEventListener("click", async () => {
            if (!isConnected) {
                try {
                    await hubConnection.start();
                    isConnected = true;
                    btnOnOff.innerText = "NGẮT HỆ THỐNG";
                    btnOnOff.classList.replace("btn-success", "btn-danger");

                    const mainDashboard = document.getElementById("mainDashboard");
                    if (mainDashboard) mainDashboard.classList.remove("disabled-overlay");

                    // GỌI ĐÚNG API CỦA BẠN:
                    await sendControlAPI({ Action: "TOGGLE_SYSTEM", Value: true });
                    addLogToTable("Mở Hệ Thống", "ONLINE", "Bật kết nối giám sát real-time");
                } catch (err) {
                    console.error("Lỗi khi kết nối SignalR:", err);
                }
            } else {
                try {
                    await hubConnection.stop();
                    isConnected = false;
                    btnOnOff.innerText = "BẬT HỆ THỐNG";
                    btnOnOff.classList.replace("btn-danger", "btn-success");

                    const mainDashboard = document.getElementById("mainDashboard");
                    if (mainDashboard) mainDashboard.classList.add("disabled-overlay");

                    addLogToTable("Tắt Hệ Thống", "OFFLINE", "Đã ngắt cổng kết nối Web");
                } catch (err) {
                    console.error("Lỗi khi ngắt kết nối SignalR:", err);
                }
            }
        });
    }
});

// =======================================================
// CÁC HÀM XỬ LÝ ĐIỀU KHIỂN TỪ NÚT BẤM GIAO DIỆN
// =======================================================

function changeMode(modeName) {
    sendControlAPI({ Action: "CHANGE_MODE", Mode: modeName });
}

function triggerManualPhase(dir, lightColor) {
    sendControlAPI({ Action: "SET_LIGHT", Direction: dir, Light: lightColor });
}

function saveTimeConfig() {
    const timeGreenEl = document.getElementById("timeGreen");
    const timeYellowEl = document.getElementById("timeYellow");
    const timeRedEl = document.getElementById("timeRed");

    if (!timeGreenEl || !timeYellowEl || !timeRedEl) {
        alert("Lỗi giao diện: Không tìm thấy ô nhập thời gian.");
        return;
    }

    const gVal = parseInt(timeGreenEl.value);
    const yVal = parseInt(timeYellowEl.value);
    const rVal = parseInt(timeRedEl.value);

    // Validate dữ liệu để tránh nhập sai
    if (isNaN(gVal) || isNaN(yVal) || isNaN(rVal) || gVal < 1 || yVal < 1 || rVal < 1) {
        alert("Vui lòng nhập số giây hợp lệ (lớn hơn 0)!");
        return;
    }

    // GỌI ĐÚNG API LƯU CẤU HÌNH:
    sendControlAPI({
        Action: "SET_TIME",
        Green: gVal,
        Yellow: yVal,
        Red: rVal
    });

    alert("Đã lưu và áp dụng cấu hình thời gian mới!");
    addLogToTable("Cấu hình thời gian", "CONFIG", `Cài đặt: Xanh ${gVal}s, Vàng ${yVal}s, Đỏ ${rVal}s`);
}