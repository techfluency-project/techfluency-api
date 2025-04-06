﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using TechFluency.Repository;

namespace TechFluency.Models
{
    public class PathStage : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } 

        [BsonElement("questions")]
        public List<string> Questions { get; set; } 

        [BsonElement("xpReward")]
        public int XpReward { get; set; } 

        [BsonElement("isCompleted")]
        public bool IsCompleted { get; set; } 
    }
}
