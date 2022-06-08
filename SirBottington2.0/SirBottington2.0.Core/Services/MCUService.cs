using Discord;
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
    public class MCUService : IMCUService
    {
        private readonly string _url = "https://www.whenisthenextmcufilm.com/api";
        public MCUService()
        {

        }

        private async Task<MCUCountdown> GetMCUCountdownAsync()
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(_url);

                var countdown = JsonConvert.DeserializeObject<MCUCountdown>(json);

                return countdown;
            }
        }

        public async Task<Embed> GetMCUCountownEmbedAsync()
        {
            var countdown = await GetMCUCountdownAsync();

            
            EmbedBuilder embedBuilder = new EmbedBuilder();

            if(countdown is null)
            {
                embedBuilder.WithTitle("Error")
                    .WithDescription("The server where the Countdown is coming from is struggling at the moment. Try again soon")
                    .WithColor(Color.Red);
            }
            else
            {
                embedBuilder.WithTitle(countdown.Title)
                    .WithDescription($"{countdown.Title} comes out {countdown.ReleaseDate}." +
                    $" That's in {countdown.DaysUntil} days.")
                    .WithImageUrl(countdown.PosterUrl);
            }
            return embedBuilder.Build();
        }
    }
}
