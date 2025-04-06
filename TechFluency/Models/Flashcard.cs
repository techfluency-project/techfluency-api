using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using TechFluency.Enums;
using TechFluency.Repository;

namespace TechFluency.Models
{
    public class Flashcard : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("question")]
        [BsonRepresentation(BsonType.String)]
        public string QuestionText { get; set; }  

        [BsonElement("answer")]
        [BsonRepresentation(BsonType.String)]
        public string AnswerText { get; set; }  

        [BsonElement("difficulty")]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public EnumDifficulty Difficulty { get; set; }

        [BsonElement("easeFactor")]
        [BsonRepresentation(BsonType.Double)]
        public double EaseFactor { get; set; } = 2.5;

        [BsonElement("nextReviewDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime NextReviewDate { get; set; } = DateTime.UtcNow.AddDays(1);

        [BsonElement("lastReviewed")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime LastReviewed { get; set; } = DateTime.UtcNow;

        [BsonElement("repetitionInterval")]
        [BsonRepresentation(BsonType.Int32)]
        public int RepetitionInterval { get; set; } = 1;

        [BsonElement("flashcardGroupId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FlashcardGroupId { get; set; }
    }
}
