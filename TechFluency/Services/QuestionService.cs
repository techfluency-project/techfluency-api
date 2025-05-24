using Microsoft.IdentityModel.Tokens;
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
        private readonly BadgeService _badgeService;
        private readonly PathStageRepository _pathStageRepository;

        public QuestionService(QuestionRepository questionRepository, UserProgresRepository userProgresRepository, BadgeService badgeService, PathStageRepository pathStageRepository)
        {
            _questionRepository = questionRepository;
            _userProgresRepository = userProgresRepository;
            _badgeService = badgeService;
            _pathStageRepository = pathStageRepository;
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
                                    .Take(8)
                                    .Select(x => x.Id)
                                    .ToList();

            return randomQuestions;
        }

        public UserAnswerResultDTO AnswerQuestion(UserAnswerPathDTO answer, string userId)
        {
            var question = GetQuestionById(answer.QuestionId);
            var userProgress = _userProgresRepository.GetUserProgress(userId);
            var pathStage = _pathStageRepository.GetStageById(answer.PathStageId);

            userProgress.Activities ??= new List<ActivityProgress>();
            userProgress.StageProgresses ??= new List<StageProgress>();

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
                _badgeService.CheckBadgeAchievement(userProgress);
            }

            var stageProgress = userProgress.StageProgresses
                .FirstOrDefault(s => s.StageId == pathStage.Id);

            if (stageProgress == null)
            {
                stageProgress = new StageProgress
                {
                    StageId = pathStage.Id,
                    TotalAnswered = 0,
                    TotalCorrect = 0
                };
                userProgress.StageProgresses.Add(stageProgress);
            }

            if (!stageProgress.IsCompleted && stageProgress.HasFailed)
            {
                stageProgress.TotalAnswered = 0;
                stageProgress.TotalCorrect = 0;
                stageProgress.HasFailed = false;
            }

            stageProgress.TotalAnswered++;
            if (isCorrect)
                stageProgress.TotalCorrect++;

            _userProgresRepository.Update(userProgress.Id, userProgress);

            bool stageCompleted = stageProgress.IsCompleted;
            bool stageFailed = false;


            if (!stageCompleted && stageProgress.TotalAnswered >= pathStage.Questions.Count())
            {
                double accuracy = (double)stageProgress.TotalCorrect / stageProgress.TotalAnswered;
                if (accuracy >= 0.7)
                {
                    userProgress.TotalXP += pathStage.XpReward;
                    pathStage.IsCompleted = true;
                    stageCompleted = pathStage.IsCompleted;
                    stageProgress.IsCompleted = true;
                    _pathStageRepository.Update(pathStage.Id, pathStage);
                }
                else
                {
                    stageFailed = true;
                    stageProgress.HasFailed = true;
                }
            }
           
            _userProgresRepository.Update(userProgress.Id, userProgress);

            return new UserAnswerResultDTO
            {
                IsCorrect = isCorrect,
                QuestionID = question.Id,
                StageCompleted = stageCompleted,
                StageAnswered = stageProgress.TotalAnswered,
                StageCorrect = stageProgress.TotalCorrect,
                StageFailed = stageFailed
            };

        }


    }
}
