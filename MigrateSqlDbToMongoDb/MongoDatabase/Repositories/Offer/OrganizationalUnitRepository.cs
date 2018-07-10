using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.Offer
{
	public class OrganizationalUnitRepository
    {
        private readonly OfferDbContext _dbContext;

        public OrganizationalUnitRepository(IConfiguration configuration)
		{
			_dbContext = new OfferDbContext(configuration);
		}

		public async Task CreateOrganizationalUnitAsync(Domain.Offer.AggregatesModel.OrganizationalUnit organizationalUnit)
        {
            await _dbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
        }

		public async Task CreateOrganizationalUnitsAsync(IList<Domain.Offer.AggregatesModel.OrganizationalUnit> organizationalUnits)
		{
			await _dbContext.OrganizationalUnitCollection.InsertManyAsync(organizationalUnits);
		}

		public async Task<Domain.Offer.AggregatesModel.OrganizationalUnit> GetOrganizationalUnitByIdAsync(string organizationalUnitId)
        {
            return await _dbContext.OrganizationalUnitCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == organizationalUnitId);
        }

		public async Task<IList<Domain.Offer.AggregatesModel.OrganizationalUnit>> GetOrganizationalUnitsAsync(Expression<Func<Domain.Offer.AggregatesModel.OrganizationalUnit, bool>> filters)
		{
			return await _dbContext.OrganizationalUnitCollection.AsQueryable()
				.Where(filters).ToListAsync();
		}

		public async Task UpdateOrganizationalUnitAsync(Domain.Offer.AggregatesModel.OrganizationalUnit organizationalUnit)
        {
            var result = Builders<Domain.Offer.AggregatesModel.OrganizationalUnit>.Filter.Where(x => x.Id == organizationalUnit.Id);
            var data = Builders<Domain.Offer.AggregatesModel.OrganizationalUnit>.Update.Set(x => x.Name, organizationalUnit.Name);
            await _dbContext.OrganizationalUnitCollection.UpdateOneAsync(result, data);
        }
    }
}
