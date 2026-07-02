using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrafficSystem_WinForm
{
    internal class TrafficStatus
    {
        [JsonPropertyName("north")]
        public string NorthLight { get; set; }

        [JsonPropertyName("south")]
        public string SouthLight { get; set; }

        [JsonPropertyName("east")]
        public string EastLight { get; set; }

        [JsonPropertyName("west")]
        public string WestLight { get; set; }

        [JsonPropertyName("remainingTime")]
        public int RemainingTime { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("phase")]
        public string Phase { get; set; }
    }
}
