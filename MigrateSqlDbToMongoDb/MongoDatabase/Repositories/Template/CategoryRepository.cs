using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Template
{
	public class JobCategoryRepository
	{
		private readonly TemplateDbContext _dbContext;

		public JobCategoryRepository(IConfiguration configuration)
		{
			_dbContext = new TemplateDbContext(configuration);
		}

		public async Task<IList<Domain.Template.AggregatesModel.JobCategory>> GetJobCategoriesAsync()
		{
			return await _dbContext.JobCategoryCollection.AsQueryable().ToListAsync();
		}
	}
}
