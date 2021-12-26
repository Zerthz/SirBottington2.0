using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SirBottington.Models
{
    public class XKCDSearchModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SearchTerm { get; set; }
        public XKCDModel Comic { get; set; }
    }
}
