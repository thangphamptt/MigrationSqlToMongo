using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;

namespace JobMatching.Persistance.Repositories
{
	public class CategoryRepository
    {
        private readonly JobMatchingDbContext _dbContext;

        public CategoryRepository(IConfiguration configuration)
		{
			_dbContext = new JobMatchingDbContext(configuration);
		}
	}
}
