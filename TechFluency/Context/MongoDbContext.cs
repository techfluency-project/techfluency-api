using MongoDB.Driver;

namespace TechFluency.Context
{
    public class MongoDbContext
    {
        private readonly IConfiguration _config;
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration config)
        {
            _config = config;

            var connectionString = _config.GetConnectionString("DefaultConnection");

            var mongoURL = MongoUrl.Create(connectionString);

            var mongoClient = new MongoClient(mongoURL);
            _database = mongoClient.GetDatabase(mongoURL.DatabaseName);
        }
        public IMongoDatabase Database => _database;

    }
}
