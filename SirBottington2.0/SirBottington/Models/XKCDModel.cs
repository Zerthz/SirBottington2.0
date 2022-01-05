﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SirBottington.Models
{
    public class XKCDModel : IXKCDModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string month { get; set; }
        public int num { get; set; }
        public string link { get; set; }
        public string year { get; set; }
        public string news { get; set; }
        public string safe_title { get; set; }
        public string transcript { get; set; }
        public string alt { get; set; }
        public string img { get; set; }
        public string title { get; set; }
        public string day { get; set; }
    }
}
