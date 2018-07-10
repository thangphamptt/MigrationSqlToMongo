using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Job
{
	public class JobRepository
	{
		private JobDbContext _dbContext;
		public JobRepository(IConfiguration configuration)
		{
			_dbContext = new JobDbContext(configuration);
		}

		public async Task<Domain.Job.AggregatesModel.Job> GetAsync(string id)
		{
			return await _dbContext.JobCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task CreateAsync(Domain.Job.AggregatesModel.Job data)
		{
			await _dbContext.JobCollection.InsertOneAsync(data);
		}
	}
}
