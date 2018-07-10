using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Interview.AggregatesModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Interview.Persistance.Repositories
{
	public class OrganizationalUnitRepository
	{
		private readonly InterviewDbContext _dbContext;

		public OrganizationalUnitRepository(IConfiguration configuration)
		{
			_dbContext = new InterviewDbContext(configuration);
		}

		public async Task CreateOrganizationalUnitAsync(OrganizationalUnit organizationalUnit)
        {
            await _dbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
		}

		public async Task CreateOrganizationalUnitsAsync(IList<OrganizationalUnit> organizationalUnits)
		{
			await _dbContext.OrganizationalUnitCollection.InsertManyAsync(organizationalUnits);
		}

		public async Task<OrganizationalUnit> GetOrganizationalUnitByIdAsync(string organizationalUnitId)
        {
            return await _dbContext.OrganizationalUnitCollection.AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == organizationalUnitId);
		}

		public async Task<IList<OrganizationalUnit>> GetOrganizationalUnitsAsync(Expression<Func<OrganizationalUnit, bool>> filters)
		{
			return await _dbContext.OrganizationalUnitCollection.AsQueryable()
				.Where(filters).ToListAsync();
		}
	}
}
