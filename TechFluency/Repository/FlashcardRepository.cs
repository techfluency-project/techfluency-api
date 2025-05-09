using MongoDB.Driver;
using TechFluency.Context;
using TechFluency.Models;

namespace TechFluency.Repository
{
    public class FlashcardRepository : TechFluencyRepository<Flashcard>
    {
        public FlashcardRepository(MongoDbContext context) : base(context, "Flashcards")
        {

        }

        public Flashcard GetFlashcardById(string flashcardId)
        {
            return _collection.Find(x => x.Id == flashcardId).FirstOrDefault();
        }

        public IEnumerable<Flashcard> GetAllFlashcardsAvailableToReviewByGroupId(string groupId)
        {
            return _collection.AsQueryable().Where(x => x.FlashcardGroupId == groupId); 
        }
    }
}
