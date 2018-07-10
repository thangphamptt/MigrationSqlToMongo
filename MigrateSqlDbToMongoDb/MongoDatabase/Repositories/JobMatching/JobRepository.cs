using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.JobMatching
{
	public class JobRepository
    {
        private readonly JobMatchingDbContext _dbContext;

        public JobRepository(IConfiguration configuration)
		{
			_dbContext = new JobMatchingDbContext(configuration);
		}

		public async Task CreateJobAsync(Domain.JobMatching.AggregatesModel.Job job)
        {
            await _dbContext.JobCollection.InsertOneAsync(job);
        }

        public async Task<Domain.JobMatching.AggregatesModel.Job> GetJobByIdAsync(string jobId)
        {
            return await _dbContext.JobCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == jobId);
        }

        public async Task UpdateJobAsync(Domain.JobMatching.AggregatesModel.Job job)
        {
            var result = Builders<Domain.JobMatching.AggregatesModel.Job>.Filter.Where(x => x.Id == job.Id);
            var data = Builders<Domain.JobMatching.AggregatesModel.Job>.Update.Set(x => x.Name, job.Name)
                                           .Set(x => x.OrganizationalUnitId, job.OrganizationalUnitId)
                                           .Set(x => x.Status, job.Status)
                                           .Set(x => x.CategoryIds, job.CategoryIds)
                                           .Set(x => x.Description, job.Description)
                                           .Set(x => x.Summary, job.Summary)
                                           .Set(x => x.PositionLevel, job.PositionLevel)
                                           .Set(x => x.JobType, job.JobType);

            await _dbContext.JobCollection.UpdateOneAsync(result, data);
        }
    }
}
