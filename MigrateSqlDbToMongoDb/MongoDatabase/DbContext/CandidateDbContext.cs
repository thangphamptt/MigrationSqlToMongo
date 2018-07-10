using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace MongoDatabase.DbContext
{
	public class CandidateDbContext
	{
		private readonly IMongoDatabase _database;

		public CandidateDbContext(IConfiguration configuration)
		{
			var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
			_database = client.GetDatabase(configuration.GetSection("MongoDB:CandidateDatabaseName").Value);
		}

		public IMongoCollection<Domain.Candidate.AggregatesModel.Candidate> CandidateCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.Candidate>(nameof(Domain.Candidate.AggregatesModel.Candidate));
		public IQueryable<Domain.Candidate.AggregatesModel.Candidate> Candidates => CandidateCollection.AsQueryable();

		public IMongoCollection<Domain.Candidate.AggregatesModel.Pipeline> PipelineCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.Pipeline>(nameof(Domain.Candidate.AggregatesModel.Pipeline));
		public IQueryable<Domain.Candidate.AggregatesModel.Pipeline> Pipelines => PipelineCollection.AsQueryable();

		public IMongoCollection<Domain.Candidate.AggregatesModel.Application> ApplicationCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.Application>(nameof(Domain.Candidate.AggregatesModel.Application));
		public IQueryable<Domain.Candidate.AggregatesModel.Application> Applications => ApplicationCollection.AsQueryable();

		public IMongoCollection<Domain.Candidate.AggregatesModel.Job> JobCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.Job>(nameof(Domain.Candidate.AggregatesModel.Job));
		public IQueryable<Domain.Candidate.AggregatesModel.Job> Jobs => JobCollection.AsQueryable();

		public IMongoCollection<Domain.Candidate.AggregatesModel.User> UserCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.User>(nameof(Domain.Candidate.AggregatesModel.User));
		public IQueryable<Domain.Candidate.AggregatesModel.User> Users => UserCollection.AsQueryable();

		public IMongoCollection<Domain.Candidate.AggregatesModel.JobCategory> JobCategoryCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.JobCategory>(nameof(Domain.Candidate.AggregatesModel.JobCategory));
		public IQueryable<Domain.Candidate.AggregatesModel.JobCategory> JobCategories => JobCategoryCollection.AsQueryable();

		public IMongoCollection<Domain.Candidate.AggregatesModel.InterestProfile> InterestProfileCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.InterestProfile>(nameof(Domain.Candidate.AggregatesModel.InterestProfile));
		public IQueryable<Domain.Candidate.AggregatesModel.InterestProfile> InterestProfiles => InterestProfileCollection.AsQueryable();

		public IMongoCollection<Domain.Candidate.AggregatesModel.JobFamily> JobFamilyCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.JobFamily>(nameof(Domain.Candidate.AggregatesModel.JobFamily));
		public IQueryable<Domain.Candidate.AggregatesModel.JobFamily> JobFamilies => JobFamilyCollection.AsQueryable();

		public IMongoCollection<Domain.Candidate.AggregatesModel.OrganizationalUnit> OrganizationalUnitCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.OrganizationalUnit>(nameof(Domain.Candidate.AggregatesModel.OrganizationalUnit));
		public IQueryable<Domain.Candidate.AggregatesModel.OrganizationalUnit> OrganizationalUnits => OrganizationalUnitCollection.AsQueryable();

		public IMongoCollection<Domain.Candidate.AggregatesModel.Template> TemplateCollection => _database.GetCollection<Domain.Candidate.AggregatesModel.Template>(nameof(Domain.Candidate.AggregatesModel.Template));
		public IQueryable<Domain.Candidate.AggregatesModel.Template> Templates => TemplateCollection.AsQueryable();
	}
}
