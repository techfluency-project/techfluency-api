using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TechFluency.Models
{
    public class Badge
    {
        [BsonElement("title")]
        [BsonRepresentation(BsonType.String)]
        public string Title { get; set; }

        [BsonElement("description")]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("icon")]
        [BsonRepresentation(BsonType.String)]
        public string Icon { get; set; }
    }
}
