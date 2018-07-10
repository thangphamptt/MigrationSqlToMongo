using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;

namespace MongoDatabase.Repositories.Schedule
{
	public class CandidateRepository
	{
		private readonly ScheduleDbContext _dbContext;

		public CandidateRepository(IConfiguration configuration)
		{
			_dbContext = new ScheduleDbContext(configuration);
		}

		public async Task CreateCandidateAsync(Domain.Schedule.AggregatesModel.Candidate candidate)
		{
			await _dbContext.CandidateCollection.InsertOneAsync(candidate);
		}

        public async Task<Domain.Schedule.AggregatesModel.Candidate> GetCandidateByIdAsync(string candidateId)
		{
			return await _dbContext.CandidateCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == candidateId);
		}

		public async Task UpdateCandidateAsync(Domain.Schedule.AggregatesModel.Candidate candidate)
		{
			var filter = Builders<Domain.Schedule.AggregatesModel.Candidate>.Filter.Where(x => x.Id == candidate.Id);
			UpdateDefinition<Domain.Schedule.AggregatesModel.Candidate> update = Builders<Domain.Schedule.AggregatesModel.Candidate>.Update
															.Set(x => x.Email, candidate.Email)
															.Set(x => x.FirstName, candidate.FirstName)
															.Set(x => x.LastName, candidate.LastName);

			await _dbContext.CandidateCollection.UpdateOneAsync(filter, update);
		}
	}
}
