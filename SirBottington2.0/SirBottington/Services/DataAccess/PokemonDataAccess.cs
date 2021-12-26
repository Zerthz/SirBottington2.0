using MongoDB.Driver;
using SirBottington.Models;

namespace SirBottington.Services.DataAccess
{
    public class PokemonDataAccess
    {
        private readonly ConnectToMongo _connection;
        private const string CurrentGameCollection = "CurrentGuessingGame";
        private const string PreviousGameCollection = "PreviousGuessingGame";

        public PokemonDataAccess(ConnectToMongo connection)
        {
            _connection = connection;
        }

        public async Task<PokemonModel> GetCurrentPokemonGame()
        {
            var pokemonCollection = _connection.Connect<PokemonModel>(CurrentGameCollection);
            var results = await pokemonCollection.FindAsync(_ => true);
            return results.FirstOrDefault();
        }

        public Task InsertPokemonGame(PokemonModel pokemon)
        {
            var pokemonCollection = _connection.Connect<PokemonModel>(CurrentGameCollection);
            return pokemonCollection.InsertOneAsync(pokemon);
        }
        public Task InsertPreviousPokemonGame(PokemonModel pokemon)
        {
            var pokemonCollection = _connection.Connect<PokemonModel>(PreviousGameCollection);
            return pokemonCollection.InsertOneAsync(pokemon);
        }
        public Task RemovePokemonGame(PokemonModel pokemon)
        {
            var pokemonCollection = _connection.Connect<PokemonModel>(CurrentGameCollection);
            return pokemonCollection.DeleteOneAsync(p => p.Id == pokemon.Id);
        }
        public Task UpdatePreviousPokemonGame(PokemonModel pokemon)
        {
            var pokemonCollection = _connection.Connect<PokemonModel>(PreviousGameCollection);
            var filter = Builders<PokemonModel>.Filter.Empty;
            return pokemonCollection.ReplaceOneAsync(filter, pokemon);
        }
        public Task RemovePreviousGame()
        {
            var pokemonCollection = _connection.Connect<PokemonModel>(PreviousGameCollection);
            return pokemonCollection.DeleteOneAsync(_ => true);
        }
    }
}
