using MongoDB.Driver;
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

        public async Task<User> GetUserById(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsername(string userName)
        {
            return await _collection.Find(x => x.Username == userName).FirstOrDefaultAsync();
        }
    }
}
