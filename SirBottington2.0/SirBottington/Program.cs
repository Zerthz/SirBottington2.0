using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace SirBottington
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = DI.CreateHostBuilder(args).Build();

            using (host)
            {
               await host.RunAsync();
            }

        }
    }
}