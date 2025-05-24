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

        public UserAnswerResultDTO AnswerQuestion(List<UserAnswerPathDTO> answers, string userId)
        {
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 4 };

            int totalQuestions = 0;
            int totalCorrect = 0;

            var userProgress = _userProgresRepository.GetUserProgress(userId);
            userProgress.Activities ??= new List<ActivityProgress>();
            userProgress.StageProgresses ??= new List<StageProgress>();

            object lockObj = new();

            var allAnswers = answers
                .SelectMany(stage => stage.Answers.Select(answer => new
                {
                    Answer = answer,
                    PathStageId = stage.PathStageId
                }))
                .ToList();

            Parallel.ForEach(allAnswers, options, entry =>
            {
                var question = GetQuestionById(entry.Answer.QuestionId);
                var pathStage = _pathStageRepository.GetStageById(entry.PathStageId);
                bool isCorrect = question.CorrectAnswer == entry.Answer.SelectedOption;

                lock (lockObj)
                {
                    totalQuestions++;
                    if (isCorrect) totalCorrect++;
                }

                lock (userProgress.Activities)
                {
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
                    if (isCorrect)
                    {
                        existingActivity.TotalCorrect++;
                        _badgeService.CheckBadgeAchievement(userProgress);
                    }
                }

                lock (userProgress.StageProgresses)
                {
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

                    if (!stageProgress.IsCompleted && stageProgress.TotalAnswered >= pathStage.Questions.Count())
                    {
                        double accuracy = (double)stageProgress.TotalCorrect / stageProgress.TotalAnswered;
                        if (accuracy >= 0.7)
                        {
                            userProgress.TotalXP += pathStage.XpReward;
                            pathStage.IsCompleted = true;
                            stageProgress.IsCompleted = true;
                            _pathStageRepository.Update(pathStage.Id, pathStage);
                        }
                        else
                        {
                            stageProgress.HasFailed = true;
                        }
                    }
                }

                _userProgresRepository.Update(userProgress.Id, userProgress);
            });

            return new UserAnswerResultDTO()
            {
                Percentage = totalQuestions == 0 ? 0 : (double)totalCorrect / totalQuestions
            };

        }

    }
}
