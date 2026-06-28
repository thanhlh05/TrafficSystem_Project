using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("traffic_logs")]
    public class TrafficLog
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("phase_name")]
        public string? PhaseName { get; set; }

        [Column("north_light")]
        public string? NorthLight { get; set; }

        [Column("south_light")]
        public string? SouthLight { get; set; }

        [Column("east_light")]
        public string? EastLight { get; set; }

        [Column("west_light")]
        public string? WestLight { get; set; }

        [Column("remaining_time")]
        public int RemainingTime { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}