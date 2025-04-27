using System.Reflection.Emit;
using Microsoft.AspNetCore.Http.HttpResults;
using TechFluency.DTOs;
using TechFluency.Enums;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class PlacementTestService
    {
        private readonly QuestionRepository _questionRespository;
        private readonly UserProgresRepository _userProgresRepository;
        private readonly JwtService _jwtService;


        public PlacementTestService(QuestionRepository questionRespository, JwtService jwtService, UserProgresRepository userProgresRepository)
        {
            _questionRespository = questionRespository;
            _jwtService = jwtService;
            _userProgresRepository = userProgresRepository;
        }

        public List<Question> GetQuestionsForPlacementTest()
        {
            try
            {
                var random = new Random();
                var levelsList = Enum.GetValues(typeof(EnumLevel)).Cast<EnumLevel>().ToList();
                var listQuestions = new List<Question>();
                foreach (var level in levelsList)
                {
                    var questions = _questionRespository.GetQuestionsByLevel(level);
                    var fiveRandom = questions.OrderBy(q => random.Next()).Take(5).ToList();
                    listQuestions.AddRange(fiveRandom);
                }

                return listQuestions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> GetResultFromPlacementTest(List<UserAnswerDTO> userAnswers, string userId)
        {
            var totalScore = 0;
            object lockObj = new();
            var userLevel = EnumLevel.Beginner;
            var userProgress = _userProgresRepository.GetUserProgress(userId);


            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4 
            };

            Parallel.ForEach(userAnswers, parallelOptions, answer =>
            {
                var question = _questionRespository.GetQuestionById(answer.QuestionId);
                
                if(question.CorrectAnswer == answer.SelectedOption)
                {
                    int weight = question.Level switch
                    {
                        EnumLevel.Beginner => 1,
                        EnumLevel.Intermediate => 2,
                        EnumLevel.Advanced => 3,
                        _ => 0 
                    };

                    lock(lockObj)
                    {
                        totalScore += weight;
                    }
                }
            });

            if (totalScore <= 10)
            {
                userLevel = EnumLevel.Beginner;
            }
            else if (totalScore <= 20)
            {
                userLevel = EnumLevel.Intermediate;
            }
            else
            {
                userLevel = EnumLevel.Advanced;
            }

            userProgress.Level = userLevel;
            _userProgresRepository.Update(userProgress.Id, userProgress);

            return userLevel.ToString();
        }

    }
}
