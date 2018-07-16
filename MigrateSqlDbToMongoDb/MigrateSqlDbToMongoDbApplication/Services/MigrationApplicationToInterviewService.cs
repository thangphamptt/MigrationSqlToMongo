using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDatabase.Domain.Candidate.AggregatesModel;
using System.Linq;
using MigrateSqlDbToMongoDbApplication.Constants;
using System;
using MigrateSqlDbToMongoDbApplication.Common.Services;
using MongoDB.Driver;

namespace MigrateSqlDbToMongoDbApplication.Services
{
	public class MigrationApplicationToInterviewService
	{
		private HrToolv1DbContext hrToolDbContext;
		private InterviewDbContext interviewDbContext;

		public MigrationApplicationToInterviewService(IConfiguration configuration)
		{
			hrToolDbContext = new HrToolv1DbContext(configuration);
			interviewDbContext = new InterviewDbContext(configuration);
		}

		public async Task<int> ExecuteAsync()
		{
			var applications = hrToolDbContext.JobApplications.ToList();
			var totalApplications = 0;
			foreach (var app in applications)
			{
				if (!interviewDbContext.Applications.Any(x => x.Id == app.Id.ToString()))
				{
					await interviewDbContext.ApplicationCollection.InsertOneAsync(new MongoDatabase.Domain.Interview.AggregatesModel.Application
					{
						Id = app.Id.ToString()
					});
					totalApplications++;
				}
			}
			return totalApplications;
		}
	}
}
