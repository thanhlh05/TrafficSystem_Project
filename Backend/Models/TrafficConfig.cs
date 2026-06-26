using System;

namespace Backend.Models
{
    public class TrafficConfig
    {
        public long Id { get; set; }
        public int GreenTime { get; set; }
        public int YellowTime { get; set; }
        public int RedTime { get; set; }
        public string Mode { get; set; } = "AUTO";
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}