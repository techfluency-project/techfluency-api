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

        public List<LearningPath> GetLearningPath(string userId)
        {
            return _collection.AsQueryable().Where(x => x.UserId == userId).ToList();
        }
    }
}
