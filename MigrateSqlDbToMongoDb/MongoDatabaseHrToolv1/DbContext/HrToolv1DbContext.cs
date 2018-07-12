using Microsoft.Extensions.Configuration;
using MongoDatabaseHrToolv1.Model;
using MongoDB.Driver;
using System.Linq;

namespace MongoDatabaseHrToolv1.DbContext
{
    public class HrToolv1DbContext
    {
        private readonly IMongoDatabase _database;

        public HrToolv1DbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            _database = client.GetDatabase(configuration.GetSection("MongoDB:HrToolv1DatabaseName").Value);
        }

        private IMongoCollection<Job> JobCollection => _database.GetCollection<Job>(nameof(Job));
        public IQueryable<Job> Jobs => JobCollection.AsQueryable();

        private IMongoCollection<RecruitmentTemplate> RecruitmentTemplateCollection => _database.GetCollection<RecruitmentTemplate>(nameof(RecruitmentTemplate));
        public IQueryable<RecruitmentTemplate> RecruitmentTemplates => RecruitmentTemplateCollection.AsQueryable();
		
		private IMongoCollection<JobStatus> JobStatusCollection => _database.GetCollection<JobStatus>(nameof(JobStatus));
		public IQueryable<JobStatus> JobStatuses => JobStatusCollection.AsQueryable();

		private IMongoCollection<JobApplication> JobApplicationCollection => _database.GetCollection<JobApplication>(nameof(JobApplication));
		public IQueryable<JobApplication> JobApplications => JobApplicationCollection.AsQueryable();

		private IMongoCollection<Candidate> CandidateCollection => _database.GetCollection<Candidate>(nameof(Candidate));
		public IQueryable<Candidate> Candidates => CandidateCollection.AsQueryable();

		private IMongoCollection<Education> EducationCollection => _database.GetCollection<Education>(nameof(Education));
		public IQueryable<Education> Educations => EducationCollection.AsQueryable();

	}
}
