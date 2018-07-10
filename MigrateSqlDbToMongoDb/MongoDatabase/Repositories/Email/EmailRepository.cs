using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Email.AggregatesModel;
using MongoDB.Driver;

namespace Email.Persistance.Repositories
{
	public class EmailRepository
    {
        private readonly EmailDbContext _dbContext;

        public EmailRepository(IConfiguration configuration)
		{
			_dbContext = new EmailDbContext(configuration);
		}

		public async Task CreateInterviewEmailAsync(InterviewEmail interviewEmail)
		{
			await _dbContext.EmailCollection.InsertOneAsync(interviewEmail);
		}

		public async Task CreateOfferEmailAsync(OfferEmail offerEmail)
		{
			await _dbContext.EmailCollection.InsertOneAsync(offerEmail);
		}

		public async Task UpdateOfferEmailAttachmentsAsync(string id, IList<Attachment> attachments)
		{
			var filter = Builders<OfferEmail>.Filter.Where(x => x.Id == id);
			var update = Builders<OfferEmail>.Update.Set(x => x.Attachments, attachments);

			await _dbContext.EmailCollection.OfType<OfferEmail>().UpdateOneAsync(filter, update);
		}

		public async Task UpdateInterviewEmailAttachmentsAsync(string id, IList<Attachment> attachments)
		{
			var filter = Builders<InterviewEmail>.Filter.Where(x => x.Id == id);
			var update = Builders<InterviewEmail>.Update.Set(x => x.Attachments, attachments);

			await _dbContext.EmailCollection.OfType<InterviewEmail>().UpdateOneAsync(filter, update);
		}

        public async Task CreateThankyouEmailAsync(ThankyouEmail thankyouEmail)
        {
            await _dbContext.EmailCollection.InsertOneAsync(thankyouEmail);
        }

        public async Task UpdateThankyouEmailAttachmentsAsync(string id, IList<Attachment> attachments)
        {
            var filter = Builders<ThankyouEmail>.Filter.Where(x => x.Id == id);
            var update = Builders<ThankyouEmail>.Update.Set(x => x.Attachments, attachments);

            await _dbContext.EmailCollection.OfType<ThankyouEmail>().UpdateOneAsync(filter, update);
        }
    }
}
