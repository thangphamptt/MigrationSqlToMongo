using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.JobMatching
{
	public class OrganizationalUnitRepository
    {
        private readonly JobMatchingDbContext _dbContext;

        public OrganizationalUnitRepository(IConfiguration configuration)
		{
			_dbContext = new JobMatchingDbContext(configuration);
		}

		public async Task CreateOrganizationalUnitAsync(Domain.JobMatching.AggregatesModel.OrganizationalUnit organizationalUnit)
        {
            await _dbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
        }

        public async Task CreateOrganizationalUnitsAsync(IList<Domain.JobMatching.AggregatesModel.OrganizationalUnit> organizationalUnits)
        {
            await _dbContext.OrganizationalUnitCollection.InsertManyAsync(organizationalUnits);
        }

        public async Task<Domain.JobMatching.AggregatesModel.OrganizationalUnit> GetOrganizationalUnitByIdAsync(string organizationalUnitId)
        {
            return await _dbContext.OrganizationalUnitCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == organizationalUnitId);
        }

        public async Task<IList<Domain.JobMatching.AggregatesModel.OrganizationalUnit>> GetOrganizationalUnitsAsync(Expression<Func<Domain.JobMatching.AggregatesModel.OrganizationalUnit, bool>> filters)
        {
            return await _dbContext.OrganizationalUnitCollection.AsQueryable()
                .Where(filters).ToListAsync();
        }

        public async Task UpdateOrganizationalUnitAsync(Domain.JobMatching.AggregatesModel.OrganizationalUnit organizationalUnit)
        {
            var result = Builders<Domain.JobMatching.AggregatesModel.OrganizationalUnit>.Filter.Where(x => x.Id == organizationalUnit.Id);
            var data = Builders<Domain.JobMatching.AggregatesModel.OrganizationalUnit>.Update.Set(x => x.Name, organizationalUnit.Name);
            await _dbContext.OrganizationalUnitCollection.UpdateOneAsync(result, data);
        }
    }
}
