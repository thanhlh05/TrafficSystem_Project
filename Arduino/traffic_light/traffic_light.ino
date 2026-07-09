#include "LedControl.h"

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

LedControl lc = LedControl(A0, A1, A2, 1); 

int nsDisplayTime = 0;
int ewDisplayTime = 0;

//tgian macdinh
int nsGreenTime = 25;
int nsYellowTime = 3;
int ewGreenTime = 33;
int ewYellowTime = 3;

int currentMode = 0; // 0=AUTO, 1=MANUAL, 2=EMERGENCY
bool systemOn = true;
int lightN = 0, lightS = 0, lightE = 0, lightW = 0; // 0=RED, 1=GREEN, 2=YELLOW

int autoPhase = 0; // 0=NS_GREEN, 1=NS_YELLOW, 2=EW_GREEN, 3=EW_YELLOW
int remainingTime = 0;

unsigned long lastTick = 0;
unsigned long lastSend = 0;
unsigned long lastFlashTick = 0;
bool flashState = false;

void setup() {
  Serial.begin(9600);
  Serial.setTimeout(20); 

  int pins[] = {N_GREEN, N_YELLOW, N_RED, S_GREEN, S_YELLOW, S_RED, E_GREEN, E_YELLOW, E_RED, W_GREEN, W_YELLOW, W_RED};
  for (int i = 0; i < 12; i++) {
    pinMode(pins[i], OUTPUT);
    digitalWrite(pins[i], LOW);
  }

  lc.shutdown(0, false); 
  lc.setIntensity(0, 8);
  lc.clearDisplay(0);

  setAutoPhase(0);
}

void loop() {
  unsigned long now = millis();

  if (Serial.available() > 0) {
    String cmd = Serial.readStringUntil('\n');
    cmd.trim();
    handleCommand(cmd);

    sendStatus();       
    lastSend = millis(); 
  }

  if (systemOn && currentMode == 0 && now - lastTick >= 1000) {
    lastTick = now;
    remainingTime--;
    
    if (remainingTime <= 0) {
      autoPhase = (autoPhase + 1) % 4;
      setAutoPhase(autoPhase);
    }
    
    syncDisplays(); 
  
  }

  if (now - lastSend >= 1000) {
    lastSend = now;
    sendStatus();
  }

  if (systemOn && currentMode == 3 && now - lastFlashTick >= 500) { 
    lastFlashTick = now;
    flashState = !flashState; 
    int c = flashState ? 2 : 4; //2 vang, 4 tat
    lightN = c; lightS = c; lightE = c; lightW = c;
    applyLights(); 
    
    sendStatus();
    lastSend = now;
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
    else if (mode == "MANUAL") { currentMode = 1; syncDisplays(); }
    else if (mode == "EMERGENCY") { currentMode = 2; setEmergency(); }
    else if (mode == "FLASH") { currentMode = 3; setFlash(); }
  }
  else if (action == "SET_LIGHT") {
    if (currentMode != 1) return; 
    String dir = parts[2]; 
    String color = parts[3];
    
    if (dir == "BN") {
      int c = (color == "XANH") ? 1 : ((color == "VANG") ? 2 : 0);
      lightN = c; lightS = c;
      lightE = 0; lightW = 0;
    }
    else if (dir == "DT") {
      int c = (color == "XANH") ? 1 : ((color == "VANG") ? 2 : 0);
      lightE = c; lightW = c;
      lightN = 0; lightS = 0;
    }
    applyLights();
    syncDisplays();
  }
  else if (action == "SET_TIME") {
    String timeData = parts[2];
    int firstComma = timeData.indexOf(',');
    int secondComma = timeData.indexOf(',', firstComma + 1);
    
    if (firstComma > 0 && secondComma > firstComma) {
      int g = timeData.substring(0, firstComma).toInt();
      int y = timeData.substring(firstComma + 1, secondComma).toInt();
      int r = timeData.substring(secondComma + 1).toInt();
      
      nsGreenTime = g;
      nsYellowTime = y;
      ewGreenTime = r - y; 
      ewYellowTime = y;
      
      if (currentMode == 0) {
        setAutoPhase(autoPhase);
      }
    }
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
      lc.clearDisplay(0); 
    } else {
      if (currentMode == 0) setAutoPhase(autoPhase);
      else if (currentMode == 2) setEmergency();
      else { applyLights(); syncDisplays(); }
    }
  }
  else if (action == "CONFIG") {
    String param = parts[2];
    int val = parts[3].toInt();
    if (param == "NS_GREEN") nsGreenTime = val;
    else if (param == "NS_YELLOW") nsYellowTime = val;
    else if (param == "EW_GREEN") ewGreenTime = val;
    else if (param == "EW_YELLOW") ewYellowTime = val;
  }
}

//gui trang thai
void sendStatus() {
  if (!systemOn) {
    Serial.println("STATUS:OFF,OFF,OFF,OFF,0,OFF");
    return;
  }
  String modeStr = "AUTO";
  if (currentMode == 1) modeStr = "MANUAL";
  else if (currentMode == 2) modeStr = "EMERGENCY";
  else if (currentMode == 3) modeStr = "FLASH";

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
  if (c == 4) return "OFF";
  return "RED";
}

//dieukhien&pha
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
    case 0: 
      lightN = 1; lightS = 1; lightE = 0; lightW = 0;
      remainingTime = nsGreenTime;
      break;
    case 1: 
      lightN = 2; lightS = 2; lightE = 0; lightW = 0;
      remainingTime = nsYellowTime;
      break;
    case 2:
      lightN = 0; lightS = 0; lightE = 1; lightW = 1;
      remainingTime = ewGreenTime;
      break;
    case 3:
      lightN = 0; lightS = 0; lightE = 2; lightW = 2;
      remainingTime = ewYellowTime;
      break;
  }
  applyLights();
  syncDisplays();//cap nhat man hinh lap tuc khi vua doi pha
}

void setEmergency() {
  lightN = 0; lightS = 0; lightE = 0; lightW = 0; 
  remainingTime = 0;
  applyLights();
  syncDisplays();//cap nhat man hinh so 0
}

void allOff() {
  int pins[] = {N_GREEN, N_YELLOW, N_RED, S_GREEN, S_YELLOW, S_RED, E_GREEN, E_YELLOW, E_RED, W_GREEN, W_YELLOW, W_RED};
  for (int i = 0; i < 12; i++) digitalWrite(pins[i], LOW);
}

void setOneLight(String dir, String color) {
  int c = 0; 
  if (color == "GREEN" || color == "3") c = 1;
  else if (color == "YELLOW" || color == "2") c = 2;
  else if (color == "RED" || color == "1") c = 0;

  if (dir == "NORTH") lightN = c;
  else if (dir == "SOUTH") lightS = c;
  else if (dir == "EAST") lightE = c;
  else if (dir == "WEST") lightW = c;
  
  applyLights();
}

void syncDisplays() {
  if (!systemOn) return;

  if (currentMode == 1 || currentMode == 3) {
    // Chế độ thủ công: Hiện "--" ở 4 hướng
    updateCountdownDisplays(-1, -1);
  } 
  else if (currentMode == 2) {
    // Chế độ khẩn cấp: Hiện "00" ở 4 hướng
    updateCountdownDisplays(0, 0);
  } 
  else if (currentMode == 0) {
    if (autoPhase == 0) {
      nsDisplayTime = remainingTime;
      ewDisplayTime = remainingTime + nsYellowTime; 
    } 
    else if (autoPhase == 1) {
      nsDisplayTime = remainingTime;
      ewDisplayTime = remainingTime; 
    } 
    else if (autoPhase == 2) {
      ewDisplayTime = remainingTime;
      nsDisplayTime = remainingTime + ewYellowTime; 
    } 
    else if (autoPhase == 3) {
      ewDisplayTime = remainingTime;
      nsDisplayTime = remainingTime; 
    }
    updateCountdownDisplays(nsDisplayTime, ewDisplayTime);
  }
}

void updateCountdownDisplays(int nsValue, int ewValue) {
  if (nsValue >= 0) {
    lc.setDigit(0, 0, nsValue % 10, false);
    lc.setDigit(0, 1, (nsValue / 10) % 10, false);
    lc.setDigit(0, 2, nsValue % 10, false);
    lc.setDigit(0, 3, (nsValue / 10) % 10, false);
  } else {
    lc.setRow(0, 0, 0b00000001);
    lc.setRow(0, 1, 0b00000001); 
    lc.setRow(0, 2, 0b00000001); 
    lc.setRow(0, 3, 0b00000001); 
  }

  if (ewValue >= 0) {
    lc.setDigit(0, 4, ewValue % 10, false);
    lc.setDigit(0, 5, (ewValue / 10) % 10, false);
    lc.setDigit(0, 6, ewValue % 10, false);
    lc.setDigit(0, 7, (ewValue / 10) % 10, false);
  } else {
    lc.setRow(0, 4, 0b00000001);
    lc.setRow(0, 5, 0b00000001); 
    lc.setRow(0, 6, 0b00000001); 
    lc.setRow(0, 7, 0b00000001); 
  }
}

void setFlash() {
  lightN = 4; lightS = 4; lightE = 4; lightW = 4;
  remainingTime = 0;
  flashState = false;
  applyLights();
  syncDisplays();
}