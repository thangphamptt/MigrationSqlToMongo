using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;
using System.Linq;
using MongoDatabase.Domain.Job.AggregatesModel;
using System;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
	public class MigrateJob
	{
		public async Task<int> Execute(IConfiguration configuration)
		{
			var hrToolDbContext = new HrToolv1DbContext(configuration);
			var jobDbContext = new JobDbContext(configuration);
			var category = GetCategory(jobDbContext);
			var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			var userId = configuration.GetSection("AdminUser:Id")?.Value;
			var result = new List<Job>();

			var jobs = hrToolDbContext.Jobs.ToList();
			foreach (var job in jobs)
			{
				var st = ConvertStatus(hrToolDbContext, job.ExternalId);
				var template = GetRecruitmentTemplate(hrToolDbContext, job.ExternalId);
				var newJob = new Job
				{
					Id = job.Id.ToString(),
					Name = job.JobTitle,
					Vacancies = job.Quantity,
					OrganizationalUnitId = organizationalUnitId,
					Summary = string.Empty,
					Description = template?.JobDescription,
					Publications = new List<Publication>
					   {
						   new Publication
						   {
							   ExpirationDate = job.EndDate,
							   PublishedDate = job.StartDate
						   }
					   },
					CreatedDate = template?.CreatedDate ?? DateTime.Now,
					JobType = JobType.FullTime,
					PositionLevel = PositionLevel.Experienced,
					Status = ConvertStatus(hrToolDbContext, job.ExternalId),
					CreatedByUserId = userId,
					CategoryIds = new List<string> { category.Id }
				};
				result.Add(newJob);
			}
			//await jobDbContext.JobCollection.InsertManyAsync(jobs);
			
			return jobDbContext.Jobs.Count();
		}

		private Category GetCategory(JobDbContext jobDbContext)
		{
			return jobDbContext.Categories.FirstOrDefault(x => x.Code == "other_others");
		}

		private JobStatus ConvertStatus(HrToolv1DbContext hrToolDbContext, int jobExternalId)
		{
			var st = hrToolDbContext.JobStatuses?.Where(x => x.JobId == jobExternalId)
				.OrderBy(x => x.Id)
				.FirstOrDefault();
			if (st != null)
			{
				switch (st.Status)
				{
					case "0":
						return JobStatus.Draft;
					case "3":
						return JobStatus.Published;
					case "4":
						return JobStatus.Closed;
				}
			}
			return JobStatus.Closed;
		}

		private MongoDatabaseHrToolv1.Model.RecruitmentTemplate GetRecruitmentTemplate(HrToolv1DbContext hrToolDbContext, int jobExternalId)
		{
			return hrToolDbContext.RecruitmentTemplates
				.Where(w => jobExternalId == w.JobId)
				.OrderBy(o => o.ExternalId)
				.FirstOrDefault();
		}
	}
}
