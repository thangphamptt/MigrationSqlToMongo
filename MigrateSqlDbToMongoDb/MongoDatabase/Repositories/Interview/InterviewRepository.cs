using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System;

namespace MongoDatabase.Repositories.Interview
{
	public class InterviewRepository
	{
		private readonly InterviewDbContext _dbContext;

		public InterviewRepository(IConfiguration configuration)
		{
			_dbContext = new InterviewDbContext(configuration);
		}

		public async Task<Domain.Interview.AggregatesModel.Interview> GetInterviewAsync(string id)
		{
			return await _dbContext.InterviewCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task InsertInterviewAsync(Domain.Interview.AggregatesModel.Interview interview)
		{
			await _dbContext.InterviewCollection.InsertOneAsync(interview);
		}

		public async Task UpdateEmailSentDateAsync(Domain.Interview.AggregatesModel.Interview data)
		{
			var filter = Builders<Domain.Interview.AggregatesModel.Interview>.Filter.Where(x => x.Id == data.Id);
			var update = Builders<Domain.Interview.AggregatesModel.Interview>.Update.Set(x => x.ModifiedDate, DateTime.Now)
														.Set(x => x.ModifiedByUserId, data.ModifiedByUserId)
														.Set(x => x.SentDate, data.SentDate);

			await _dbContext.InterviewCollection.UpdateOneAsync(filter, update);
		}
	}
}
