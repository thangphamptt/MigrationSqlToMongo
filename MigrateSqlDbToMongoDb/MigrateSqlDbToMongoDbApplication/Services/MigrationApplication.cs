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
		private HrToolv1DbContext hrToolDbContext;
		private CandidateDbContext candidateDbContext;
		private InterviewDbContext interviewDbContext;
		private string organizationalUnitId;
		private readonly string userId;

		public MigrationApplication(IConfiguration configuration)
		{
			hrToolDbContext = new HrToolv1DbContext(configuration);
			candidateDbContext = new CandidateDbContext(configuration);
			interviewDbContext = new InterviewDbContext(configuration);
			organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			userId = configuration.GetSection("AdminUser:Id")?.Value;
		}

		public void Execute()
		{
			var pipeline = candidateDbContext.Pipelines.FirstOrDefault(x => x.OrganizationalUnitId == organizationalUnitId);
			var results = new List<MongoDatabase.Domain.Candidate.AggregatesModel.Application>();
			var applications = hrToolDbContext.JobApplications.ToList();

			foreach (var app in applications)
			{
				var ca = GetCandidate(hrToolDbContext, app.CandidateId);
				var jo = (app.JobId is int) ? GetJob(hrToolDbContext, (int)app.JobId) : null;
				var isReject = app.OverallStatus != null ? IsReject((int)app.OverallStatus) : false;
				var currentPipelineStage = GetPipeline(pipeline, (int)app.OverallStatus);
				var a = new MongoDatabase.Domain.Candidate.AggregatesModel.Application
				{
					AppliedDate = app.CreatedDate,
					CandidateId = ca.Id.ToString(),
					Id = app.Id.ToString(),
					OrganizationalUnitId = organizationalUnitId,
					JobId = jo?.Id.ToString() ?? null,
					IsRejected = isReject,
					CurrentPipelineStage = new CurrentPipelineStage
					{
						PipelineId = pipeline.Id,
						PipelineStageId = currentPipelineStage.Id,
						PipelineStageName = currentPipelineStage.Name
					},
					CV = new CV
					{
						Education = GetEducations(hrToolDbContext, ca.ExternalId),
						Projects = GetProjects(hrToolDbContext, ca.ExternalId),
						Skills = GetSkills(hrToolDbContext, ca.ExternalId),
						WorkExperiences = GetWorkExperiences(hrToolDbContext, ca.ExternalId)
					}
				};
				results.Add(a);
				if (!candidateDbContext.Applications.Any(x => x.Id == a.Id))
				{
					candidateDbContext.ApplicationCollection.InsertOne(a);
				}
				if (!interviewDbContext.Applications.Any(x => x.Id == a.Id))
				{
					interviewDbContext.ApplicationCollection.InsertOneAsync(new MongoDatabase.Domain.Interview.AggregatesModel.Application
					{
						Id = app.Id.ToString()
					});
				}
			}
		}

		private PipelineStage GetPipeline(Pipeline pipeline, int? status)
		{
			if (status.HasValue)
			{
				switch (status)
				{
					case (int)EnumResult.New:
					case (int)EnumResult.RejectAll:
					case (int)EnumResult.Rejected:
					case (int)EnumScreeningCv.Rejected:
					case (int)EnumScreeningCv.BlackList:
						return pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.New);

					case (int)EnumResult.OnHold:
						return pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.Lead);

					case (int)EnumResult.Open:
					case (int)EnumScreeningCv.Open:
					case (int)EnumResult.Passed:
					case (int)EnumScreeningCv.Passed:
						return pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.Lead);

					case (int)EnumFirstInterview.New:
					case (int)EnumFirstInterview.Passed:
					case (int)EnumFirstInterview.Rejected:

					case (int)EnumSecondInterview.New:
					case (int)EnumSecondInterview.Passed:
					case (int)EnumSecondInterview.Rejected:

					case (int)EnumThirdInterview.New:
					case (int)EnumThirdInterview.Passed:
					case (int)EnumThirdInterview.Rejected:
						return pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.Interviewing);

					case (int)EnumOfferStatus.AcceptOffer:
					case (int)EnumOfferStatus.AlreadySent:
					case (int)EnumOfferStatus.HasOffer:
					case (int)EnumOfferStatus.RejectOffer:
						return pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.Offered);

					case (int)EnumResult.Shortlist:
						return pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.Shortlisted);

					case (int)EnumBecomeEmployee.Completed:
						return pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.Hired);

				}
			}
			return pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.New);
		}

		private List<Education> GetEducations(HrToolv1DbContext hrToolv1DbContext, object externalId)
		{
			return hrToolv1DbContext.Educations.Where(x => x.CandidateId == externalId).ToList().Select(x => MapEducation(x)).ToList();
		}

		private List<Project> GetProjects(HrToolv1DbContext hrToolv1DbContext, object externalId)
		{
			return hrToolv1DbContext.OutstandingProjects.Where(x => x.CandidateId == externalId).ToList().Select(x => MapProject(x)).ToList();
		}

		private List<Skill> GetSkills(HrToolv1DbContext hrToolv1DbContext, object externalId)
		{
			return hrToolv1DbContext.Skills.Where(x => x.CandidateId == externalId).ToList().Select(x => MapSkill(x)).ToList();
		}

		private List<WorkExperience> GetWorkExperiences(HrToolv1DbContext hrToolv1DbContext, object externalId)
		{
			return hrToolv1DbContext.EmploymentHistories.Where(x => x.CandidateId == externalId).ToList().Select(x => MapWorkExperience(x)).ToList();
		}

		private WorkExperience MapWorkExperience(MongoDatabaseHrToolv1.Model.EmploymentHistory data)
		{
			var FromMonth = new int?();
			var FromYear = new int?();
			var ToMonth = new int?();
			var ToYear = new int?();
			if (data.StartTime is DateTime)
			{
				FromMonth = ((DateTime)data.StartTime).Month;
				FromYear = ((DateTime)data.StartTime).Year;
			}
			if (data.EndTime is DateTime)
			{
				ToMonth = ((DateTime)data.EndTime).Month;
				ToYear = ((DateTime)data.EndTime).Year;
			}
			return new WorkExperience
			{
				Id = data.Id.ToString(),
				Company = data.Company,
				Description = data.Responsibilities,
				FromMonth = FromMonth,
				FromYear = FromYear,
				Title = data.Position,
				ToMonth = ToMonth,
				ToYear = ToYear
			};
		}

		private Skill MapSkill(MongoDatabaseHrToolv1.Model.Skill data)
		{
			return new Skill
			{
				Id = data.Id.ToString(),
				Name = data.SkillName
			};
		}

		private Project MapProject(MongoDatabaseHrToolv1.Model.OutstandingProject data)
		{
			var FromMonth = new int?();
			var FromYear = new int?();
			var ToMonth = new int?();
			var ToYear = new int?();
			if (data.StartTime is DateTime)
			{
				FromMonth = ((DateTime)data.StartTime).Month;
				FromYear = ((DateTime)data.StartTime).Year;
			}
			if (data.EndTime is DateTime)
			{
				ToMonth = ((DateTime)data.EndTime).Month;
				ToYear = ((DateTime)data.EndTime).Year;
			}
			return new Project
			{
				FromMonth = FromMonth,
				FromYear = FromYear,
				Id = data.Id.ToString(),
				Name = data.ProjectName,
				Position = data.Position,
				ToMonth = ToMonth,
				ToYear = ToYear
			};
		}

		private Education MapEducation(MongoDatabaseHrToolv1.Model.Education data)
		{
			var FromMonth = new int?();
			var FromYear = new int?();
			var ToMonth = new int?();
			var ToYear = new int?();
			if (data.From is DateTime)
			{
				FromMonth = ((DateTime)data.From).Month;
				FromYear = ((DateTime)data.From).Year;
			}
			if (data.To is DateTime)
			{
				ToMonth = ((DateTime)data.To).Month;
				ToYear = ((DateTime)data.To).Year;
			}
			return new Education
			{
				Id = data.Id.ToString(),
				FromMonth = FromMonth,
				FromYear = FromYear,
				ToMonth = ToMonth,
				ToYear = ToYear,
				School = data.School,
				Degree = data.SchoolLevel
			};
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
