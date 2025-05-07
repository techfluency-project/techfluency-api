using TechFluency.Context;
using TechFluency.Models;

namespace TechFluency.Repository
{
    public class FlashcardRepository : TechFluencyRepository<Flashcard>
    {
        public FlashcardRepository(MongoDbContext context) : base(context, "Flashcards")
        {

        }
    }
}
