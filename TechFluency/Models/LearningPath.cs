using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechFluency.Models
{
    public class LearningPath
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }  

        [BsonElement("description")]
        public string Description { get; set; } 

        [BsonElement("stages")]
        public List<PathStage> Stages { get; set; } 

    }
}
