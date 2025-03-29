﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TechFluency.Enums;

namespace TechFluency.Models
{
    public class UserProgress
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userID")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("level")]
        [BsonRepresentation(BsonType.String)]
        public EnumLevel Level { get; set; }
            
        [BsonElement("dailyStudyTime")]
        [BsonRepresentation(BsonType.String)]
        public EnumDailyStudyTime DailyStudyTime { get; set; }

        [BsonElement("xp")]
        [BsonRepresentation(BsonType.String)]
        public int TotalXP { get; set; }

        [BsonElement("activities")]
        [BsonIgnoreIfNull]
        public List<ActivityProgress>? Activities { get; set; }

        [BsonElement("badges")]
        [BsonIgnoreIfNull]
        public List<Badge> Badges { get; set; }
    }
}
