using TechFluency.Context;
using TechFluency.Enums;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class LearningPathService
    {
        private readonly LearningPathRepository _learningPathRepository;
        private readonly UserProgresRepository _userProgressRepository;
        private readonly PathStageService _pathStageService;
        private readonly JwtService _jwtService;

        public LearningPathService(MongoDbContext context, UserProgresRepository userProgressRepository, PathStageService pathStageService, LearningPathRepository learningPathRepository, JwtService jwtService) 
        {
            _userProgressRepository = userProgressRepository;
            _pathStageService = pathStageService;
            _learningPathRepository = learningPathRepository;
            _jwtService = jwtService;
        }

        public async Task MountingLearningPath(string userId)
        {
            try
            {
                var userProgress = _userProgressRepository.GetUserProgress(userId);
                var learningPath = CreateLearningPath(userId, userProgress.Level);
                var stages = _pathStageService.GetStagesForLearningPath(learningPath.Level, learningPath.Id).ToList();
                userProgress.LearningPathId = learningPath.Id;

                learningPath.Stages.AddRange(stages);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }

        public async Task<LearningPath> GetLearningPath(string userId)
        {
            var userProgress = _userProgressRepository.GetUserProgress(userId);
            return _learningPathRepository.GetLearningPath(userProgress.LearningPathId);
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

            _learningPathRepository.Add(learningPath);

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
