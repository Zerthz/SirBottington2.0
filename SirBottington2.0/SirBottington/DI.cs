using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokeApiNet;
using SirBottington.Models;
using SirBottington.Services;
using SirBottington.Services.API;
using SirBottington.Services.DataAccess;
using SirBottington.Utilities;
using SirBottingtonPokemon.API;
using SirBottingtonPokemon.GuessGame;
using SirBottingtonPokemon.Util;

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
                logging.SetMinimumLevel(LogLevel.Information);
                
            })
            .ConfigureDiscordHost((context, config) =>
            {
                config.SocketConfig = new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Info,
                    AlwaysDownloadUsers = true,
                    MessageCacheSize = 200,
                };
                config.Token = context.Configuration["Prod_Token"];
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<DiscordSocketClient>();
                services.AddTransient<Program>();
                services.AddTransient<XKCDModel>();
                services.AddTransient<XKCDUtil>();
                services.AddSingleton<XKCDDataAccess>();
                services.AddTransient<GetXKCDAPI>();
                services.AddTransient<EmbedBuilder>();
                services.AddSingleton<Random>();
                services.AddSingleton<ConnectToMongo>();
                services.AddSingleton<GetPokemon>();
                services.AddSingleton<PokeApiClient>();
                services.AddTransient<CreateGameImages>();
                services.AddScoped<PokemonGame>();
                services.AddTransient<PokemonModel>();
                services.AddSingleton<PokemonDataAccess>();
                services.AddSingleton<UserDataAccess>();
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
