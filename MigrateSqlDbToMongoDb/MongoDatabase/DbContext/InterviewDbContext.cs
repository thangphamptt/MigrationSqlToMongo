using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace MongoDatabase.DbContext
{
	public class InterviewDbContext
	{
		private readonly IMongoDatabase _database;

		public InterviewDbContext(IConfiguration configuration)
		{
			var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
			_database = client.GetDatabase(configuration.GetSection("MongoDB:InterviewDatabaseName").Value);
		}

		public IMongoCollection<Domain.Interview.AggregatesModel.Interview> InterviewCollection => _database.GetCollection<Domain.Interview.AggregatesModel.Interview>(nameof(Domain.Interview.AggregatesModel.Interview));
		public IQueryable<Domain.Interview.AggregatesModel.Interview> Interviews => InterviewCollection.AsQueryable();

		public IMongoCollection<Domain.Interview.AggregatesModel.InterviewType> InterviewTypeCollection => _database.GetCollection<Domain.Interview.AggregatesModel.InterviewType>(nameof(Domain.Interview.AggregatesModel.InterviewType));
		public IQueryable<Domain.Interview.AggregatesModel.InterviewType> InterviewTypes => InterviewTypeCollection.AsQueryable();

		public IMongoCollection<Domain.Interview.AggregatesModel.Job> JobCollection => _database.GetCollection<Domain.Interview.AggregatesModel.Job>(nameof(Domain.Interview.AggregatesModel.Job));
		public IQueryable<Domain.Interview.AggregatesModel.Job> Jobs => JobCollection.AsQueryable();

		public IMongoCollection<Domain.Interview.AggregatesModel.User> UserCollection => _database.GetCollection<Domain.Interview.AggregatesModel.User>(nameof(Domain.Interview.AggregatesModel.User));
		public IQueryable<Domain.Interview.AggregatesModel.User> Users => UserCollection.AsQueryable();

		public IMongoCollection<Domain.Interview.AggregatesModel.Candidate> CandidateCollection => _database.GetCollection<Domain.Interview.AggregatesModel.Candidate>(nameof(Domain.Interview.AggregatesModel.Candidate));
		public IQueryable<Domain.Interview.AggregatesModel.Candidate> Candidates => CandidateCollection.AsQueryable();

		public IMongoCollection<Domain.Interview.AggregatesModel.OrganizationalUnit> OrganizationalUnitCollection => _database.GetCollection<Domain.Interview.AggregatesModel.OrganizationalUnit>(nameof(Domain.Interview.AggregatesModel.OrganizationalUnit));
		public IQueryable<Domain.Interview.AggregatesModel.OrganizationalUnit> OrganizationalUnits => OrganizationalUnitCollection.AsQueryable();

		public IMongoCollection<Domain.Interview.AggregatesModel.Template> TemplateCollection => _database.GetCollection<Domain.Interview.AggregatesModel.Template>(nameof(Domain.Interview.AggregatesModel.Template));
		public IQueryable<Domain.Interview.AggregatesModel.Template> Templates => TemplateCollection.AsQueryable();

		public IMongoCollection<Domain.Interview.AggregatesModel.AssessmentType> AssessmentTypeCollection => _database.GetCollection<Domain.Interview.AggregatesModel.AssessmentType>(nameof(Domain.Interview.AggregatesModel.AssessmentType));
		public IQueryable<Domain.Interview.AggregatesModel.AssessmentType> AssessmentTypes => AssessmentTypeCollection.AsQueryable();

		public IMongoCollection<Domain.Interview.AggregatesModel.Location> LocationCollection => _database.GetCollection<Domain.Interview.AggregatesModel.Location>(nameof(Domain.Interview.AggregatesModel.Location));
		public IQueryable<Domain.Interview.AggregatesModel.Location> Locations => LocationCollection.AsQueryable();

		public IMongoCollection<Domain.Interview.AggregatesModel.Application> ApplicationCollection => _database.GetCollection<Domain.Interview.AggregatesModel.Application>(nameof(Domain.Interview.AggregatesModel.Application));
		public IQueryable<Domain.Interview.AggregatesModel.Application> Applications => ApplicationCollection.AsQueryable();
	}
}
