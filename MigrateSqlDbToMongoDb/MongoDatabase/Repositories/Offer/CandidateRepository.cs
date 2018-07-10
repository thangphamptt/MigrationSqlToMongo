using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.Offer
{
	public class CandidateRepository
	{
		private OfferDbContext _dbContext;

		public CandidateRepository(IConfiguration configuration)
        {
            _dbContext = new OfferDbContext(configuration);
        }

        public async Task CreateCandidateAsync(Domain.Offer.AggregatesModel.Candidate candidate)
        {
            await _dbContext.CandidateCollection.InsertOneAsync(candidate);
        }

        public async Task<Domain.Offer.AggregatesModel.Candidate> GetCandidateByIdAsync(string candidateId)
        {
            return await _dbContext.CandidateCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == candidateId);
        }
    }
}
