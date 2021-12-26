using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Addons.Interactive;
using PokeApiNet;
using SirBottingtonPokemon.Util;

namespace SirBottingtonPokemon.GuessGame
{
    public class PokemonGame : InteractiveBase
    {
        Random r;
        PokeApiClient client;
        private readonly CreateGameImages _createImages;

        public PokemonGame(Random r, PokeApiClient client, CreateGameImages createImages)
        {
            this.r = r;
            this.client = client;
            _createImages = createImages;
        }
        public async Task Initialize()
        {
            int randomPokemon = r.Next(1, 722);
            await _createImages.Create(randomPokemon);
        }
        public async Task GameLogic()
        {

            
        }
    }
}
 