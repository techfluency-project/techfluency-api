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

        [BsonElement("correctCount")]
        [BsonRepresentation(BsonType.Int32)]
        public int CorrectCount { get; set; }  

        [BsonElement("totalCount")]
        [BsonRepresentation(BsonType.Int32)]
        public int TotalCount { get; set; }  
    }
}
