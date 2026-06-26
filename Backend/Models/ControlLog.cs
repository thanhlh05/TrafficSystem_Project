using System;

namespace Backend.Models
{
    public class ControlLog
    {
        public long Id { get; set; }
        public string? ActionName { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}