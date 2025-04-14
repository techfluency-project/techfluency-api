using TechFluency.Context;
using TechFluency.Models;
using TechFluency.Enums;

namespace TechFluency.Repository
{
    public class LearningPathRepository : TechFluencyRepository<LearningPath>
    {
       
        public LearningPathRepository(MongoDbContext context) : base(context, "LearningPaths")
        {
            
        }
    }
}
