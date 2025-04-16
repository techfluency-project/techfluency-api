using TechFluency.Context;
using TechFluency.Enums;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class PathStageService
    {
        private readonly QuestionService _questionService;
        private readonly PathStageRepository _pathStageRepository;
        public PathStageService(QuestionService questionService, PathStageRepository pathStageRepository, MongoDbContext context) 
        {
            _questionService = questionService;
            _pathStageRepository = pathStageRepository;
        }

        public IEnumerable<string> GetStagesForLearningPath(EnumLevel learningPathLevel, string learningPathId)
        {
            var xpReward = 0;
            foreach (EnumTopic topic in Enum.GetValues(typeof(EnumTopic)))
            {
                var stage = new PathStage();
                var questions = _questionService.GetQuestionsForStage(learningPathLevel, topic);
                xpReward += 200;

                stage.Name = topic.ToString();
                stage.Topic = topic;
                stage.Questions = questions;
                stage.XpReward += xpReward;
                stage.IsCompleted = false;
                stage.LearningPathId = learningPathId;

                _pathStageRepository.Add(stage);
            }

            return _pathStageRepository.GetStagesForLearningPath(learningPathId);
        }
    }
}
