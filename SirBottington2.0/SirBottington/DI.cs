using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokeApiNet;
using SirBottington.Core.Interfaces;
using SirBottington.Core.Services;
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
#if DEBUG
                config.Token = context.Configuration["Debug_Token"];
#else
                config.Token = context.Configuration["Prod_Token"];
#endif

            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<DiscordSocketClient>();
                services.AddSingleton<IXKCDDataAccess, XKCDDataAccess>();
                services.AddSingleton<IUserDataAccess, UserDataAccess>();
                services.AddSingleton<IPokemonDataAccess, PokemonDataAccess>();
                services.AddSingleton<IGetPokemon, GetPokemon>();
                services.AddSingleton<ConnectToMongo>();
                services.AddSingleton<Random>();
                services.AddSingleton<PokeApiClient>();

                services.AddTransient<IEHugService, EHugService>();
                services.AddTransient<Program>();
                services.AddTransient<EmbedBuilder>();
                services.AddTransient<IXKCDModel, XKCDModel>();
                services.AddTransient<IXKCDUtil, XKCDUtil>();
                services.AddTransient<IGetXKCDAPI, GetXKCDAPI>();
                services.AddTransient<CreateGameImages>();
                services.AddTransient<IPokemonModel, PokemonModel>();
                
                
                services.AddScoped<IPokemonGame, PokemonGame>();

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
