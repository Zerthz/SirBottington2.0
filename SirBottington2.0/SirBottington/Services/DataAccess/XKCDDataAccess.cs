using MongoDB.Driver;
using SirBottington.Models;

namespace SirBottington.Services.DataAccess
{
    public class XKCDDataAccess : IXKCDDataAccess
    {
        private readonly ConnectToMongo _connection;
        private const string ComicCollection = "XKCDCollection";
        private const string ComicSearchHistory = "XKCDHistory";
        public XKCDDataAccess(ConnectToMongo connection)
        {
            _connection = connection;
        }

        public async Task<List<XKCDModel>> GetAll()
        {
            var comicCollection = _connection.Connect<XKCDModel>(ComicCollection);
            var results = await comicCollection.FindAsync(_ => true);
            return results.ToList();
        }
        public Task Insert(XKCDModel comic)
        {
            var comicCollection = _connection.Connect<XKCDModel>(ComicCollection);
            return comicCollection.InsertOneAsync(comic);
        }

        public Task Update(XKCDModel comic)
        {
            var comicCollection = _connection.Connect<XKCDModel>(ComicCollection);
            var filter = Builders<XKCDModel>.Filter.Eq("Id", comic.Id);
            return comicCollection.ReplaceOneAsync(filter, comic, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<Dictionary<string, XKCDModel>> GetHistory()
        {
            var searchCollection = _connection.Connect<XKCDSearchModel>(ComicSearchHistory);
            var results = await searchCollection.FindAsync(_ => true);
            var output = new Dictionary<string, XKCDModel>();
            foreach (XKCDSearchModel item in results.ToList())
            {
                output.Add(item.SearchTerm, item.Comic as XKCDModel);
            }
            return output;
        }
        public Task InsertComicHistory(XKCDSearchModel comic)
        {
            var searchCollection = _connection.Connect<XKCDSearchModel>(ComicSearchHistory);
            return searchCollection.InsertOneAsync(comic);
        }
    }
}
