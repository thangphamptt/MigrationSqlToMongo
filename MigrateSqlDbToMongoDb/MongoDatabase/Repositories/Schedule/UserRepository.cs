using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;

namespace MongoDatabase.Repositories.Schedule
{
	public class UserRepository
	{
		private readonly ScheduleDbContext _dbContext;

		public UserRepository(IConfiguration configuration)
		{
			_dbContext = new ScheduleDbContext(configuration);
		}

		public async Task<Domain.Schedule.AggregatesModel.User> GetUserByIdAsync(string id)
		{
			return await _dbContext.UserCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
		}

        public async Task AddUserAsync(Domain.Schedule.AggregatesModel.User user)
        {
            await _dbContext.UserCollection.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(Domain.Schedule.AggregatesModel.User user)
        {
            var filter = Builders<Domain.Schedule.AggregatesModel.User>.Filter.Where(x => x.Id == user.Id);
            await _dbContext.UserCollection.ReplaceOneAsync(filter, user);
        }
	}
}
