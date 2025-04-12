using TechFluency.Context;
using TechFluency.Models;

namespace TechFluency.Repository
{
    public class UserRepository : TechFluencyRepository<User>
    {
        public UserRepository(MongoDbContext context) : base(context, "Users")
        {

        }

        public void GetQuestionsByLevel()
        {
            
        }
    }
}
