using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechFluency.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        [BsonRepresentation(BsonType.String)]
        [BsonRequired]
        public string Username { get; set; }

        [BsonElement("email")]
        [BsonRepresentation(BsonType.String)]
        [BsonRequired]
        public string Email { get; set; }

        [BsonElement("password")]
        [BsonRepresentation(BsonType.String)]
        [BsonRequired]
        public string Password { get; set; }

        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string? Name { get; set; }

        [BsonElement("lastname")]
        [BsonRepresentation(BsonType.String)]
        public string? LastName { get; set; }

        [BsonElement("phone")]
        [BsonRepresentation(BsonType.String)]
        public string? Phone { get; set; }
        
        [BsonElement("gender")]
        [BsonRepresentation(BsonType.String)]
        public string? Gender { get; set; }

        [BsonElement("birthdate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateOnly? Birthdate { get; set; }
    }
}
