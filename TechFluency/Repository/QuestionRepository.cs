using TechFluency.Context;
using TechFluency.Models;

namespace TechFluency.Repository
{
    public class QuestionRepository : TechFluencyRepository<Question>
    {
        public QuestionRepository(MongoDbContext context) : base(context, "Questions")
        {
        }
    }
}
