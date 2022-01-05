using SirBottington.Models;

namespace SirBottington.Services.DataAccess
{
    public interface IPokemonDataAccess
    {
        Task<PokemonModel> GetCurrentPokemonGame();
        Task InsertPokemonGame(PokemonModel pokemon);
        Task InsertPreviousPokemonGame(PokemonModel pokemon);
        Task RemovePokemonGame(PokemonModel pokemon);
        Task RemovePreviousGame();
        Task UpdatePreviousPokemonGame(PokemonModel pokemon);
    }
}