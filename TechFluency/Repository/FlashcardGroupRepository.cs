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

        public IEnumerable<FlashcardGroup> GetAllFlashcardsGroup(string userId)
        {
            return _collection.AsQueryable().Where(x => x.UserId == userId).ToList();
        }

        public FlashcardGroup GetFlashcardGroup(string flashCardGroupId)
        {
            return _collection.Find(x => x.Id == flashCardGroupId).FirstOrDefault(); 
        }
    }
}
