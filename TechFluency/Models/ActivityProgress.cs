using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TechFluency.Enums;

namespace TechFluency.Models
{
    public class ActivityProgress
    {
        [BsonElement("questionTopic")]
        [BsonRepresentation(BsonType.String)]
        public EnumTopic Topic { get; set; }

        [BsonElement("questionType")]
        [BsonRepresentation(BsonType.String)]
        public EnumTypeQuestion Type { get; set; }

        [BsonElement("totalCompleted")]
        [BsonRepresentation(BsonType.Int32)]
        public int TotalCompleted { get; set; }  

        [BsonElement("totalCorrect")]
        [BsonRepresentation(BsonType.Int32)]
        public int TotalCorrect { get; set; }  
    }
}
