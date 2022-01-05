using SirBottingtonPokemon.Models;

namespace SirBottingtonPokemon.GuessGame
{
    public interface IPokemonGame
    {
        Task<PokemonGameModel> Initialize();
    }
}