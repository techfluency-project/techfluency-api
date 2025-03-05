using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechFluency.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("email")]
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonElement("password")]
        [BsonRepresentation(BsonType.String)]
        public string Password { get; set; }

        [BsonElement("birthdate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateOnly Birthdate { get; set; }
    }
}
