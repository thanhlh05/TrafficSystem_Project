using Backend.Data;
using Backend.Models;
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
        public async Task<IActionResult> UpdateConfig(
            TrafficConfig config)
        {
            _context.TrafficConfigs.Update(config);
            await _context.SaveChangesAsync();

            return Ok(config);
        }
    }
}