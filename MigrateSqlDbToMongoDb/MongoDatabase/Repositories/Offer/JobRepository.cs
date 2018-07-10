using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.Offer
{
	public class JobRepository
	{
		private OfferDbContext _dbContext;

		public JobRepository(IConfiguration configuration)
		{
			_dbContext = new OfferDbContext(configuration);
		}

		public async Task CreateJobAsync(Domain.Offer.AggregatesModel.Job job)
        {
            await _dbContext.JobCollection.InsertOneAsync(job);
        }

        public async Task<Domain.Offer.AggregatesModel.Job> GetJobByIdAsync(string jobId)
        {
            return await _dbContext.JobCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == jobId);
        }		
    }
}
