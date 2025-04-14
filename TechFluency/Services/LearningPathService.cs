using TechFluency.Context;
using TechFluency.Enums;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class LearningPathService
    {
        private readonly UserProgresRepository _userProgressRepository;
        private readonly PathStageService _pathStageService;

        public LearningPathService(MongoDbContext context, UserProgresRepository userProgressRepository, PathStageService pathStageService) 
        {
            _userProgressRepository = userProgressRepository;
            _pathStageService = pathStageService;
        }

        public void MountingLearningPath(User user)
        {
            var userProgress = _userProgressRepository.GetUserProgress(user.Id);
            var learningPath = CreateLearningPath(user.Id, userProgress.Level);
            var stages = _pathStageService.GetStagesForLearningPath(learningPath.Level, learningPath.Id);
            userProgress.LearningPathId = learningPath.Id;

            learningPath.Stages.AddRange(stages);
        }

        private LearningPath CreateLearningPath(string userId, EnumLevel level)
        {
            var learningPath = new LearningPath
            {
                UserId = userId,
                Level = level,
                Name = GetPathName(level),
                Description = GetPathDescription(level)
            };

            return learningPath;
        }

        private string GetPathName(EnumLevel level)
        {
            switch (level)
            {
                case EnumLevel.Beginner:
                    return "Beginner Path";

                case EnumLevel.Intermediate:
                    return "Intermediate Path";

                case EnumLevel.Advanced:
                    return "Advanced Path";

                default:
                    return "Unkown Path";
            }
        }

        private string GetPathDescription(EnumLevel level)
        {
            switch (level)
            {
                case EnumLevel.Beginner:
                    return "This is the beginning of your journey, good luck!";

                case EnumLevel.Intermediate:
                    return "Awesome, you already are in the middle of the journey! Carry on!";

                case EnumLevel.Advanced:
                    return "Wow! You are almost there, let's do it!";

                default:
                    return "No description available";
            }
        }

    }
}
