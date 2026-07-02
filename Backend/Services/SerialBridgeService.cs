using Backend.Data;
using Backend.Hubs;
using Backend.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class SerialBridgeService : BackgroundService
    {
        private SerialPort? _port;
        private readonly IHubContext<TrafficHub> _hub;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SerialBridgeService> _logger;

        public static SerialBridgeService? Instance { get; private set; }

        public SerialBridgeService(
            IHubContext<TrafficHub> hub,
            IServiceScopeFactory scopeFactory,
            ILogger<SerialBridgeService> logger)
        {
            _hub = hub;
            _scopeFactory = scopeFactory;
            _logger = logger;
            Instance = this;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _port = new SerialPort("COM4", 9600);
                _port.NewLine = "\n";
                _port.Open();
                _logger.LogInformation(">>> Cổng kết nối mô phỏng COM4 đã mở thành công.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"[WARNING] Không thể mở cổng COM4 ({ex.Message}). Đang chạy chế độ Không có mạch.");
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_port != null && _port.IsOpen && _port.BytesToRead > 0)
                    {
                        string line = _port.ReadLine().Trim();
                        if (line.StartsWith("STATUS:"))
                            await HandleStatusLine(line);
                    }

                    await Task.Delay(100, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Serial read error: {ex.Message}");
                    await Task.Delay(1000, stoppingToken);
                }
            }

            _port?.Close();
        }

        private async Task HandleStatusLine(string line)
        {
            var data = line.Substring(7).Split(',');

            if (data.Length < 6)
                return;

            var statusDto = new
            {
                North = data[0],
                South = data[1],
                East = data[2],
                West = data[3],
                RemainingTime = int.TryParse(data[4], out int t) ? t : 0,
                Mode = data.Length > 5 ? data[5] : "AUTO",
                Phase = data.Length > 6 ? data[6] : "UNKNOWN",
                UpdatedAt = DateTime.UtcNow
            };

            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var liveStatus = new TrafficStatus
                {
                    NorthLight = statusDto.North?.ToUpper(),
                    SouthLight = statusDto.South?.ToUpper(),
                    EastLight = statusDto.East?.ToUpper(),
                    WestLight = statusDto.West?.ToUpper(),
                    RemainingTime = statusDto.RemainingTime,
                    Mode = statusDto.Mode,
                    Phase = statusDto.Phase,
                    UpdatedAt = statusDto.UpdatedAt
                };

                var current = await db.TrafficStatuses.FirstOrDefaultAsync();

                if (current == null)
                {
                    db.TrafficStatuses.Add(liveStatus);
                }
                else
                {
                    current.NorthLight = liveStatus.NorthLight;
                    current.SouthLight = liveStatus.SouthLight;
                    current.EastLight = liveStatus.EastLight;
                    current.WestLight = liveStatus.WestLight;
                    current.RemainingTime = liveStatus.RemainingTime;
                    current.Mode = liveStatus.Mode;
                    current.Phase = liveStatus.Phase;
                    current.UpdatedAt = DateTime.UtcNow;
                }
                db.TrafficLogs.Add(new TrafficLog
                {
                    PhaseName = statusDto.Phase,
                    NorthLight = statusDto.North,
                    SouthLight = statusDto.South,
                    EastLight = statusDto.East,
                    WestLight = statusDto.West,
                    RemainingTime = statusDto.RemainingTime,
                    CreatedAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();
            }

            await _hub.Clients.All.SendAsync("ReceiveStatus", statusDto);
        }

        public void SendCommandToArduino(
            string action,
            string? param1 = null,
            string? param2 = null)
        {
            if (_port == null || !_port.IsOpen)
                return;

            string cmd = $"CMD:{action}";

            if (param2 != null)
                cmd += $":{param1}:{param2}";
            else if (param1 != null)
                cmd += $":{param1}";

            _port.WriteLine(cmd);
            _logger.LogInformation($"Sent to Arduino: {cmd}");
        }
        private int ConvertLightTextToNo(string lightText)
        {
            if (string.IsNullOrEmpty(lightText)) return 0; // Mặc định là Đỏ (0) nếu null

            switch (lightText.ToUpper())
            {
                case "RED": return 0;
                case "GREEN": return 1;
                case "YELLOW": return 2;
                default: return 0;
            }
        }
    }
}