using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication.Services
{
	public class MigrateInterview
    {
		private readonly InterviewDbContext interviewDbContext;
		private readonly HrToolv1DbContext hrToolDbContext;
		private readonly string organizationalUnitId;
		private readonly string userId;

		public MigrateInterview(IConfiguration configuration)
		{
			hrToolDbContext = new HrToolv1DbContext(configuration);
			interviewDbContext = new InterviewDbContext(configuration);
			organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			userId = configuration.GetSection("AdminUser:Id")?.Value;
		}

		public async Task ExecuteAsync()
		{
			//var interviews = hrToolDbContext.In
		}
    }
}
