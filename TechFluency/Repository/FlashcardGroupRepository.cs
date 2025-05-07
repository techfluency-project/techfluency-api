using MongoDB.Driver;
using TechFluency.Context;
using TechFluency.Models;

namespace TechFluency.Repository
{
    public class FlashcardGroupRepository : TechFluencyRepository<FlashcardGroup>
    {
        public FlashcardGroupRepository(MongoDbContext context) : base(context, "FlashcardsGroup")
        {

        }

        public IEnumerable<FlashcardGroup> GetFlashcardGroup(string userId)
        {
            return _collection.AsQueryable().Where(x => x.UserId == userId).ToList();
        }
    }
}
