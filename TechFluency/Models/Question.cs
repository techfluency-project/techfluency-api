using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TechFluency.Enums;
using TechFluency.Repository;

namespace TechFluency.Models
{
    public class Question : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("question")]
        [BsonRepresentation(BsonType.String)]
        public string? QuestionText { get; set; }

        [BsonElement("questionConversation")]
        [BsonIgnoreIfNull]
        public List<ConversationLine> QuestionTextConversation { get; set; }

        [BsonElement("level")]
        [BsonRepresentation(BsonType.String)]
        public EnumLevel Level { get; set; }

        [BsonElement("type")]
        [BsonRepresentation(BsonType.String)]
        public EnumTypeQuestion Type { get; set; }

        [BsonElement("topic")]
        [BsonRepresentation(BsonType.String)]
        public EnumTopic Topic { get; set; }

        [BsonElement("options")]
        [BsonIgnoreIfNull]
        public List<string>? Options { get; set; }

        [BsonElement("correctanswer")]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string? CorrectAnswer { get; set; }

        [BsonElement("pairs")]
        [BsonIgnoreIfNull]
        public Dictionary<string, string>? Pairs { get; set; }

        [BsonElement("correctanswerforpairs")]
        [BsonIgnoreIfNull]
        public Dictionary<string, string>? CorrectAnswerForPairs { get; set; }

        [BsonElement("shuffledwords")]
        [BsonIgnoreIfNull]
        public List<string>? ShuffledWords { get; set; }

        [BsonElement("correctanswerforshuffled")]
        [BsonIgnoreIfNull]
        public List<string>? CorrectAnswerForShuffled { get; set; }  

        [BsonElement("audio")]
        [BsonIgnoreIfNull]
        public byte[]? Audio { get; set; }
    }
}

