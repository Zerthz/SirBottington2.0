using Discord.Addons.Interactive;
using PokeApiNet;
using SirBottingtonPokemon.API;
using SirBottingtonPokemon.Models;
using SirBottingtonPokemon.Util;

namespace SirBottingtonPokemon.GuessGame
{
    public class PokemonGame : InteractiveBase
    {
        Random r;
        private readonly CreateGameImages _createImages;
        private readonly GetPokemon _getPokemon;

        public PokemonGame(Random r, PokeApiClient client, CreateGameImages createImages, GetPokemon getPokemon)
        {
            this.r = r;
            _createImages = createImages;
            _getPokemon = getPokemon;
        }
        public async Task<PokemonGameModel> Initialize()
        {
            int randomPokemon = r.Next(1, 722);
            await _createImages.Create(randomPokemon);
            var pokemon = await _getPokemon.GetPokemonId(randomPokemon);
            return new PokemonGameModel { Name = pokemon.Name, PokedexId = pokemon.Id };
        }
     
    }
}
 