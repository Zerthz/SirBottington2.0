using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using SirBottington.Models;

namespace SirBottington.Services.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly ConnectToMongo _connection;
        private const string UserCollection = "users";

        public UserDataAccess(ConnectToMongo connection)
        {
            _connection = connection;
        }

        public async Task<UserModel> GetUser(string dV)
        {
            var userCollection = _connection.Connect<UserModel>(UserCollection);
            var results = await userCollection.FindAsync(u => u.Discriminator == dV);
            return results.FirstOrDefault();
        }
        public async Task<List<UserModel>> GetAll()
        {
            var userCollection = _connection.Connect<UserModel>(UserCollection);
            var results = await userCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public Task Update(UserModel user)
        {
            var userCollection = _connection.Connect<UserModel>(UserCollection);
            var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
            return userCollection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
        }
        public Task Insert(UserModel user)
        {
            var userCollection = _connection.Connect<UserModel>(UserCollection);
            return userCollection.InsertOneAsync(user);
        }
    }
}
