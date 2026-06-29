using Backend.Data;
using Backend.Hubs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/arduino")]
    public class ArduinoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<TrafficHub> _hub;

        public ArduinoController(
            AppDbContext context,
            IHubContext<TrafficHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateStatus(
            [FromBody] TrafficStatus status)
        {
            status.UpdatedAt = DateTime.UtcNow;

            var current =
                await _context.TrafficStatuses
                    .FirstOrDefaultAsync();

            if (current == null)
            {
                _context.TrafficStatuses.Add(status);
            }
            else
            {
                current.NorthLight = status.NorthLight;
                current.SouthLight = status.SouthLight;
                current.EastLight = status.EastLight;
                current.WestLight = status.WestLight;
                current.RemainingTime = status.RemainingTime;
                current.Mode = status.Mode;
                current.Phase = status.Phase;
                current.UpdatedAt = status.UpdatedAt;
            }

            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync(
                "ReceiveStatus",
                status
            );

            return Ok();
        }
    }
}