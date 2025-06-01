using System.Reflection.Emit;
using System.Runtime.InteropServices;
using TechFluency.DTOs;
using TechFluency.Enums;
using TechFluency.Helpers;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class FlashcardService
    {
        private readonly FlashcardRepository _flashCardRepository;
        private readonly FlashcardGroupRepository _flashCardGroupRepository;

        public FlashcardService(FlashcardRepository flashCardRepository, FlashcardGroupRepository flashCardGroupRepository)
        {
            _flashCardRepository = flashCardRepository;
            _flashCardGroupRepository = flashCardGroupRepository;
        }

        public Flashcard GetFlashcardById(string flashcardId)
        {
            var flashcard = _flashCardRepository.GetFlashcardById(flashcardId);

            if(flashcard == null)
            {
                throw new ArgumentException("Flashcard do not exists.");
            }
            return flashcard;
        }

        public IEnumerable<FlashcardToGetDTO> GetAllFlashcardsAvailableToReviewByGroupId(string groupId)
        {
            var allFlashcards = _flashCardRepository.GetAllFlashcardsAvailableToReviewByGroupId(groupId);

            if (!allFlashcards.Any())
                throw new ArgumentException("Flashcards do not exist.");

            var todayBrasiliaDate = DateTimeHelper.StartOfDayBrasilia(DateTime.UtcNow);

            var startOfDayUtc = DateTimeHelper.ToUtcTime(todayBrasiliaDate);
            var endOfDayUtc = DateTimeHelper.ToUtcTime(DateTimeHelper.EndOfDayBrasilia(DateTime.UtcNow));

            var flashcardsDTO = allFlashcards
                .Where(x => x.NextReviewDate >= startOfDayUtc && x.NextReviewDate < endOfDayUtc)
                .Select(x => new FlashcardToGetDTO
                {
                    FlashcardID = x.Id,
                    FrontQuestion = x.QuestionText,
                    BackAnswer = x.AnswerText
                })
                .ToList();

            return flashcardsDTO;   
        }

        public void UpdateFlashcardReview(Flashcard flashcard, EnumDifficulty level)
        {

            switch (level)
            {
                case EnumDifficulty.Easy:
                    flashcard.EaseFactor += 0.15;
                    flashcard.RepetitionInterval = (int)(flashcard.RepetitionInterval * flashcard.EaseFactor);
                    break;

                case EnumDifficulty.Medium:
                    flashcard.RepetitionInterval = (int)(flashcard.RepetitionInterval * 1.2);
                    break;

                case EnumDifficulty.Hard:
                    flashcard.EaseFactor = Math.Max(1.3, flashcard.EaseFactor - 0.2);
                    flashcard.RepetitionInterval = 1;
                    break;
            }

            if (flashcard.RepetitionInterval < 1)
                flashcard.RepetitionInterval = 1;

            var nextReviewDateBrasilia = DateTimeHelper.NextReviewDateBrasilia(DateTime.UtcNow, flashcard.RepetitionInterval);
            flashcard.NextReviewDate = DateTimeHelper.ToUtcTime(nextReviewDateBrasilia);
            flashcard.LastReviewed = DateTimeHelper.ToUtcTime(DateTimeHelper.StartOfDayBrasilia(DateTime.UtcNow));

            flashcard.Difficulty = level;

            _flashCardRepository.Update(flashcard.Id, flashcard);
        }

        public void DeleteFlashCard(string id)
        {
            try
            {
                var flashcardToDelete = GetFlashcardById(id);

                if (flashcardToDelete != null)
                {
                    var flashCardGroup = _flashCardGroupRepository.GetFlashcardGroup(flashcardToDelete.FlashcardGroupId);
                    var groupWithoutFlashcard = flashCardGroup.Flashcards.Where(x => x.FlashcardID != flashcardToDelete.Id).ToList();
                    flashCardGroup.Flashcards = groupWithoutFlashcard;
                    _flashCardRepository.Delete(id);
                    _flashCardGroupRepository.Update(flashCardGroup.Id, flashCardGroup);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
