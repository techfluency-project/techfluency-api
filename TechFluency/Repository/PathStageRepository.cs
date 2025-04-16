using MongoDB.Driver;
using TechFluency.Context;
using TechFluency.Enums;
using TechFluency.Models;

namespace TechFluency.Repository
{
    public class PathStageRepository : TechFluencyRepository<PathStage>
    {
        public PathStageRepository(MongoDbContext context) : base(context, "PathStages")
        {
        }

        public IEnumerable<string> GetStagesForLearningPath(string learningPathId)
        {
            return _collection.Find(x => x.LearningPathId == learningPathId).ToList().Select(x => x.Id);
        }

    }
}
