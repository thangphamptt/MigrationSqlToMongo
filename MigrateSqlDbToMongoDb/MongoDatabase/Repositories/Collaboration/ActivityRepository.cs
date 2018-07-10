using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Collaboration.AggregatesModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Collaboration
{
	public class ActivityRepository
    {
        private readonly CollaborationDbContext _dbContext;

        public ActivityRepository(IConfiguration configuration)
		{
			_dbContext = new CollaborationDbContext(configuration);
		}

		public async Task CreateActivityAsync(Activity activityCommand)
        {
            await _dbContext.ActivityCollection.InsertOneAsync(activityCommand);
        }

        public async Task UpdateAgreedUserIdsAsync(string activityId, IList<string> agreedUserIds)
        {
            var filter = Builders<Activity>.Filter.Where(t => t.Id == activityId);
            var update = Builders<Activity>.Update
                                        .Set(t => t.AgreedUserIds, agreedUserIds);
            await _dbContext.ActivityCollection.UpdateOneAsync(filter, update);
        }

        public async Task<Activity> GetActivityByIdAsync(string activityId)
        {
            return await _dbContext.ActivityCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == activityId);
        }

        public async Task EditActivityTextAsync(string activityId, string text)
        {
            var filter = Builders<Activity>.Filter.Where(x => activityId == x.Id);

            var update = Builders<Activity>.Update
                                .Set(x => x.Text, text)
                                .Set(x => x.ModifiedDate, DateTime.Now);

            await _dbContext.ActivityCollection.UpdateOneAsync(filter, update);
        }

		public async Task CreateActivitiesAsync(IList<Activity> activities)
		{
			await _dbContext.ActivityCollection.InsertManyAsync(activities);
		}
	}
}
