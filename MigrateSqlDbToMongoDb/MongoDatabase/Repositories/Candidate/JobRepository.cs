using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Candidate.AggregatesModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Candidate.Persistance.Repositories
{
	public class JobRepository
    {
        private readonly CandidateDbContext _dbContext;

        public JobRepository(IConfiguration configuration)
		{
			_dbContext = new CandidateDbContext(configuration);
		}

		public async Task CreateJobAsync(Job job)
        {
            await _dbContext.JobCollection.InsertOneAsync(job);
        }

        public Task<Job> GetJobAsync(Expression<Func<Job, bool>> filter)
        {
            return _dbContext.JobCollection.AsQueryable().FirstOrDefaultAsync(filter);
        }

        public Job GetJobById(string jobId)
        {
            return _dbContext.JobCollection.AsQueryable().FirstOrDefault(x => x.Id == jobId);
        }

		public async Task<Job> GetPublishedJobAsync(string jobId, IList<string> organizationalUnitIds)
		{
			return await _dbContext.JobCollection.AsQueryable()
												.FirstOrDefaultAsync(x => x.Id == jobId
																		  && x.Status == JobStatus.Published
																		  && organizationalUnitIds.Contains(x.OrganizationalUnitId));
		}

		public async Task UpdateJobAsync(Job job)
        {
            var filter = Builders<Job>.Filter.Where(x => x.Id == job.Id);
            UpdateDefinition<Job> update = Builders<Job>.Update.Set(x => x.Name, job.Name)
                                                            .Set(x => x.JobCategoryIds, job.JobCategoryIds)
                                                            .Set(x => x.Status, job.Status)
                                                            .Set(x => x.Location, job.Location)
                                                            .Set(x => x.OrganizationalUnitId, job.OrganizationalUnitId)
                                                            .Set(x => x.Vacancies, job.Vacancies)
                                                            .Set(x => x.ExpirationDate, job.ExpirationDate);
            await _dbContext.JobCollection.UpdateOneAsync(filter, update);
        }
    }
}
