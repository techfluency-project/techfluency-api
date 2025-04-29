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

        public LearningPathService(MongoDbContext context, UserProgresRepository userProgressRepository, PathStageService pathStageService, LearningPathRepository learningPathRepository) 
        {
            _userProgressRepository = userProgressRepository;
            _pathStageService = pathStageService;
            _learningPathRepository = learningPathRepository;
        }

        public async Task MountingLearningPath(string userId)
        {
            try
            {
                var userProgress = _userProgressRepository.GetUserProgress(userId);
                var learningPath = CreateLearningPath(userId, userProgress.Level);
                var stages = _pathStageService.GetStagesForLearningPath(learningPath.Level, learningPath.Id).ToList();
                userProgress.LearningPathId = learningPath.Id;

                learningPath.Stages = stages;
                userProgress.LearningPathId = learningPath.Id;
                _learningPathRepository.Update(learningPath.Id, learningPath);
                _userProgressRepository.Update(userProgress.Id, userProgress);
            }
            catch (Exception ex)
            {
                throw;
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
                    return BeginnerDescriptions[random.Next(BeginnerDescriptions.Length)];

                case EnumLevel.Intermediate:
                    return IntermediateDescriptions[random.Next(IntermediateDescriptions.Length)];

                case EnumLevel.Advanced:
                    return AdvancedDescriptions[random.Next(AdvancedDescriptions.Length)];

                default:
                    return "No description available";
            }
        }

        private static readonly Random random = new Random();

        private static readonly string[] BeginnerDescriptions = new string[]
        {
            "You are taking your first steps. Enjoy every discovery!",
            "Every master was once a beginner. Keep going!",
            "The beginning can be tough, but every step is worth it!",
            "Welcome to your journey! Great things await you.",
            "Be brave! Every mistake is a chance to learn.",
            "The most important thing is to start. You've already done it!",
            "Explore, try, fail, and learn: that's how you grow!",
            "Small steps today lead to big achievements tomorrow.",
            "Believe in yourself — beginnings are powerful!",
            "Your adventure has just begun. Embrace the challenge!"
        };

        private static readonly string[] IntermediateDescriptions = new string[]
        {
            "You're already in the thick of it. Keep the momentum!",
            "Halfway there! Stay strong and focused.",
            "You've built a solid foundation. Now push forward!",
            "Challenges are opportunities — you're ready for them!",
            "Momentum is on your side. Keep moving!",
            "You've come far. Trust your progress!",
            "The journey is tough, but so are you!",
            "You're no longer a beginner. Own your progress!",
            "Consistency is your superpower. Keep it up!",
            "You're shaping your future one step at a time!"
        };

        private static readonly string[] AdvancedDescriptions = new string[]
        {
            "You're so close! Push through the final stretch!",
            "Mastery is within your reach. Stay sharp!",
            "You've climbed so high — don't stop now!",
            "This is where true greatness is forged. Keep going!",
            "Your skills are shining bright. Finish strong!",
            "The finish line is near. Give it your all!",
            "Excellence is achieved by those who persist. That's you!",
            "Your hard work is paying off. Keep believing!",
            "Victory is near — stay determined!",
            "You're setting an example for everyone. Lead the way!"
        };
    }
}
