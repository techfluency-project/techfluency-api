using TechFluency.DTOs;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class UserProgressService
    {
        private readonly QuestionService _questionService;
        private readonly LearningPathService _learningPathService;
        private readonly UserProgresRepository _userProgresRepository;
        private readonly PathStageRepository _pathStageRepository;

        public UserProgressService(
            QuestionService questionService,
            LearningPathService learningPathService,
            UserProgresRepository userProgresRepository,
            PathStageRepository pathStageRepository)
        {
            _questionService = questionService;
            _learningPathService = learningPathService;
            _userProgresRepository = userProgresRepository;
            _pathStageRepository = pathStageRepository;
        }

        public async Task<UserAnswerResultDTO> AnswerQuestionAndUpdateProgress(UserAnswerPathDTO answerDto, string userId)
        {
            var result = _questionService.AnswerQuestion(answerDto, userId);

            if (result.StageCompleted)
            {
                var userProgress = _userProgresRepository.GetUserProgress(userId);
                var completedStages = _pathStageRepository.GetStagesCompleted(userProgress.LearningPathId);

                if (completedStages.Count() > 5) 
                {
                    _learningPathService.PromoteUserToNextLevel(userProgress);
                    _userProgresRepository.Update(userProgress.Id, userProgress);
                }
            }

            return result;
        }
    }
}
