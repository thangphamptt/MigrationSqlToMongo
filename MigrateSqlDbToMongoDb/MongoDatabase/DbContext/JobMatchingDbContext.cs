using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace MongoDatabase.DbContext
{
    public class JobMatchingDbContext
    {
        private readonly IMongoDatabase _database;

        public JobMatchingDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            _database = client.GetDatabase(configuration.GetSection("MongoDB:JobMatchingDatabaseName").Value);
        }

        public IMongoCollection<Domain.JobMatching.AggregatesModel.Job> JobCollection => _database.GetCollection<Domain.JobMatching.AggregatesModel.Job>(nameof(Domain.JobMatching.AggregatesModel.Job));
        public IQueryable<Domain.JobMatching.AggregatesModel.Job> Jobs => JobCollection.AsQueryable();

        public IMongoCollection<Domain.JobMatching.AggregatesModel.Category> CategoryCollection => _database.GetCollection<Domain.JobMatching.AggregatesModel.Category>(nameof(Domain.JobMatching.AggregatesModel.Category));
        public IQueryable<Domain.JobMatching.AggregatesModel.Category> Categories => CategoryCollection.AsQueryable();

        public IMongoCollection<Domain.JobMatching.AggregatesModel.User> UserCollection => _database.GetCollection<Domain.JobMatching.AggregatesModel.User>(nameof(Domain.JobMatching.AggregatesModel.User));
        public IQueryable<Domain.JobMatching.AggregatesModel.User> Users => UserCollection.AsQueryable();

        public IMongoCollection<Domain.JobMatching.AggregatesModel.Candidate> CandidateCollection => _database.GetCollection<Domain.JobMatching.AggregatesModel.Candidate>(nameof(Domain.JobMatching.AggregatesModel.Candidate));
        public IQueryable<Domain.JobMatching.AggregatesModel.Candidate> Candidates => CandidateCollection.AsQueryable();

        public IMongoCollection<Domain.JobMatching.AggregatesModel.OrganizationalUnit> OrganizationalUnitCollection => _database.GetCollection<Domain.JobMatching.AggregatesModel.OrganizationalUnit>(nameof(Domain.JobMatching.AggregatesModel.OrganizationalUnit));
        public IQueryable<Domain.JobMatching.AggregatesModel.OrganizationalUnit> OrganizationalUnits => OrganizationalUnitCollection.AsQueryable();

        public IMongoCollection<Domain.JobMatching.AggregatesModel.SocCategory> SocCategoryCollection => _database.GetCollection<Domain.JobMatching.AggregatesModel.SocCategory>(nameof(Domain.JobMatching.AggregatesModel.SocCategory));
        public IQueryable<Domain.JobMatching.AggregatesModel.SocCategory> SocCategories => SocCategoryCollection.AsQueryable();
    }
}
