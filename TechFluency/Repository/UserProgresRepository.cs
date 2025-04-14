using MongoDB.Driver;
using TechFluency.Context;
using TechFluency.Models;

namespace TechFluency.Repository
{
    public class UserProgresRepository : TechFluencyRepository<UserProgress>
    {
        public UserProgresRepository(MongoDbContext context) : base(context, "UsersProgress")
        {
            
        }

        public UserProgress GetUserProgress(string id)
        {
            return _collection.Find(x => x.UserId == id).FirstOrDefault();
        }
    }
}
