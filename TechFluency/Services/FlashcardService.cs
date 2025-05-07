using TechFluency.Repository;

namespace TechFluency.Services
{
    public class FlashcardService
    {
        private readonly FlashcardRepository _flashCardRepository;

        public FlashcardService(FlashcardRepository flashCardRepository)
        {
            _flashCardRepository = flashCardRepository;
        }
    }
}
