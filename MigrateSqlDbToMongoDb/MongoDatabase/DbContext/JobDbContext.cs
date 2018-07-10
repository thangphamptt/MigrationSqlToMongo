using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace MongoDatabase.DbContext
{
	public class JobDbContext
	{
		private readonly IMongoDatabase _database;

		public JobDbContext(IConfiguration configuration)
		{
			var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
			_database = client.GetDatabase(configuration.GetSection("MongoDB:JobDatabaseName").Value);
		}

		public IMongoCollection<Domain.Job.AggregatesModel.Job> JobCollection => _database.GetCollection<Domain.Job.AggregatesModel.Job>(nameof(Domain.Job.AggregatesModel.Job));
		public IQueryable<Domain.Job.AggregatesModel.Job> Jobs => JobCollection.AsQueryable();

		public IMongoCollection<Domain.Job.AggregatesModel.Category> CategoryCollection => _database.GetCollection<Domain.Job.AggregatesModel.Category>(nameof(Domain.Job.AggregatesModel.Category));
		public IQueryable<Domain.Job.AggregatesModel.Category> Categories => CategoryCollection.AsQueryable();

		public IMongoCollection<Domain.Job.AggregatesModel.User> UserCollection => _database.GetCollection<Domain.Job.AggregatesModel.User>(nameof(Domain.Job.AggregatesModel.User));
		public IQueryable<Domain.Job.AggregatesModel.User> Users => UserCollection.AsQueryable();

		public IMongoCollection<Domain.Job.AggregatesModel.OrganizationalUnit> OrganizationalUnitCollection => _database.GetCollection<Domain.Job.AggregatesModel.OrganizationalUnit>(nameof(Domain.Job.AggregatesModel.OrganizationalUnit));
		public IQueryable<Domain.Job.AggregatesModel.OrganizationalUnit> OrganizationalUnits => OrganizationalUnitCollection.AsQueryable();

		public IMongoCollection<Domain.Job.AggregatesModel.Candidate> CandidateCollection => _database.GetCollection<Domain.Job.AggregatesModel.Candidate>(nameof(Domain.Job.AggregatesModel.Candidate));
		public IQueryable<Domain.Job.AggregatesModel.Candidate> Candidates => CandidateCollection.AsQueryable();
		public IMongoCollection<Domain.Job.AggregatesModel.SocCategory> SocCategoryCollection => _database.GetCollection<Domain.Job.AggregatesModel.SocCategory>(nameof(Domain.Job.AggregatesModel.SocCategory));
		public IQueryable<Domain.Job.AggregatesModel.SocCategory> SocCategories => SocCategoryCollection.AsQueryable();
	}
}
