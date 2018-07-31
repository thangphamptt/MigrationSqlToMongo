using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Offer
{
	public class OfferRepository
	{
		private OfferDbContext _dbContext;

		public OfferRepository(IConfiguration configuration)
		{
			_dbContext = new OfferDbContext(configuration);
		}

		public async Task<Domain.Offer.AggregatesModel.Offer> GetOfferByIdAsync(string offerId)
        {
            return await _dbContext.OfferCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == offerId);
        }

        public async Task CreateOfferAsync(Domain.Offer.AggregatesModel.Offer offer)
        {
            await _dbContext.OfferCollection.InsertOneAsync(offer);
        }

        public async Task UpdateOfferAsync(Domain.Offer.AggregatesModel.Offer offer)
        {
            var result = Builders<Domain.Offer.AggregatesModel.Offer>.Filter.Where(x => x.Id == offer.Id);
            var data = Builders<Domain.Offer.AggregatesModel.Offer>.Update.Set(x => x.ReportTo, offer.ReportTo)
                                                  .Set(x => x.Position, offer.Position)
                                                  .Set(x => x.Salary, offer.Salary)
                                                  .Set(x => x.CurrencyId, offer.CurrencyId)
                                                  .Set(x => x.StartDate, offer.StartDate)
                                                  .Set(x => x.IsUpdated, offer.IsUpdated)
                                                  .Set(x => x.ModifiedDate, offer.ModifiedDate)
                                                  .Set(x => x.ModifiedByUserId, offer.ModifiedByUserId)
                                                  .Set(x => x.ExpirationDate, offer.ExpirationDate);

            await _dbContext.OfferCollection.UpdateOneAsync(result, data);
        }

        public async Task UpdateOfferAfterSendMailAsync(Domain.Offer.AggregatesModel.Offer offer)
        {
            var result = Builders<Domain.Offer.AggregatesModel.Offer>.Filter.Where(x => x.Id == offer.Id);
            var data = Builders<Domain.Offer.AggregatesModel.Offer>.Update.Set(x => x.Status, null)
                                                  .Set(x => x.IsUpdated, false)
                                                  .Set(x => x.SentDate, offer.SentDate)
                                                  .Set(x => x.SentByUserId, offer.SentByUserId);
            await _dbContext.OfferCollection.UpdateOneAsync(result, data);
        }

        public async Task UpdateOfferStatusAsync(Domain.Offer.AggregatesModel.Offer offer)
        {
            var result = Builders<Domain.Offer.AggregatesModel.Offer>.Filter.Where(x => x.Id == offer.Id);
            var data = Builders<Domain.Offer.AggregatesModel.Offer>.Update.Set(x => x.Status, offer.Status)
                                                  .Set(x => x.ModifiedByUserId, offer.ModifiedByUserId)
                                                  .Set(x => x.OrganizationalUnitId, offer.OrganizationalUnitId)
                                                  .Set(x => x.IsUpdated, offer.IsUpdated)
                                                  .Set(x => x.ModifiedDate, offer.ModifiedDate)
                                                  .Set(x => x.ModifiedByUserId, offer.ModifiedByUserId);

            await _dbContext.OfferCollection.UpdateOneAsync(result, data);
        }
    }
}
