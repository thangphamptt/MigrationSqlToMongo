using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Candidate
{
	public class ApplicationRepository
	{
		private CandidateDbContext _dbContext;
		public ApplicationRepository(IConfiguration configuration)
		{
			_dbContext = new CandidateDbContext(configuration);
		}

		public async Task<Domain.Candidate.AggregatesModel.Application> GetAsync(string id)
		{
			return await _dbContext.ApplicationCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task CreateAsync(Domain.Candidate.AggregatesModel.Application data)
		{
			await _dbContext.ApplicationCollection.InsertOneAsync(data);
		}
	}
}
