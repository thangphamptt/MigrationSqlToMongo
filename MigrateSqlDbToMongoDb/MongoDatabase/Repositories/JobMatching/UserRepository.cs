using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.JobMatching
{ 
	public class UserRepository
    {
        private readonly JobMatchingDbContext _dbContext;

        public UserRepository(IConfiguration configuration)
		{
			_dbContext = new JobMatchingDbContext(configuration);
		}

		public async Task CreateUserAsync(Domain.JobMatching.AggregatesModel.User user)
        {
            await _dbContext.UserCollection.InsertOneAsync(user);
        }

        public async Task<Domain.JobMatching.AggregatesModel.User> GetUserByIdAsync(string userId)
        {
            return await _dbContext.UserCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task UpdateUserAsync(Domain.JobMatching.AggregatesModel.User user)
        {
            var result = Builders<Domain.JobMatching.AggregatesModel.User>.Filter.Where(x => x.Id == user.Id);
            await _dbContext.UserCollection.ReplaceOneAsync(result, user);
        }
    }
}
