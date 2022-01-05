using Discord;
using PokeApiNet;

namespace SirBottingtonPokemon.API
{
    public interface IGetPokemon
    {
        Task<Embed> GetPokemonFullId(int num);
        Task<Embed> GetPokemonFullName(string name);
        Task<Pokemon> GetPokemonId(int num);
        Task<Pokemon> GetPokemonName(string name);
    }
}