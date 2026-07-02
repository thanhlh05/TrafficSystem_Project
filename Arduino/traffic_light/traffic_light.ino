#define N_GREEN 2
#define N_YELLOW 3
#define N_RED 4
#define S_GREEN 5
#define S_YELLOW 6
#define S_RED 7
#define E_GREEN 8
#define E_YELLOW 9
#define E_RED 10
#define W_GREEN 11
#define W_YELLOW 12
#define W_RED 13

// Trục 1: Bắc - Nam (Đèn 1)
int nsGreenTime = 25;
int nsYellowTime = 3;

// Trục 2: Đông - Tây (Đèn 2)
int ewGreenTime = 33;
int ewYellowTime = 3;

int currentMode = 0; // 0=AUTO, 1=MANUAL, 2=EMERGENCY
bool systemOn = true;
int lightN = 0, lightS = 0, lightE = 0, lightW = 0; // 0=RED, 1=GREEN, 2=YELLOW

int autoPhase = 0; // 0=NS_GREEN, 1=NS_YELLOW, 2=EW_GREEN, 3=EW_YELLOW
int remainingTime = 0;

unsigned long lastTick = 0;
unsigned long lastSend = 0;

void setup() {
  Serial.begin(9600);
  Serial.setTimeout(20); // THÊM DÒNG NÀY: Giới hạn thời gian chờ chống kẹt (lag) loop
  
  int pins[] = {N_GREEN, N_YELLOW, N_RED, S_GREEN, S_YELLOW, S_RED, E_GREEN, E_YELLOW, E_RED, W_GREEN, W_YELLOW, W_RED};
  for (int i = 0; i < 12; i++) {
    pinMode(pins[i], OUTPUT);
    digitalWrite(pins[i], LOW);
  }
  setAutoPhase(0);
}

void loop() {
  unsigned long now = millis();

  // 1. Đọc lệnh từ Backend (Không gây blocking)
  if (Serial.available() > 0) {
    String cmd = Serial.readStringUntil('\n');
    cmd.trim();
    handleCommand(cmd);

    sendStatus();       // Ép Arduino gửi trạng thái mới về WinForms ngay lập tức
    lastSend = millis(); // Reset lại bộ đếm để không bị gửi trùng lặp
  }

  // 2. Đếm ngược chu trình tự động
  if (systemOn && currentMode == 0 && now - lastTick >= 1000) {
    lastTick = now;
    remainingTime--;
    if (remainingTime <= 0) {
      autoPhase = (autoPhase + 1) % 4;
      setAutoPhase(autoPhase);
    }
  }

  // 3. Đẩy thông tin trạng thái lên Web Server mỗi giây
  if (now - lastSend >= 1000) {
    lastSend = now;
    sendStatus();
  }
}

void handleCommand(String raw) {
  if (!raw.startsWith("CMD:")) return;
  
  String parts[4];
  int count = 0;
  int start = 0;
  
  for (int i = 0; i <= raw.length() && count < 4; i++) {
    if (i == raw.length() || raw[i] == ':') {
      parts[count++] = raw.substring(start, i);
      start = i + 1;
    }
  }

  String action = parts[1];
  
  if (action == "CHANGE_MODE") {
    String mode = parts[2];
    if (mode == "AUTO") { currentMode = 0; setAutoPhase(0); }
    else if (mode == "MANUAL") { currentMode = 1; }
    else if (mode == "EMERGENCY") { currentMode = 2; setEmergency(); }
  }
  else if (action == "SET_LIGHT") {
    if (currentMode != 1) return; // Chỉ cho phép khi ở mode MANUAL
    String dir = parts[2];
    String color = parts[3];
    setOneLight(dir, color);
  }
  else if (action == "RESTART") {
    currentMode = 0;
    systemOn = true;
    setAutoPhase(0);
  }
  else if (action == "TOGGLE") {
    systemOn = (parts[2] == "ON");
    if (!systemOn) {
      allOff();
    } else {
      // Đã fix lỗi mất trạng thái Mode khi bật lại hệ thống
      if (currentMode == 0) setAutoPhase(autoPhase);
      else if (currentMode == 2) setEmergency();
      else applyLights(); 
    }
  }
  else if (action == "CONFIG") {
    String param = parts[2];
    int val = parts[3].toInt();
    // Cập nhật cấu hình độc lập cho từng trục giao thông
    if (param == "NS_GREEN") nsGreenTime = val;
    else if (param == "NS_YELLOW") nsYellowTime = val;
    else if (param == "EW_GREEN") ewGreenTime = val;
    else if (param == "EW_YELLOW") ewYellowTime = val;
  }
  else if (raw.startsWith("CMD:SET_TIME")) {
    int firstColon = raw.indexOf(':');
    int secondColon = raw.indexOf(':', firstColon + 1);
    int thirdColon = raw.indexOf(':', secondColon + 1);

    if (secondColon > 0 && thirdColon > 0) {
      // Tách chuỗi lấy số giây Xanh và Vàng
      String greenStr = raw.substring(secondColon + 1, thirdColon);
      String yellowStr = raw.substring(thirdColon + 1);

      // Ép kiểu sang số nguyên và cập nhật vào biến toàn cục của Arduino
      nsGreenTime = greenStr.toInt();
      ewGreenTime = greenStr.toInt();
      nsYellowTime = yellowStr.toInt();
      ewYellowTime = yellowStr.toInt();

      // Ép Arduino gửi trạng thái mới lên màn hình ngay lập tức để đồng bộ
      sendStatus();
      lastSend = millis();
    }
  }
}

void sendStatus() {
  if (!systemOn) {
    Serial.println("STATUS:OFF,OFF,OFF,OFF,0,OFF");
    return;
  }
  String modeStr = "AUTO";
  if (currentMode == 1) modeStr = "MANUAL";
  else if (currentMode == 2) modeStr = "EMERGENCY";

  Serial.print("STATUS:");
  Serial.print(colorStr(lightN)); Serial.print(",");
  Serial.print(colorStr(lightS)); Serial.print(",");
  Serial.print(colorStr(lightE)); Serial.print(",");
  Serial.print(colorStr(lightW)); Serial.print(",");
  Serial.print(remainingTime); Serial.print(",");
  Serial.println(modeStr);
}

String colorStr(int c) {
  if (c == 1) return "GREEN";
  if (c == 2) return "YELLOW";
  return "RED";
}

void applyLights() {
  if (!systemOn) return;
  digitalWrite(N_GREEN, lightN == 1 ? HIGH : LOW);
  digitalWrite(N_YELLOW, lightN == 2 ? HIGH : LOW);
  digitalWrite(N_RED, lightN == 0 ? HIGH : LOW);

  digitalWrite(S_GREEN, lightS == 1 ? HIGH : LOW);
  digitalWrite(S_YELLOW, lightS == 2 ? HIGH : LOW);
  digitalWrite(S_RED, lightS == 0 ? HIGH : LOW);

  digitalWrite(E_GREEN, lightE == 1 ? HIGH : LOW);
  digitalWrite(E_YELLOW, lightE == 2 ? HIGH : LOW);
  digitalWrite(E_RED, lightE == 0 ? HIGH : LOW);

  digitalWrite(W_GREEN, lightW == 1 ? HIGH : LOW);
  digitalWrite(W_YELLOW, lightW == 2 ? HIGH : LOW);
  digitalWrite(W_RED, lightW == 0 ? HIGH : LOW);
}

void setAutoPhase(int phase) {
  autoPhase = phase;
  switch (phase) {
    case 0: // Bắc - Nam Xanh (25s), Đông - Tây Đỏ (36s)
      lightN = 1; lightS = 1; lightE = 0; lightW = 0;
      remainingTime = nsGreenTime;
      break;
    case 1: // Bắc - Nam Vàng (3s), Đông - Tây Đỏ
      lightN = 2; lightS = 2; lightE = 0; lightW = 0;
      remainingTime = nsYellowTime;
      break;
    case 2: // Đông - Tây Xanh (33s), Bắc - Nam Đỏ (28s)
      lightN = 0; lightS = 0; lightE = 1; lightW = 1;
      remainingTime = ewGreenTime;
      break;
    case 3: // Đông - Tây Vàng (3s), Bắc - Nam Đỏ
      lightN = 0; lightS = 0; lightE = 2; lightW = 2;
      remainingTime = ewYellowTime;
      break;
  }
  applyLights();
}

void setEmergency() {
  lightN = 0; lightS = 0; lightE = 0; lightW = 0; 
  remainingTime = 0;
  applyLights();
}

void allOff() {
  int pins[] = {N_GREEN, N_YELLOW, N_RED, S_GREEN, S_YELLOW, S_RED, E_GREEN, E_YELLOW, E_RED, W_GREEN, W_YELLOW, W_RED};
  for (int i = 0; i < 12; i++) digitalWrite(pins[i], LOW);
}

void setOneLight(String dir, String color) {
  int c = 0; // Mặc định là Đỏ (0)
  
  // Hỗ trợ nhận diện cả CHỮ (từ test Serial) và SỐ (từ WinForms C#)
  if (color == "GREEN" || color == "3") c = 1;
  else if (color == "YELLOW" || color == "2") c = 2;
  else if (color == "RED" || color == "1") c = 0;

  if (dir == "NORTH") lightN = c;
  else if (dir == "SOUTH") lightS = c;
  else if (dir == "EAST") lightE = c;
  else if (dir == "WEST") lightW = c;
  
  applyLights();
}