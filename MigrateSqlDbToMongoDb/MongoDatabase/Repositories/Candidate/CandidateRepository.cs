using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.Candidate
{
	public class CandidateRepository
    {
		private CandidateDbContext _dbContext;
		public CandidateRepository(IConfiguration configuration)
		{
			_dbContext = new CandidateDbContext(configuration);
		}

		public async Task<Domain.Candidate.AggregatesModel.Candidate> GetCandidateAsync(string id)
		{
			return await _dbContext.CandidateCollection.AsQueryable().FirstOrDefaultAsync(x=>x.Id == id);
		}

		public async Task CreateCandidateAsync(Domain.Candidate.AggregatesModel.Candidate data)
		{
			await _dbContext.CandidateCollection.InsertOneAsync(data);
		}

	}
}
