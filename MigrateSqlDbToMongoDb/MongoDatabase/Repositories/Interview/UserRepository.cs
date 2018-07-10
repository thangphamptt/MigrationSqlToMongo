using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDatabase.Domain.Interview.AggregatesModel;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;

namespace Interview.Persistance.Repositories
{
	public class UserRepository
	{
		private readonly InterviewDbContext _dbContext;

		public UserRepository(IConfiguration configuration)
		{
			_dbContext = new InterviewDbContext(configuration);
		}

		public async Task<User> GetUserByIdAsync(string id)
		{
			return await _dbContext.UserCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
		}

        public async Task AddUserAsync(User user)
        {
            await _dbContext.UserCollection.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            var filter = Builders<User>.Filter.Where(x => x.Id == user.Id);
            await _dbContext.UserCollection.ReplaceOneAsync(filter, user);
        }
	}
}
