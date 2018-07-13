﻿using Microsoft.Extensions.Configuration;
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

        private IMongoCollection<Position> PositionCollection => _database.GetCollection<Position>(nameof(Position));
        public IQueryable<Position> Positions => PositionCollection.AsQueryable();

		private IMongoCollection<Candidate> CandidateCollection => _database.GetCollection<Candidate>(nameof(Candidate));
		public IQueryable<Candidate> Candidates => CandidateCollection.AsQueryable();

        private IMongoCollection<ContractCode> ContractCodeCollection => _database.GetCollection<ContractCode>(nameof(ContractCode));
        public IQueryable<ContractCode> ContractCodes => ContractCodeCollection.AsQueryable();

        private IMongoCollection<ContractType> ContractTypeCollection => _database.GetCollection<ContractType>(nameof(ContractType));
        public IQueryable<ContractType> ContractTypes => ContractTypeCollection.AsQueryable();

        private IMongoCollection<Contract> ContractCollection => _database.GetCollection<Contract>(nameof(Contract));
        public IQueryable<Contract> Contracts => ContractCollection.AsQueryable();

		#region CV
		private IMongoCollection<Education> EducationCollection => _database.GetCollection<Education>(nameof(Education));
		public IQueryable<Education> Educations => EducationCollection.AsQueryable();

		private IMongoCollection<EmploymentHistory> EmploymentHistoryCollection => _database.GetCollection<EmploymentHistory>(nameof(EmploymentHistory));
		public IQueryable<EmploymentHistory> EmploymentHistories => EmploymentHistoryCollection.AsQueryable();
       
		private IMongoCollection<OutstandingProject> OutstandingProjectCollection => _database.GetCollection<OutstandingProject>(nameof(OutstandingProject));
		public IQueryable<OutstandingProject> OutstandingProjects => OutstandingProjectCollection.AsQueryable();

		private IMongoCollection<Skill> SkillCollection => _database.GetCollection<Skill>(nameof(Skill));
		public IQueryable<Skill> Skills => SkillCollection.AsQueryable();

		private IMongoCollection<JobApplicationAttachment> JobApplicationAttachmentCollection => _database.GetCollection<JobApplicationAttachment>(nameof(JobApplicationAttachment));
		public IQueryable<JobApplicationAttachment> JobApplicationAttachments => JobApplicationAttachmentCollection.AsQueryable();
		#endregion
    }
}
