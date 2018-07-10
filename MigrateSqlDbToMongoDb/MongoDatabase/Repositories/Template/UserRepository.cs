using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDatabase.Repositories.Template
{
	public class UserRepository
	{
		private readonly TemplateDbContext _dbContext;

		public UserRepository(IConfiguration configuration)
		{
			_dbContext = new TemplateDbContext(configuration);
		}

		public async Task<Domain.Template.AggregatesModel.User> GetUserByIdAsync(string id)
		{
			return await _dbContext.UserCollection.AsQueryable()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

        public async Task CreateUserAsync(Domain.Template.AggregatesModel.User user)
        {
            await _dbContext.UserCollection.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(Domain.Template.AggregatesModel.User user)
        {
            var filter = Builders<Domain.Template.AggregatesModel.User>.Filter.Where(x => x.Id == user.Id);
            await _dbContext.UserCollection.ReplaceOneAsync(filter, user);
        }
    }
}