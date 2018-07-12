using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDatabase.Domain.Candidate.AggregatesModel;
using System.Linq;
using MigrateSqlDbToMongoDbApplication.Constants;
using System;

namespace MigrateSqlDbToMongoDbApplication.Services
{
	public class MigrationApplication
	{
		public async Task ExecuteAsync(IConfiguration configuration)
		{
			var hrToolDbContext = new HrToolv1DbContext(configuration);
			var candidateDbContext = new CandidateDbContext(configuration);
			var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			var userId = configuration.GetSection("AdminUser:Id")?.Value;
			var results = new List<Application>();
			var applications = hrToolDbContext.JobApplications.ToList();

			foreach (var app in applications)
			{
				var ca = GetCandidate(hrToolDbContext, app.CandidateId);
				var jo = (app.JobId == null) ? null : GetJob(hrToolDbContext, (int)app.JobId);
				var isReject = app.OverallStatus != null ? IsReject((int)app.OverallStatus) : false;
				var a = new Application
				{
					AppliedDate = app.CreatedDate,
					CandidateId = ca.Id.ToString(),
					Id = app.Id.ToString(),
					OrganizationalUnitId = "",
					JobId = jo?.Id.ToString() ?? null,
					IsRejected = isReject,

				};
			}
		}

		private List<Education> GetEducations(HrToolv1DbContext hrToolv1DbContext, object externalId)
		{
			return hrToolv1DbContext.Educations.Where(x => x.CandidateId == externalId).Select(x => new Education
			{
				Id = x.Id.ToString(),
				//FromMonth = x.From != null ? ((DateTime)x.From).Month : null
			}).ToList();
		}


		private bool IsReject(int? status)
		{
			if (status.HasValue)
			{
				switch (status)
				{
					case (int)EnumResult.RejectAll:
					case (int)EnumResult.Rejected:
					case (int)EnumScreeningCv.Rejected:
					case (int)EnumScreeningCv.BlackList:
						return true;
					default: return false;
				}
			}
			return false;
		}

		private MongoDatabaseHrToolv1.Model.Candidate GetCandidate(HrToolv1DbContext hrToolDbContext, int externalId)
		{
			return hrToolDbContext.Candidates.FirstOrDefault(x => x.ExternalId == externalId);
		}

		private MongoDatabaseHrToolv1.Model.Job GetJob(HrToolv1DbContext hrToolDbContext, int externalId)
		{
			return hrToolDbContext.Jobs.FirstOrDefault(x => x.ExternalId == externalId);
		}
	}
}
