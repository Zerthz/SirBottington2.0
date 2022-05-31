using Discord.Commands;
using Microsoft.Extensions.Configuration;
using SirBottington.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        private readonly IConfiguration _configuration;
        private readonly IEHugService _eHugService;

        public Commands(IConfiguration configuration, 
                     IEHugService eHugService)   
        {
            _configuration = configuration;
            _eHugService = eHugService;
        }

        [Command("vibes")]
        [Summary("")]
        public async Task VibesAsync()
        {
            await ReplyAsync(_configuration["Vibes"]);
        }

        
        [Command("eHug")]
        [Summary("Sends a little pick me up message")]
        public async Task EHugAsync()
        {
            var affirm = await _eHugService.GetEHugAsync();
            await ReplyAsync(affirm.ToString());
        }
    }
}
