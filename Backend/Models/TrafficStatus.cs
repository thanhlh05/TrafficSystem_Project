using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("traffic_status")]
    public class TrafficStatus
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("north_light")]
        public string NorthLight { get; set; }

        [Column("south_light")]
        public string SouthLight { get; set; }

        [Column("east_light")]
        public string EastLight { get; set; }

        [Column("west_light")]
        public string WestLight { get; set; }

        [Column("remaining_time")]
        public int RemainingTime { get; set; }

        [Column("phase")]
        public string Phase { get; set; }

        [Column("mode")]
        public string Mode { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        
    }
}