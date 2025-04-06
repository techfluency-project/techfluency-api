using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TechFluency.Repository;

namespace TechFluency.Models
{
    public class Badge : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        [BsonRepresentation(BsonType.String)]
        public string Title { get; set; }

        [BsonElement("description")]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("icon")]
        [BsonRepresentation(BsonType.String)]
        public string Icon { get; set; }

        [BsonElement("goal")]
        [BsonRepresentation(BsonType.Int32)]
        public int Goal { get; set; }
    }
}
