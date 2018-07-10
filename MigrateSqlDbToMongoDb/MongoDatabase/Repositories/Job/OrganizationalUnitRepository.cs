using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Job
{
	public class OrganizationalUnitRepository
	{
		private JobDbContext _dbContext;
		public OrganizationalUnitRepository(IConfiguration configuration)
		{
			_dbContext = new JobDbContext(configuration);
		}

		public async Task<Domain.Job.AggregatesModel.OrganizationalUnit> GetAsync(string id)
		{
			return await _dbContext.OrganizationalUnitCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task CreateAsync(Domain.Job.AggregatesModel.OrganizationalUnit data)
		{
			await _dbContext.OrganizationalUnitCollection.InsertOneAsync(data);
		}
	}
}
