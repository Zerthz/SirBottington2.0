using Discord.Commands;
using Microsoft.Extensions.Configuration;
using SirBottington.Models;
using SirBottington.Services.DataAccess;
using SirBottingtonPokemon.API;
using SirBottingtonPokemon.GuessGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Modules
{
    public class PokemonModule : ModuleBase<SocketCommandContext>
    {
        private readonly GetPokemon _poke;
        private readonly PokemonGame _game;
        private readonly PokemonDataAccess _pkmnDA;
        private readonly UserDataAccess _userDA;
        private readonly IConfiguration _configuration;

        public PokemonModule(GetPokemon poke, PokemonGame game, PokemonDataAccess db, IConfiguration configuration, UserDataAccess userDataAccess)
        {
            _poke = poke;
            _game = game;
            _pkmnDA = db;
            _userDA = userDataAccess;
            _configuration = configuration;
        }
        [Command("guess")]
        public async Task GuessAsync(string arg)
        {
            var currentGame = await _pkmnDA.GetCurrentPokemonGame();
            if(currentGame is null)
            {
                await ReplyAsync("There's no game running, I'll set one up for you");
                await PokemonAsync("play");
            }
            else
            {
                if(string.Equals(arg, currentGame.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var answer = Path.Combine(_configuration.GetSection("PokemonPaths")["PROD_BW"], currentGame.PokedexId + "answer.png");
                    await Context.Channel.SendFileAsync(answer, "Correct! It was " + currentGame.Name);
                    await _pkmnDA.RemovePokemonGame(currentGame);
                    await _pkmnDA.RemovePreviousGame();
                    await _pkmnDA.InsertPreviousPokemonGame(currentGame);

                    var user = await _userDA.GetUser(Context.User.Discriminator);
                    if(user is null)
                    {
                        user = new UserModel { Username = Context.User.Username ,Discriminator = Context.User.Discriminator, PokemonScore = 1 };
                        await _userDA.InsertUser(user);
                    }
                    else
                    {
                        user.PokemonScore += 1;
                        user.Username = Context.User.Username;
                        await _userDA.UpdateUser(user);
                    }

                }
            }
        }


        [Command("pokemon")]
        public async Task PokemonAsync(string arg = null, string arg2 = null)
        {

            if (int.TryParse(arg, out var id))
            {
                if (id > 0 && id <= 898)
                {
                    await ReplyAsync(embed: await _poke.GetPokemonFullId(id));
                }
                else
                {
                    await ReplyAsync("The database contains information for Pokémon between PokeID 1 and 898");
                }
            }
            else if (String.Equals(arg, "play", StringComparison.OrdinalIgnoreCase))
            {
                var pokemon = await _pkmnDA.GetCurrentPokemonGame();
                if(pokemon is null)
                {
                    var pokemonModel =  await _game.Initialize();
                    pokemon = new PokemonModel { Name = pokemonModel.Name, PokedexId = pokemonModel.PokedexId };

                }
                var fileLink = Path.Combine(_configuration.GetSection("PokemonPaths")["PROD_BW"], pokemon.PokedexId + "black.png");
                await Context.Channel.SendFileAsync(fileLink, "WHO'S THAT POKEMON?!");
                await _pkmnDA.InsertPokemonGame(pokemon);
            }
            else if(String.Equals(arg, "score", StringComparison.OrdinalIgnoreCase))
            {
                var users = await _userDA.GetAllUsers();
                string output = "```Pokemon Score\n";
                foreach (var user in users)
                {
                    output += $"{user.Username}: {user.PokemonScore} points.\n";
                }
                await ReplyAsync(output +"```");
            }
            else if (arg is not null)
            {
               var pokemonEmbed = await _poke.GetPokemonFullName(arg);
                if (pokemonEmbed is not null)
                {
                    await ReplyAsync(embed: pokemonEmbed);
                }
                else
                {
                    await ReplyAsync("Check the spelling on that, I couldn't find a match");
                }
            }

        }
    }
}
