using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using PokeApiNet;
using SirBottingtonPokemon;
using SirBottingtonPokemon.Util;

namespace SirBottingtonPokemon.API
{
    public class GetPokemon
    {
        PokeApiClient _client;
        public GetPokemon()
        {
            _client = new PokeApiClient();
        }

        public async Task<Embed> GetPokemonId(int num)
        {
            Pokemon pokemon = await _client.GetResourceAsync<Pokemon>(num);
            PokemonSpecies species = await _client.GetResourceAsync<PokemonSpecies>(num);
            string evos = await Evolutions.GetEvolutions(species, _client);
            return PokemonEmbedBuilder.GetEmbed(pokemon, species, evos);
        }
        public async Task<Embed> GetPokemonName(string name)
        {
            try
            {
                Pokemon pokemon = await _client.GetResourceAsync<Pokemon>(name);
                PokemonSpecies species = await _client.GetResourceAsync<PokemonSpecies>(name);
                string evos = await Evolutions.GetEvolutions(species, _client);
                return PokemonEmbedBuilder.GetEmbed(pokemon, species, evos);
            }catch 
            {
                return null;
            }
        }
    }
}
