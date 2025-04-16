using TechFluency.Enums;
using TechFluency.Models;
using TechFluency.Repository;

namespace TechFluency.Services
{
    public class QuestionService
    {
        private readonly QuestionRepository _questionRepository;

        public QuestionService(QuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
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
                                    .Take(5)
                                    .Select(x => x.Id)
                                    .ToList();

            return randomQuestions;
        }
    }
}
