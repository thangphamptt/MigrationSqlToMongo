using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace MongoDatabase.DbContext
{
	public class EmailDbContext
    {
        private readonly IMongoDatabase _database;

        public EmailDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            _database = client.GetDatabase(configuration.GetSection("MongoDB:EmailDatabaseName").Value);
        }

		public IMongoCollection<Domain.Email.AggregatesModel.Email> EmailCollection
			=> _database.GetCollection<Domain.Email.AggregatesModel.Email>(nameof(Domain.Email.AggregatesModel.Email));

        public IQueryable<Domain.Email.AggregatesModel.Email> Emails => EmailCollection.AsQueryable();
    }
}
