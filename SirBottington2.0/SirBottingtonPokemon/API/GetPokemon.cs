using Discord;
using PokeApiNet;
using SirBottingtonPokemon.Util;

namespace SirBottingtonPokemon.API
{
    public class GetPokemon : IGetPokemon
    {
        PokeApiClient _client;
        public GetPokemon(PokeApiClient client)
        {
            _client = client;
        }

        public async Task<Pokemon> GetPokemonId(int num)
        {
            Pokemon pokemon = await _client.GetResourceAsync<Pokemon>(num);
            return pokemon;

        }
        public async Task<Pokemon> GetPokemonName(string name)
        {
            try
            {
                Pokemon pokemon = await _client.GetResourceAsync<Pokemon>(name);
                return pokemon;

            }
            catch
            {
                return null;
            }
        }
        public async Task<Embed> GetPokemonFullId(int num)
        {
            var pokemon = await GetPokemonId(num);
            PokemonSpecies species = await _client.GetResourceAsync<PokemonSpecies>(num);
            string evos = await Evolutions.GetEvolutions(species, _client);
            return PokemonEmbedBuilder.GetEmbed(pokemon, species, evos);
        }
        public async Task<Embed> GetPokemonFullName(string name)
        {
            var pokemon = await GetPokemonName(name);
            if (pokemon is null) return null;

            PokemonSpecies species = await _client.GetResourceAsync<PokemonSpecies>(name);
            string evos = await Evolutions.GetEvolutions(species, _client);
            return PokemonEmbedBuilder.GetEmbed(pokemon, species, evos);
        }
    }
}
