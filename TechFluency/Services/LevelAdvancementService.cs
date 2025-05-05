using TechFluency.Enums;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class LevelAdvancementService
    {
        private readonly LearningPathService _learningPathService;
        private readonly UserProgresRepository _userProgresRepository;

        public LevelAdvancementService(LearningPathService learningPathService, UserProgresRepository userProgresRepository)
        {
            _learningPathService = learningPathService;
            _userProgresRepository = userProgresRepository;
        }

        public async Task<Boolean> ChangeToNextStage(string userId)
        {
            var userProgress = _userProgresRepository.GetUserProgress(userId);
            
            foreach (var stage in userProgress.StageProgresses)
            {
                if (stage.IsCompleted == false)
                {
                    return false;
                }
            }    

            var nivelAtual = userProgress.Level;

            if (nivelAtual == EnumLevel.Beginner)
            {
                userProgress.Level = EnumLevel.Intermediate;
            }
            else if (nivelAtual == EnumLevel.Intermediate)
            {
               userProgress.Level = EnumLevel.Advanced;
            }
            else
            {
               userProgress.pathsCompleted = true;
                _userProgresRepository.Update(userProgress.Id, userProgress);
                return true;
            }
            userProgress.StageProgresses = null;
            _userProgresRepository.Update(userProgress.Id, userProgress);
            await _learningPathService.MountingLearningPath(userProgress.UserId);
            return true;         
        }
    }
}
