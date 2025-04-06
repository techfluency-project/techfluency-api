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

        public void Add(Question question)
        {
            _questionRepository.Add(question);
        }
    }
}
