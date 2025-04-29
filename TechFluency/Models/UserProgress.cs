using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TechFluency.Enums;
using TechFluency.Repository;

namespace TechFluency.Models
{
    public class UserProgress : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userID")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("level")]
        public EnumLevel Level { get; set; }
            
        [BsonElement("dailyStudyTime")]
        [BsonRepresentation(BsonType.String)]
        public EnumDailyStudyTime DailyStudyTime { get; set; }

        [BsonElement("xp")]
        public int TotalXP { get; set; } = 0;

        [BsonElement("activities")]
        [BsonIgnoreIfNull]
        public List<ActivityProgress>? Activities { get; set; }

        [BsonElement("badges")]
        [BsonIgnoreIfNull]
        public List<string> Badges { get; set; }

        [BsonElement("learningPathId")]
        public string LearningPathId { get; set; }

        [BsonElement("dtCreated")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? DtCreated { get; set; } = DateTime.UtcNow;
    }
}
