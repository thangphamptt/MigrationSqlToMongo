using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDatabase.Domain.Interview.AggregatesModel;

namespace Interview.Persistance.Repositories
{
	public class JobRepository
	{
		private readonly InterviewDbContext _dbContext;

		public JobRepository(IConfiguration configuration)
		{
			_dbContext = new InterviewDbContext(configuration);
		}

		public async Task CreateJobAsync(Job job)
		{
			await _dbContext.JobCollection.InsertOneAsync(job);
		}

		public async Task<Job> GetJobByIdAsync(string jobId)
		{
			return await _dbContext.JobCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == jobId);
		}

		public async Task UpdateJobAsync(Job job)
		{
			var filter = Builders<Job>.Filter.Where(x => x.Id == job.Id);
			UpdateDefinition<Job> update = Builders<Job>.Update.Set(x => x.Name, job.Name)
															.Set(x => x.Status, job.Status);

			await _dbContext.JobCollection.UpdateOneAsync(filter, update);
		}
	}
}
