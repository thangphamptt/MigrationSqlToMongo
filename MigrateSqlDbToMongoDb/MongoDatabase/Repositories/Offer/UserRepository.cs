using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.Offer
{
	public class UserRepository
	{
		private OfferDbContext _dbContext;

		public UserRepository(IConfiguration configuration)
		{
			_dbContext = new OfferDbContext(configuration);
		}

		public async Task CreateUserAsync(Domain.Offer.AggregatesModel.User user)
        {
            await _dbContext.UserCollection.InsertOneAsync(user);
        }

        public async Task<Domain.Offer.AggregatesModel.User> GetUserByIdAsync(string userId)
        {
            return await _dbContext.UserCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task UpdateUserAsync(Domain.Offer.AggregatesModel.User user)
        {
            var result = Builders<Domain.Offer.AggregatesModel.User>.Filter.Where(x => x.Id == user.Id);
            await _dbContext.UserCollection.ReplaceOneAsync(result, user);
        }
    }
}
