using Newtonsoft.Json;
using SirBottington.Core.Interfaces;
using SirBottington.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Core.Services
{
    public class EHugService : IEHugService
    {
        private readonly string _url = "https://www.affirmations.dev/";

        public async Task<Affirmation> GetEHugAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetStringAsync(_url);

                return JsonConvert.DeserializeObject<Affirmation>(json);
            }
        }

    }
}
