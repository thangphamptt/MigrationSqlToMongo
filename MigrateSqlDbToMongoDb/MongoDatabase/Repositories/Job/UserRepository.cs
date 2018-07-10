using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Job
{
	public class UserRepository
	{
		private JobDbContext _dbContext;

		public UserRepository(IConfiguration configuration)
		{
			_dbContext = new JobDbContext(configuration);
		}

		public async Task<Domain.Job.AggregatesModel.User> GetAsync(string id)
		{
			return await _dbContext.UserCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task CreateAsync(Domain.Job.AggregatesModel.User data)
		{
			await _dbContext.UserCollection.InsertOneAsync(data);
		}
	}
}
