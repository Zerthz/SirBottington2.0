using Discord.Commands;
using Microsoft.Extensions.Configuration;
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

        public Commands(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Command("vibes")]
        [Summary("")]
        public async Task VibesAsync()
        {
            await ReplyAsync(_configuration["Vibes"]);
        }

        
    }
}
