using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SirBottington.Models
{
    public class XKCDSearchModel : IXKCDSearchModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SearchTerm { get; set; }
        public IXKCDModel Comic { get; set; }
    }
}
