using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Job
{
    public class CategoryRepository
	{
		private JobDbContext _dbContext;
		public CategoryRepository(IConfiguration configuration)
		{
			_dbContext = new JobDbContext(configuration);
		}

		public async Task<Domain.Job.AggregatesModel.Category> GetAsync(string id)
		{
			return await _dbContext.CategoryCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task CreateAsync(Domain.Job.AggregatesModel.Category data)
		{
			await _dbContext.CategoryCollection.InsertOneAsync(data);
		}
	}
}
