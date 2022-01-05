using SirBottington.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SirBottington.Services.API
{
    public class GetXKCDAPI : IGetXKCDAPI
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GetXKCDAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IXKCDModel> GetLatest()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync("https://xkcd.com/info.0.json");

            XKCDModel comic = JsonSerializer.Deserialize<XKCDModel>(response);

            return comic;
        }

        public async Task<IXKCDModel> GetSpecific(int number)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync($"http://xkcd.com/{number}/info.0.json");

            XKCDModel comic = JsonSerializer.Deserialize<XKCDModel>(response);

            return comic;
        }
    }
}
