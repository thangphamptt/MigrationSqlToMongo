using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;

namespace MongoDatabase.Repositories.Offer
{
	public class OfferEmailTemplateRepository
	{
		private OfferDbContext _dbContext;

		public OfferEmailTemplateRepository(IConfiguration configuration)
		{
			_dbContext = new OfferDbContext(configuration);
		}

		public async Task CreateOfferEmailTemplateAsync(Domain.Offer.AggregatesModel.OfferEmailTemplate offerEmailTemplate)
        {
            await _dbContext.OfferEmailTemplateCollection.InsertOneAsync(offerEmailTemplate);
        }

        public async Task<Domain.Offer.AggregatesModel.OfferEmailTemplate> GetOfferEmailTemplateByIdAsync(string id)
        {
            return await _dbContext.OfferEmailTemplateCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
