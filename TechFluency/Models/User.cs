﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TechFluency.Enums;
using TechFluency.Repository;

namespace TechFluency.Models
{
    public class User : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

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

        [BsonElement("phone")]
        [BsonRepresentation(BsonType.String)]
        public string? Phone { get; set; }

        [BsonElement("dtCreated")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? DtCreated { get; set; } = DateTime.UtcNow;
    }
}
