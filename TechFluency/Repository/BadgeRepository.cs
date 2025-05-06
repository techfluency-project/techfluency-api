using MongoDB.Driver;
using TechFluency.Context;
using TechFluency.Models;

namespace TechFluency.Repository
{
    public class BadgeRepository : TechFluencyRepository<Badge>
    {
        public BadgeRepository(MongoDbContext context) : base(context, "Badges")
        {

        }

        public string GetBagdeIdByTitle(string title)
        {
            return _collection
                .Find(x => x.Title == title)
                .Project(x => x.Id)
                .FirstOrDefault();
        }
    }
}
