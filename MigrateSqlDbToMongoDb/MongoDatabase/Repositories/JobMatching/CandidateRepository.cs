using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;

namespace MongoDatabase.Repositories.JobMatching
{
	public class CandidateRepository
    {
        private readonly JobMatchingDbContext _dbContext;

        public CandidateRepository(IConfiguration configuration)
		{
			_dbContext = new JobMatchingDbContext(configuration);
		}

		public async Task CreateCandidateAsync(Domain.JobMatching.AggregatesModel.Candidate candidate)
        {
            await _dbContext.CandidateCollection.InsertOneAsync(candidate);
        }

        public async Task<Domain.JobMatching.AggregatesModel.Candidate> GetCandidateByIdAsync(string candidateId)
        {
            return await _dbContext.CandidateCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == candidateId);
        }

        public async Task UpdateCandidateAsync(Domain.JobMatching.AggregatesModel.Candidate candidate)
        {
            var result = Builders<Domain.JobMatching.AggregatesModel.Candidate>.Filter.Where(x => x.Id == candidate.Id);
            var update = Builders<Domain.JobMatching.AggregatesModel.Candidate>.Update
                                                            .Set(x => x.Id, candidate.Id)
                                                            .Set(x => x.OrganizationalUnitId, candidate.OrganizationalUnitId)
                                                            .Set(x => x.UserObjectId, candidate.UserObjectId);
            await _dbContext.CandidateCollection.UpdateOneAsync(result, update);
        }
    }
}
