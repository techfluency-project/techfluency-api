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

        public PlacementTestService(QuestionRepository questionRespository)
        {
            _questionRespository = questionRespository;
        }

        public List<Question> GetQuestionsForPlacementTest()
        {
            try
            {
                var random = new Random();
                var levelsList = Enum.GetNames(typeof(EnumLevel)).ToList();
                var listQuestions = new List<Question>();
                foreach (var level in levelsList)
                {
                    var questions = _questionRespository.GetQuestionByLevel(level);
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

        public string GetResultFromPlacementTest(List<UserAnswerDTO> userAnswers)
        {
            var totalScore = 0;
            object lockObj = new();
            var userLevel = EnumLevel.Beginner;

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

            return userLevel.ToString();
        }

    }
}
