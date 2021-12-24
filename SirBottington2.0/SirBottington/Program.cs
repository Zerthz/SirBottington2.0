using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace SirBottington
{
    public class Program
    {
        private DiscordSocketClient _client;

        public static async Task Main(string[] args)
        {
            var host = DI.CreateHostBuilder(args).Build();

            using (host)
            {
               await host.RunAsync();
            }

        }

        
        public Program(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task RegisterCommands()
        {
            _client.Ready += HandleReady;
        }
        private async Task HandleReady()
        {
            await _client.SetGameAsync("+help for help commands");
        }

        

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}