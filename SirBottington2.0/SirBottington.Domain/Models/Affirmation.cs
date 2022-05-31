using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Domain.Models
{
    public class Affirmation
    {
        [JsonProperty("affirmation")]
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
