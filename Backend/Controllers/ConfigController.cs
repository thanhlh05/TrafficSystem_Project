using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/config")]
    public class ConfigController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConfigController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetConfig()
        {
            var config = await _context.TrafficConfigs
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            return Ok(config);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConfig(TrafficConfig incomingConfig)
        {
            try
            {
                // 1. TỰ ĐỘNG TÌM BẢN GHI MỚI NHẤT TRONG DB ĐỂ CẬP NHẬT (Tránh lỗi thiếu ID từ WinForms)
                var existingConfig = await _context.TrafficConfigs
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();

                if (existingConfig == null)
                {
                    // Nếu Database hoàn toàn trống, tiến hành thêm mới bản ghi đầu tiên
                    incomingConfig.UpdatedAt = DateTime.UtcNow;
                    _context.TrafficConfigs.Add(incomingConfig);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Nếu đã có cấu hình, ghi đè các giá trị thời gian mới vào bản ghi cũ đó
                    existingConfig.NsGreenTime = incomingConfig.NsGreenTime;
                    existingConfig.NsYellowTime = incomingConfig.NsYellowTime;
                    existingConfig.EwGreenTime = incomingConfig.EwGreenTime;
                    existingConfig.EwYellowTime = incomingConfig.EwYellowTime;
                    existingConfig.Mode = incomingConfig.Mode ?? "AUTO";
                    existingConfig.UpdatedAt = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                }

                // 2. BẮN LỆNH XUỐNG ARDUINO (Tận dụng hàm CONFIG độc lập có sẵn của Arduino)
                try
                {
                    // Gửi cấu hình từng cột cho trục Bắc - Nam
                    SerialBridgeService.Instance?.SendCommandToArduino(
                        "CONFIG",
                        "NS_GREEN",
                        incomingConfig.NsGreenTime.ToString());

                    await Task.Delay(50);

                    SerialBridgeService.Instance?.SendCommandToArduino(
                        "CONFIG",
                        "NS_YELLOW",
                        incomingConfig.NsYellowTime.ToString());

                    await Task.Delay(50);

                    SerialBridgeService.Instance?.SendCommandToArduino(
                        "CONFIG",
                        "EW_GREEN",
                        incomingConfig.EwGreenTime.ToString());

                    await Task.Delay(50);

                    SerialBridgeService.Instance?.SendCommandToArduino(
                        "CONFIG",
                        "EW_YELLOW",
                        incomingConfig.EwYellowTime.ToString());

                    await Task.Delay(50);

                    SerialBridgeService.Instance?.SendCommandToArduino(
                        "CHANGE_MODE",
                        "AUTO");
                }
                catch (Exception serialEx)
                {
                    Console.WriteLine($"[CẢNH BÁO SERIAL]: Chưa gửi được lên Arduino. Chi tiết: {serialEx.Message}");
                }

                return Ok(incomingConfig);
            }
            catch (Exception ex)
            {
                // Nếu có bất kỳ lỗi hệ thống nào khác, trả về thông báo lỗi chi tiết thay vì lỗi 500 chung chung
                return StatusCode(500, $"Lỗi xử lý Backend: {ex.Message}");
            }
        }
    }
}