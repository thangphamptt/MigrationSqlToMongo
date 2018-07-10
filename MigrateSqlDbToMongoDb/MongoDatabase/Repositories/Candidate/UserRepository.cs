using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.Candidate
{
    public class UserRepository
    {
		private CandidateDbContext _dbContext;
		public UserRepository(IConfiguration configuration)
		{
			_dbContext = new CandidateDbContext(configuration);
		}

		public async Task<Domain.Candidate.AggregatesModel.User> GetCandidateAsync(string id)
		{
			return await _dbContext.UserCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task CreateCandidateAsync(Domain.Candidate.AggregatesModel.User data)
		{
			await _dbContext.UserCollection.InsertOneAsync(data);
		}
	}
}
