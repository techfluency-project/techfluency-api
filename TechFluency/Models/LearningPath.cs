using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TechFluency.Repository;

namespace TechFluency.Models
{
    public class LearningPath : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }  

        [BsonElement("description")]
        public string Description { get; set; } 

        [BsonElement("stages")]
        public List<string> Stages { get; set; } 

    }
}
