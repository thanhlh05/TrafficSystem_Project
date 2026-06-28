using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("traffic")]
        public async Task<IActionResult> GetTrafficLogs()
        {
            return Ok(await _context.TrafficLogs.ToListAsync());
        }

        [HttpGet("control")]
        public async Task<IActionResult> GetControlLogs()
        {
            return Ok(await _context.ControlLogs.ToListAsync());
        }
    }
}