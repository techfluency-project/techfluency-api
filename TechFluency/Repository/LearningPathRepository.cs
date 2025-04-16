using TechFluency.Context;
using TechFluency.Models;
using TechFluency.Enums;
using MongoDB.Driver;

namespace TechFluency.Repository
{
    public class LearningPathRepository : TechFluencyRepository<LearningPath>
    {
       
        public LearningPathRepository(MongoDbContext context) : base(context, "LearningPaths")
        {
            
        }

        public LearningPath GetLearningPath(string id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }
    }
}
