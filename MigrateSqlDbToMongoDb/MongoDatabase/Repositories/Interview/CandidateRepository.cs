using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace MongoDatabase.Repositories.Interview
{
	public class CandidateRepository 
	{
		private readonly InterviewDbContext _dbContext;

		public CandidateRepository(IConfiguration configuration)
		{
			_dbContext = new InterviewDbContext(configuration);
		}

		public async Task CreateCandidateAsync(Domain.Interview.AggregatesModel.Candidate candidate)
		{
			await _dbContext.CandidateCollection.InsertOneAsync(candidate);
		}

		public async Task<Domain.Interview.AggregatesModel.Candidate> GetCandidateByIdAsync(string candidateId)
		{
			return await _dbContext.CandidateCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == candidateId);
		}

		public async Task UpdateCandidateAsync(Domain.Interview.AggregatesModel.Candidate candidate)
		{
			var filter = Builders<Domain.Interview.AggregatesModel.Candidate>.Filter.Where(x => x.Id == candidate.Id);
			UpdateDefinition<Domain.Interview.AggregatesModel.Candidate> update = Builders<Domain.Interview.AggregatesModel.Candidate>.Update
															.Set(x => x.Email, candidate.Email)
															.Set(x => x.FirstName, candidate.FirstName)
															.Set(x => x.LastName, candidate.LastName);

			await _dbContext.CandidateCollection.UpdateOneAsync(filter, update);
		}
	}
}
