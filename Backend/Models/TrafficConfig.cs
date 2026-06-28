using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("traffic_config")]
    public class TrafficConfig
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("ns_green_time")]
        public int NsGreenTime { get; set; }

        [Column("ns_yellow_time")]
        public int NsYellowTime { get; set; }

        [Column("ew_green_time")]
        public int EwGreenTime { get; set; }

        [Column("ew_yellow_time")]
        public int EwYellowTime { get; set; }

        [Column("mode")]
        public string Mode { get; set; } = "AUTO";

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}