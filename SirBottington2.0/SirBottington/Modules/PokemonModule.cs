﻿using Discord.Commands;
using SirBottingtonPokemon.API;
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

        public PokemonModule(GetPokemon poke)
        {
            _poke = poke;
        }
        [Command("pokemon")]
        public async Task PokemonAsync(string arg = null, string arg2 = null)
        {

            if (int.TryParse(arg, out var id))
            {
                if (id > 0 && id <= 898)
                {
                    await ReplyAsync(embed: await _poke.GetPokemonId(id));
                }
                else
                {
                    await ReplyAsync("The database contains information for Pokémon between PokeID 1 and 898");
                }
            }else if (arg is not null)
            {
               var pokemonEmbed = await _poke.GetPokemonName(arg);
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
