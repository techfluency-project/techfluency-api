using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechFluency.Models
{
    public class StageProgress
    {
        [BsonElement("stageId")]
        public string StageId { get; set; }

        [BsonElement("totalAnswered")]
        public int TotalAnswered { get; set; }

        [BsonElement("totalCorrect")]
        public int TotalCorrect { get; set; }

        [BsonElement("hasFailed")]
        public bool HasFailed { get; set; }

        [BsonElement("isCompleted")]
        public bool IsCompleted { get; set; }

    }

}
