using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Template
{
	public class OrganizationalUnitRepository
	{
		private readonly TemplateDbContext _dbContext;

		public OrganizationalUnitRepository(IConfiguration configuration)
		{
			_dbContext = new TemplateDbContext(configuration);
		}

		public async Task<Domain.Template.AggregatesModel.OrganizationalUnit> GetOrganizationalUnitByIdAsync(string organizationalUnitId)
        {
            return await _dbContext.OrganizationalUnitCollection.AsQueryable().FirstOrDefaultAsync(f => f.Id == organizationalUnitId);
        }

        public async Task CreateOrganizationalUnitAsync(Domain.Template.AggregatesModel.OrganizationalUnit organizationalUnit)
        {
            await _dbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
        }

		public async Task<IList<Domain.Template.AggregatesModel.OrganizationalUnit>> GetOrganizationalUnitsAsync(Expression<Func<Domain.Template.AggregatesModel.OrganizationalUnit, bool>> filters)
		{
			return await _dbContext.OrganizationalUnitCollection.AsQueryable()
				.Where(filters).ToListAsync();
		}

		public async Task CreateOrganizationalUnitsAsync(IList<Domain.Template.AggregatesModel.OrganizationalUnit> organizationalUnits)
		{
			await _dbContext.OrganizationalUnitCollection.InsertManyAsync(organizationalUnits);
		}
	}
}
