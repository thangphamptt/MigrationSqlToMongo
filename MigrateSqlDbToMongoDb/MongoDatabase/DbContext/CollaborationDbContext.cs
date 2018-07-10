using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;
using MongoDatabase.Domain.Collaboration.AggregatesModel;

namespace MongoDatabase.DbContext
{
	public class CollaborationDbContext
    {
        private readonly IMongoDatabase _database;

        public CollaborationDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            _database = client.GetDatabase(configuration.GetSection("MongoDB:CollaborationDatabaseName").Value);
        }

        public IMongoCollection<Activity> ActivityCollection => _database.GetCollection<Activity>(nameof(Activity));
        public IQueryable<Activity> Activities => ActivityCollection.AsQueryable();
        public IMongoCollection<User> UserCollection => _database.GetCollection<User>(nameof(User));
        public IQueryable<User> Users => UserCollection.AsQueryable();
    }
}
