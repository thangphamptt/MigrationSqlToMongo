using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Candidate
{
    public class PipelineRepository
    {
		private CandidateDbContext _dbContext;
		public PipelineRepository(IConfiguration configuration)
		{
			_dbContext = new CandidateDbContext(configuration);
		}

		public async Task<Domain.Candidate.AggregatesModel.Pipeline> GetAsync(string id)
		{
			return await _dbContext.PipelineCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task CreateAsync(Domain.Candidate.AggregatesModel.Pipeline data)
		{
			await _dbContext.PipelineCollection.InsertOneAsync(data);
		}
	}
}
