﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TechFluency.Repository;

namespace TechFluency.Models
{
    public class FlashcardGroup : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userID")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("flashcards")]
        [BsonRequired]
        public List<string>? Flashcards { get; set; }

    }
}
