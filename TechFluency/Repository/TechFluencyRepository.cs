using MongoDB.Driver;
using TechFluency.Models;
using TechFluency.Context;

namespace TechFluency.Repository
{
    public class TechFluencyRepository
    {
        public IMongoCollection<User> _userCollection { get; set; }
        public TechFluencyRepository(MongoDbContext context)
        {
            _userCollection = context.Database.GetCollection<User>("Users");
        }
    }
}
