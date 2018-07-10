using System.Linq;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.Collaboration
{
	public class UserRepository
    {
        private readonly CollaborationDbContext _dbContext;

        public UserRepository(IConfiguration configuration)
		{
			_dbContext = new CollaborationDbContext(configuration);
		}

		public async Task<bool> IsUserExistAsync(string userId)
        {
            return await _dbContext.UserCollection.AsQueryable().FirstOrDefaultAsync(t => t.Id == userId) != null;
        }

        public async Task<Domain.Collaboration.AggregatesModel.User> GetUserByIdAsync(string userId)
        {
            return await _dbContext.UserCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task AddUserAsync(Domain.Collaboration.AggregatesModel.User user)
        {
            await _dbContext.UserCollection.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(Domain.Collaboration.AggregatesModel.User user)
        {
            var filter = Builders<Domain.Collaboration.AggregatesModel.User>.Filter.Where(x => x.Id == user.Id);
            await _dbContext.UserCollection.ReplaceOneAsync(filter, user);
        }
    }

}
