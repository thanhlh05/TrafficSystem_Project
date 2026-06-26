using Backend.Hubs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControlController : ControllerBase
    {
        private readonly IHubContext<TrafficHub> _hub;

        public ControlController(IHubContext<TrafficHub> hub)
        {
            _hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> PostControl([FromBody] ControlRequest req)
        {
            // TODO: Thành viên Backend bổ sung ghi log vào DB

            // Gửi lệnh xuống Arduino thông qua Serial Bridge
            switch (req.Action)
            {
                case "CHANGE_MODE":
                    SerialBridgeService.Instance?.SendCommandToArduino("CHANGE_MODE", req.Mode);
                    await _hub.Clients.All.SendAsync("ReceiveCommand", new
                    {
                        action = "CHANGE_MODE",
                        mode = req.Mode
                    });
                    break;

                case "SET_LIGHT":
                    if (req.Direction == null || req.Light == null)
                    {
                        return BadRequest(new
                        {
                            error = "Thiếu thông tin hướng hoặc màu đèn!"
                        });
                    }

                    SerialBridgeService.Instance?.SendCommandToArduino(
                        "SET_LIGHT",
                        req.Direction,
                        req.Light);
                    break;

                case "RESTART":
                    SerialBridgeService.Instance?.SendCommandToArduino("RESTART");
                    break;

                case "TOGGLE_SYSTEM":
                    SerialBridgeService.Instance?.SendCommandToArduino(
                        "TOGGLE",
                        req.Value == true ? "ON" : "OFF");
                    break;

                default:
                    return BadRequest(new
                    {
                        error = "Hành động điều khiển không hợp lệ!"
                    });
            }

            return Ok(new
            {
                message = "Lệnh đã truyền thành công!",
                executedAt = DateTime.UtcNow
            });
        }
    }

    public class ControlRequest
    {
        public string? Action { get; set; }
        public string? Mode { get; set; }
        public string? Direction { get; set; }
        public string? Light { get; set; }
        public bool? Value { get; set; }
    }
}