using Discord.Commands;
using Discord.WebSocket;
using Discord.Addons.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection;
using SirBottington.Modules;

namespace SirBottington.Services
{
    public class CommandHandler : DiscordClientService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _configuration;

        public CommandHandler(DiscordSocketClient client, ILogger<CommandHandler> logger,
            IServiceProvider provider, CommandService service, IConfiguration configuration) : base(client, logger)
        {
            _client = client;
            _service = service;
            _provider = provider;
            _configuration = configuration;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client.Ready += HandleReady;
            _client.MessageReceived += HandleCommand;

            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task HandleCommand(SocketMessage socketMessage)
        {
            if (socketMessage is not SocketUserMessage message) return;

            

            var context = new SocketCommandContext(_client, message);

            if (message.Content.Contains("grond"))
            {
                await context.Channel.SendMessageAsync("GROND!");
                await context.Channel.SendMessageAsync("https://tenor.com/bFe3n.gif");
            }

            var argPos = 0;
            if (!message.HasStringPrefix(_configuration["Prefix"], ref argPos)) return;
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        private async Task HandleReady()
        {
            await _client.SetGameAsync("+help for help commands");
        }
    }
}
