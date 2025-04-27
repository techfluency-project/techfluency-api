using Microsoft.AspNetCore.Http.HttpResults;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TechFluency.DTOs;
using TechFluency.Enums;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class QuestionService
    {
        private readonly QuestionRepository _questionRepository;
        private readonly UserProgresRepository _userProgresRepository;

        public QuestionService(QuestionRepository questionRepository, UserProgresRepository userProgresRepository)
        {
            _questionRepository = questionRepository;
            _userProgresRepository = userProgresRepository;
        }

        public IEnumerable<Question> GetAll()
        {
            return _questionRepository.GetAll();
        }

        public Question GetQuestionById(string id)
        {
            return _questionRepository.GetQuestionById(id);
        }

        public List<string> GetQuestionsForStage(EnumLevel level, EnumTopic topic)
        {
            var random = new Random();
            var questions = _questionRepository.GetQuestionsByLevel(level);
            var questionsByTopic = questions.Where(x => x.Topic == topic);
            var randomQuestions = questionsByTopic
                                    .OrderBy(x => random.Next())
                                    .Take(5)
                                    .Select(x => x.Id)
                                    .ToList();

            return randomQuestions;
        }

        public UserAnswerResultDTO AnswerQuestion(UserAnswerDTO answer, string userId)
        {
            var question = GetQuestionById(answer.QuestionId);
            var userProgress = _userProgresRepository.GetUserProgress(userId);

            if (userProgress.Activities == null)
            {
                userProgress.Activities = new List<ActivityProgress>();
            }

            var existingActivity = userProgress.Activities
                .FirstOrDefault(x => x.Topic == question.Topic && x.Type == question.Type);

            if (existingActivity == null)
            {
                existingActivity = new ActivityProgress
                {
                    Topic = question.Topic,
                    Type = question.Type,
                    TotalCompleted = 0,  
                    TotalCorrect = 0     
                };

                userProgress.Activities.Add(existingActivity);
            }

            existingActivity.TotalCompleted++;

            bool isCorrect = question.CorrectAnswer == answer.SelectedOption;
            if (isCorrect)
            {
                existingActivity.TotalCorrect++; 
            }

            _userProgresRepository.Update(userProgress.Id, userProgress);

            var response = new UserAnswerResultDTO
            {
                IsCorrect = isCorrect,
                QuestionID = question.Id
            };

            return response;
        }

    }
}
