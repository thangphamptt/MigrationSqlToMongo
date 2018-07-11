using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Candidate.AggregatesModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Candidate.Persistance.Repositories
{
	public class OrganizationalUnitRepository
	{
		private readonly CandidateDbContext _dbContext;

		public OrganizationalUnitRepository(IConfiguration configuration)
		{
			_dbContext = new CandidateDbContext(configuration);
		}

		public async Task<OrganizationalUnit> GetOrganizationalUnitByIdAsync(string organizationalUnitId)
		{
            try
            {
                return await _dbContext.OrganizationalUnitCollection.AsQueryable()
                .FirstOrDefaultAsync(f => f.Id == organizationalUnitId);
            }
            catch(Exception ex)
            {
                throw ex;
            }			
		}

		public async Task CreateOrganizationalUnitAsync(OrganizationalUnit organizationalUnit)
		{
			await _dbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
		}

		public async Task<IList<OrganizationalUnit>> GetOrganizationalUnitsAsync(Expression<Func<OrganizationalUnit, bool>> filters)
		{
			return await _dbContext.OrganizationalUnitCollection.AsQueryable()
				.Where(filters).ToListAsync();
		}

		public async Task CreateOrganizationalUnitsAsync(IList<OrganizationalUnit> organizationalUnits)
		{
			await _dbContext.OrganizationalUnitCollection.InsertManyAsync(organizationalUnits);
		}
	}
}
