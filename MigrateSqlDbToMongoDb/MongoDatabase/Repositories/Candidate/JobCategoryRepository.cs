using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Candidate.AggregatesModel;
using MongoDB.Driver;

namespace Candidate.Persistance.Repositories
{
	public class JobCategoryRepository
	{
		private CandidateDbContext _dbContext;
		public JobCategoryRepository(IConfiguration configuration)
		{
			_dbContext = new CandidateDbContext(configuration);
		}

		public async Task<IList<JobCategory>> GetJobCategoriesAsync()
		{
			return await _dbContext.JobCategoryCollection.AsQueryable().ToListAsync();
		}
	}
}
