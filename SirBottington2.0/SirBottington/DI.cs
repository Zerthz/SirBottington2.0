using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Discord.Addons.Hosting;
using Discord;
using Discord.Commands;
using SirBottington.Services;
using SirBottington.Modules;
using SirBottington.Models;
using SirBottington.Utilities;
using SirBottington.Services.API;

namespace SirBottington
{
    public static class DI
    {
        internal static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(app =>
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

                app.AddConfiguration(configuration);
            })
            .ConfigureLogging(logging =>
            {
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Debug);
                
            })
            .ConfigureDiscordHost((context, config) =>
            {
                config.SocketConfig = new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Debug,
                    AlwaysDownloadUsers = true,
                    MessageCacheSize = 200,
                };
                config.Token = context.Configuration["Debug_Token"];
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<DiscordSocketClient>();
                services.AddTransient<Program>();
                services.AddTransient<XKCDModel>();
                services.AddTransient<XKCDUtil>();
                services.AddTransient<GetXKCDAPI>();
                services.AddTransient<EmbedBuilder>();
                services.AddSingleton<Random>();
            })
            .UseCommandService((context, config) =>
            {
                config.CaseSensitiveCommands = false;
                config.LogLevel = LogSeverity.Debug;
                config.DefaultRunMode = RunMode.Async;
            })
            .ConfigureServices((context, services) =>
            {
                services
                    .AddHostedService<CommandHandler>()
                    .AddHttpClient();

            })
            .UseConsoleLifetime();


            
        }
    }
}
