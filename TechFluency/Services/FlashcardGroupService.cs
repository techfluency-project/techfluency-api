﻿using System.Runtime.InteropServices;
using TechFluency.DTOs;
using TechFluency.Helpers;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class FlashcardGroupService
    {
        private readonly FlashcardGroupRepository _flashCardGroupRepository;
        private readonly FlashcardRepository _flashCardRepository;

        public FlashcardGroupService(FlashcardGroupRepository flashCardGroupRepository, FlashcardRepository flashCardRepository)
        {
            _flashCardGroupRepository = flashCardGroupRepository;
            _flashCardRepository = flashCardRepository;
        }

        public IEnumerable<FlashcardGroup> GetAllFlashcardsGroup(string userId)
        {
            try
            {
                return _flashCardGroupRepository.GetAllFlashcardsGroup(userId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public FlashcardGroup GetFlashcardGroupById(string id)
        {
            var flashCardGroup = _flashCardGroupRepository.Get(id);
            if (flashCardGroup == null)
            {
                throw new ArgumentException("FlashcardGroup do not exists.");
            }
            return flashCardGroup;
        }

        public FlashcardGroup CreateFlashcardGroup(string userId, string flashcardGroupName)
        {
            try
            {
                var flashCardGroup = new FlashcardGroup
                {
                    UserId = userId,
                    Flashcards = new List<FlashcardToGetDTO>(),
                    Name = flashcardGroupName,
                };
            
                _flashCardGroupRepository.Add(flashCardGroup);
                return flashCardGroup;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Flashcard AddCardToFlashcardGroup(FlashcardToCreateDTO flashAdd)
        {
            try
            {
                var flashCardGroup = _flashCardGroupRepository.GetFlashcardGroup(flashAdd.FlashcardGroupId);

                var nextReviewDateUtc = DateTimeHelper.ToUtcTime(DateTimeHelper.StartOfDayBrasilia(DateTime.UtcNow));

                var flashcard = new Flashcard
                {
                    FlashcardGroupId = flashAdd.FlashcardGroupId,
                    QuestionText = flashAdd.FrontQuestion,
                    AnswerText = flashAdd.BackAnswer,
                    NextReviewDate = nextReviewDateUtc, 
                    LastReviewed = nextReviewDateUtc,
                };

                _flashCardRepository.Add(flashcard);

                var flashcardDTO = new FlashcardToGetDTO
                {
                    FlashcardID = flashcard.Id,
                    FrontQuestion = flashcard.QuestionText,
                    BackAnswer = flashcard.AnswerText
                };

                flashCardGroup?.Flashcards.Add(flashcardDTO);
                _flashCardGroupRepository.Update(flashCardGroup.Id, flashCardGroup);

                return flashcard;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteFlashCardGroup(string id)
        {
            try
            {
                var options = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
                var flashcardGroupToDelete = GetFlashcardGroupById(id);

                if (flashcardGroupToDelete != null)
                {
                    var allFlashcardsByGroup = _flashCardRepository.GetAllFlashcardsByGroup(flashcardGroupToDelete.Id);
                    Parallel.ForEach(allFlashcardsByGroup, options, flashcard =>
                    {
                        _flashCardRepository.Delete(flashcard.Id);
                    });
                    _flashCardGroupRepository.Delete(id);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
