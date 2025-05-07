using TechFluency.DTOs;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class FlashcardGroupService
    {
        private readonly FlashcardGroupRepository _flashCardGroupRepository;

        public FlashcardGroupService(FlashcardGroupRepository flashCardGroupRepository)
        {
            _flashCardGroupRepository = flashCardGroupRepository;
        }

        public IEnumerable<FlashcardGroup> GetFlashcardsGroup(string userId)
        {
            return _flashCardGroupRepository.GetFlashcardGroup(userId);
        }

        public void CreateFlashcardGroup(string userId, FlashcardGroupName flashcardGroupName)
        {
            var flashCardGroup = new FlashcardGroup
            {
                UserId = userId,
                Flashcards = new List<string>(),
                Name = flashcardGroupName.Name,
            };
            
            _flashCardGroupRepository.Add(flashCardGroup);
        }
    }
}
