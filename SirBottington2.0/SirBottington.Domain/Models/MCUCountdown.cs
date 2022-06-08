using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Domain.Models
{
    public class MCUCountdown
    {
        [JsonProperty("days_until")]
        public int DaysUntil { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public string Overview { get; set; }

        [JsonProperty("poster_url")]
        public string PosterUrl { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("following_property")]
        public MCUCountdown? FollowingProduction { get; set; }

    }
}
