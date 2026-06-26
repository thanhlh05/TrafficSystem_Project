using System;

namespace Backend.Models
{
    public class TrafficStatus
    {
        public long Id { get; set; }
        public string? NorthLight { get; set; }
        public string? SouthLight { get; set; }
        public string? EastLight { get; set; }
        public string? WestLight { get; set; }
        public int RemainingTime { get; set; }
        public string? Mode { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}