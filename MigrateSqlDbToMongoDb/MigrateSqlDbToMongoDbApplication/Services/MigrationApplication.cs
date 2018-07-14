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
	public class MigrationApplication
	{
		private HrToolv1DbContext hrToolDbContext;
		private CandidateDbContext candidateDbContext;
		private InterviewDbContext interviewDbContext;
		private readonly string cvAttachmentFolderName;
		private readonly string oldHrtoolStoragePath;
		private string organizationalUnitId;
		private readonly string userId;
		private readonly UploadFileFromLink uploadFileFromLink;

		public MigrationApplication(IConfiguration configuration)
		{
			hrToolDbContext = new HrToolv1DbContext(configuration);
			candidateDbContext = new CandidateDbContext(configuration);
			interviewDbContext = new InterviewDbContext(configuration);
			uploadFileFromLink = new UploadFileFromLink(configuration.GetSection("AzureStorage:StorageConnectionString")?.Value);
			cvAttachmentFolderName = configuration.GetSection("AzureStorage:CvAttachmentContainerName")?.Value;
			oldHrtoolStoragePath = configuration.GetSection("OldHrtoolStoragePath")?.Value;
			organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			userId = configuration.GetSection("AdminUser:Id")?.Value;
		}

		public async Task<int> ExecuteAsync()
		{
			var pipeline = candidateDbContext.Pipelines.FirstOrDefault(x => x.OrganizationalUnitId == organizationalUnitId);
			var results = new List<Application>();
			var applications = hrToolDbContext.JobApplications.ToList();
			var totalApplications = 0;
			foreach (var app in applications)
			{
				var ca = GetCandidate(hrToolDbContext, app.CandidateId);
				var jo = (app.JobId is int) ?
					GetJob(hrToolDbContext, (int)app.JobId) : null;
				var isReject = (app.OverallStatus is int) ?
					IsReject((int)app.OverallStatus) : false;

				var currentPipelineStage = (app.OverallStatus is int) ?
					GetPipeline(pipeline, (int)app.OverallStatus) : pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.Sourced);

				var attachments = await GetAttachments(new AttachmentInfo
				{
					ContainerFolder = cvAttachmentFolderName,
					NewApplicationId = app.Id,
					NewCandidateId = ca.Id,
					OldApplicationId = app.ExternalId,
					OldStoragePath = oldHrtoolStoragePath
				});
				var a = new Application
				{
					AppliedDate = app.CreatedDate,
					CandidateId = ca.Id.ToString(),
					IsSentEmail = app.IsSendMail is bool ?
						(bool)app.IsSendMail : false,
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
					},
					Attachments = attachments
				};
				results.Add(a);
				if (!candidateDbContext.Applications.Any(x => x.Id == a.Id))
				{
					await candidateDbContext.ApplicationCollection.InsertOneAsync(a);
				}
				else
				{
					await candidateDbContext.ApplicationCollection.ReplaceOneAsync((x => x.Id == a.Id), a);
				}
				if (!interviewDbContext.Applications.Any(x => x.Id == a.Id))
				{
					await interviewDbContext.ApplicationCollection.InsertOneAsync(new MongoDatabase.Domain.Interview.AggregatesModel.Application
					{
						Id = app.Id.ToString()
					});
				}
				totalApplications++;
			}
			return totalApplications;
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
						return pipeline.Stages.FirstOrDefault(x => x.StageType == StageType.Interviewing);

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

		private async Task<IList<File>> GetAttachments(AttachmentInfo info)
		{
			var results = new List<File>();
			var attachments = hrToolDbContext.JobApplicationAttachments.Where(x => x.JobApplicationId == info.OldApplicationId).Select(x => x);
			foreach (var attachment in attachments)
			{
				var contentType = MimeMapping.MimeUtility.GetMimeMapping(attachment.Filename);
				var newLink = $"{organizationalUnitId}/{info.NewCandidateId.ToString()}/{info.NewApplicationId.ToString()}/{attachment.Filename}";
				var path = await uploadFileFromLink.GetAttachmentPathAsync(
					new Common.Services.Model.AttachmentFileModel
					{
						Name = attachment.Filename,
						Path = info.OldStoragePath + attachment.Path
					}, newLink, info.ContainerFolder);
				var newAttachment = new File
				{
					Id = attachment.Id.ToString(),
					Name = attachment.Filename,
					Path = path
				};
				results.Add(newAttachment);
			}
			return results;
		}

		private class AttachmentInfo
		{
			public int OldApplicationId { get; set; }
			public MongoDB.Bson.ObjectId NewCandidateId { get; set; }
			public MongoDB.Bson.ObjectId NewApplicationId { get; set; }
			public string ContainerFolder { get; set; }
			public string OldStoragePath { get; set; }
		}
	}
}
