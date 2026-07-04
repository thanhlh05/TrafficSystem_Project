\# 🚦 Hệ Thống Giám Sát Và Điều Khiển Đèn Giao Thông Thông Minh (IoT)



Dự án xây dựng hệ thống giám sát và điều khiển chu trình đèn giao thông tại ngã tư. Hệ thống hỗ trợ điều khiển luân phiên đa nền tảng (Windows Forms App và Web App) thông qua một trung tâm Backend quản lý trạng thái thời gian thực.



\---



\## 🚀 Tính Năng Cốt Lõi

\* \*\*Mô phỏng phần cứng thực tế:\*\* Sử dụng mạch Arduino trong môi trường Proteus để chạy chu trình đèn.

\* \*\*Đồng bộ thời gian thực (Real-time):\*\* Sử dụng \*\*SignalR\*\* để đẩy dữ liệu đếm giây từ mạch lên các ứng dụng giám sát ngay lập tức mà không bị trễ.

\* \*\*Điều khiển đa chế độ:\*\*

&#x20; \* \*\*Tự động (Auto):\*\* Chạy theo cấu hình thời gian nạp sẵn.

&#x20; \* \*\*Thủ công (Manual):\*\* Chỉnh đèn từng hướng theo ý muốn.

&#x20; \* \*\*Cấu hình thời gian:\*\* Thay đổi trực tiếp số giây của các đèn từ xa.

\* \*\*Cơ chế Master Switch:\*\* Cho chủ động kết nối/ngắt kết nối luồng dữ liệu.

\* \*\*Reset thông minh:\*\* Tự động khôi phục mạch Proteus về trạng thái Auto mặc định khi client ngắt kết nối (`RESTART` API).



\---



\## 🛠️ Yêu Cầu Phần Mềm (Prerequisites)



Để chạy được dự án này, máy tính của bạn cần cài đặt các công cụ sau:



1\. \*\*Lập trình \& Chạy Code:\*\*

&#x20;  \* \[Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (Cài đặt thêm gói `.NET Desktop Development` và `ASP.NET and web development`).

&#x20;  \* \[.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download)



2\. \*\*Mô Phỏng Phần Cứng:\*\*

&#x20;  \* \[Proteus Design Suite](Phiên bản 8.x trở lên).

&#x20;  \* Thư viện \*\*Arduino IDE\*\* (để biên dịch xuất file `.hex`) hoặc nạp trực tiếp file hex vào chip trong Proteus.



3\. \*\*Kết Nối Trung Gian (Cực kỳ quan trọng):\*\*

&#x20;  \* \[Virtual Serial Port Driver (VSPD)](https://www.virtual-serial-port.org/user-guides/standard/installation.html) hoặc phần mềm tạo cổng COM ảo tương đương để tạo cặp cổng kết nối tiếp (Ví dụ: `COM1` <-> `COM2`).



\---



\## 💻 Hướng Dẫn Chạy Dự Án Chi Tiết (Plug \& Play)



Dự án đã được cấu hình sẵn cổng COM và chế độ chạy đồng thời. Bạn chỉ cần thực hiện đúng 3 bước siêu tốc sau:



\### Bước 1: Tạo cặp cổng COM ảo 3 và 4

\* Mở phần mềm \*\*Virtual Serial Port Driver (VSPD)\*\* lên.

\* Tạo một cặp cổng ảo nối tiếp nhau chính xác là: \*\*`COM3`\*\* và \*\*`COM4`\*\*. 

\*(Hệ thống code đã được cài đặt mặc định kết nối qua cặp cổng này nên không cần chỉnh sửa gì thêm).\*



\### Bước 2: Khởi động mạch mô phỏng Proteus

\* Mở file mô phỏng mạch đèn giao thông bằng \*\*Proteus\*\*.

\* Click đúp vào linh kiện \*\*COMPIM\*\* (Cổng Serial trên Proteus), cấu hình cổng là \*\*`COM3`\*\* và Baud Rate là \*\*`9600`\*\*.

\* Click đúp vào bo mạch \*\*Arduino\*\*, trỏ đường dẫn file `.hex` đến file code mạch của bạn.

\* Ấn nút \*\*Play (Run)\*\* ở góc dưới Proteus để mạch bắt đầu chạy ngầm.



\### Bước 3: Chạy Solution trên Visual Studio

\* Dùng Visual Studio mở file giải pháp (`.sln`) của dự án.

\* Dự án đã được thiết lập sẵn chế độ \*\*Multiple Startup Projects\*\* (Chạy đa dự án cùng lúc). Bạn chỉ cần nhấn phím \*\*`F5`\*\* (hoặc nút \*\*Start\*\* trên thanh công cụ).

\* Ngay lập tức, cả giao diện \*\*WinForms App\*\* và \*\*Backend API\*\* sẽ tự động khởi chạy song song.



\---



\## 🕹️ Hướng Dẫn Vận Hành Trên WinForm

1\. Khi vừa mở lên, các nút chức năng điều khiển sẽ tạm thời mờ đi để đảm bảo an toàn hệ thống.

2\. Bạn bấm nút \*\*`BẬT KẾT NỐI`\*\*: Giao diện sẽ sáng lên, WinForm kích hoạt SignalR nhận luồng dữ liệu hiển thị, số giây từ Proteus sẽ lập tức nhảy đồng bộ trên màn hình!

3\. Lúc này bạn có thể thoải mái điều khiển các chế độ Tự động, Thủ công hoặc Thay đổi thời gian đèn.

4\. Khi muốn tắt để chuyển giao quyền điều khiển sang Web: Bấm \*\*`NGẮT KẾT NỐI`\*\*. WinForm sẽ đóng băng an toàn, đồng thời tự động gửi lệnh `RESTART` ép mạch Proteus xóa các chế độ kẹt để quay về chu trình Auto ban đầu.

