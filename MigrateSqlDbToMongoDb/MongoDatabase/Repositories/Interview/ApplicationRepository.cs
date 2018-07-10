using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;

namespace MongoDatabase.Repositories.Interview
{
	public class ApplicationRepository 
    {
        private readonly InterviewDbContext _dbContext;

        public ApplicationRepository(IConfiguration configuration)
		{
			_dbContext = new InterviewDbContext(configuration);
		}

		public async Task CreateApplicationAsync(Domain.Interview.AggregatesModel.Application application)
        {
            await _dbContext.ApplicationCollection.InsertOneAsync(application);
        }
    }
}
