using MongoDB.Driver;
using TechFluency.Context;
using TechFluency.Models;
using TechFluency.Enums;

namespace TechFluency.Repository
{
    public class QuestionRepository : TechFluencyRepository<Question>
    {
        public QuestionRepository(MongoDbContext context) : base(context, "Questions")
        {

        }

        public Question GetQuestionById(string id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Question> GetQuestionsByLevel(EnumLevel level) 
        {
            return _collection.Find(x => x.Level == level).ToList();
        }

        public List<string> GetQuestionsForStage(EnumLevel level, EnumTopic topic)
        {
            var random = new Random();
            var questions = this.GetQuestionsByLevel(level);
            var questionsByTopic = questions.Where(x => x.Topic == topic);
            var randomQuestions = questionsByTopic
                                    .OrderBy(x => random.Next())
                                    .Take(8)
                                    .Select(x => x.Id)
                                    .ToList();

            return randomQuestions;
        }
    }
}
