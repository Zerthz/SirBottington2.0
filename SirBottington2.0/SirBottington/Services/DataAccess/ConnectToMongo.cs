using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Services.DataAccess
{
    public class ConnectToMongo
    {
        private readonly IConfiguration _configuration;

        public ConnectToMongo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        internal IMongoCollection<T> Connect<T>(in string collection)
        {
            var client = new MongoClient(_configuration.GetConnectionString("MongoDB"));
            var db = client.GetDatabase("SirBottington");
            return db.GetCollection<T>(collection);
        }
    }
}
