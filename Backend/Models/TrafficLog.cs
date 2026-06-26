using System;

namespace Backend.Models
{
    public class TrafficLog
    {
        public long Id { get; set; }
        public string? PhaseName { get; set; }
        public string? NorthLight { get; set; }
        public string? SouthLight { get; set; }
        public string? EastLight { get; set; }
        public string? WestLight { get; set; }
        public int RemainingTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}