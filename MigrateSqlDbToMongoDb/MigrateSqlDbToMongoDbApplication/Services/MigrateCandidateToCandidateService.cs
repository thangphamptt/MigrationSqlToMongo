using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Common.Services;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Candidate.AggregatesModel;
using MongoDatabaseHrToolv1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication.Services
{
	public class MigrateCandidateToCandidateService
	{
		private UploadFileFromLink uploadFileFromLink;
		public async Task<int> InsertCandidateToCandidateService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
		{
			var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			uploadFileFromLink = new UploadFileFromLink(configuration.GetSection("AzureStorage:StorageConnectionString")?.Value);
			var profileImageContainerName = configuration.GetSection("AzureStorage:ProfileImageContainerName")?.Value;
			var oldHrtoolStoragePath = configuration.GetSection("OldHrtoolStoragePath")?.Value;
			var candidateDbContext = new CandidateDbContext(configuration);
			var hrToolv1DbContext = new HrToolv1DbContext(configuration);
			int totalCandidates = 0;
			if (candidates != null)
			{
				try
				{
					foreach (var data in candidates)
					{
						var applicationIds = hrToolv1DbContext.JobApplications.Where(x => x.CandidateId == data.ExternalId).ToList().Select(x => x.Id.ToString()).ToList();
						if (!candidateDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
						{
							var candidate = new MongoDatabase.Domain.Candidate.AggregatesModel.Candidate()
							{
								Id = data.Id.ToString(),
								Firstname = data.FirstName,
								Lastname = data.LastName,
								OrganizationalUnitId = organizationalUnitId,
								PhoneNumber = data.Phone,
								Email = data.Email,
								Gender = (Gender?)ConvertGender(data.Gender),
								DateOfBirth = !string.IsNullOrEmpty(data.BirthDay.ToString()) ? Convert.ToDateTime(data.BirthDay) :
								new DateTime?(),
								Address = new Address { City = data.City, StreetAddress = data.Address },
								CreatedDate = !string.IsNullOrEmpty(data.CreateDate.ToString()) ? (DateTime)data.CreateDate : DateTime.Now,
								ApplicationIds = applicationIds,
								ProfileImagePath = await UploadProfileImage(data.ImagePath,
																data.Id.ToString(),
																organizationalUnitId,
																profileImageContainerName)
							};
							await candidateDbContext.CandidateCollection.InsertOneAsync(candidate);
							totalCandidates++;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			return totalCandidates;
		}

		private async Task<string> UploadProfileImage(string path, string candidateId, string organizationalUnitId, string profileImageContainerName)
		{
			if (string.IsNullOrWhiteSpace(path)) return path;
			var newPath = $"{organizationalUnitId}/{candidateId}/{0}";
			var files = path.Replace("\\", "/").Split("/");
			return await uploadFileFromLink.GetAttachmentPathAsync(
				new Common.Services.Model.AttachmentFileModel
				{
					Name = files[files.Length - 1],
					Path = path
				},
				newPath,
				profileImageContainerName);
		}

		public async Task<int> InsertCandidateToInterviewService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
		{
			var interviewDbContext = new InterviewDbContext(configuration);
			int totalCandidates = 0;
			if (candidates != null)
			{
				try
				{
					foreach (var data in candidates)
					{
						if (!interviewDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
						{
							var candidate = new MongoDatabase.Domain.Interview.AggregatesModel.Candidate()
							{
								Id = data.Id.ToString(),
								FirstName = data.FirstName,
								LastName = data.LastName,
								Email = data.Email
							};
							await interviewDbContext.CandidateCollection.InsertOneAsync(candidate);
							totalCandidates++;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			return totalCandidates;
		}

		public async Task<int> InsertCandidateToJobService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
		{
			var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			var jobDbContext = new JobDbContext(configuration);
			int totalCandidates = 0;
			if (candidates != null)
			{
				try
				{
					foreach (var data in candidates)
					{
						if (!jobDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
						{
							var candidate = new MongoDatabase.Domain.Job.AggregatesModel.Candidate()
							{
								Id = data.Id.ToString(),
								FirstName = data.FirstName,
								LastName = data.LastName,
								Email = data.Email,
								OrganizationalUnitId = organizationalUnitId
							};
							await jobDbContext.CandidateCollection.InsertOneAsync(candidate);
							totalCandidates++;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			return totalCandidates;
		}

		public async Task<int> InsertCandidateToJobMatchingService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
		{
			var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			var jobMatchingDbContext = new JobMatchingDbContext(configuration);
			int totalCandidates = 0;
			if (candidates != null)
			{
				try
				{
					foreach (var data in candidates)
					{
						if (!jobMatchingDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
						{
							var candidate = new MongoDatabase.Domain.JobMatching.AggregatesModel.Candidate()
							{
								Id = data.Id.ToString(),
								Email = data.Email,
								OrganizationalUnitId = organizationalUnitId
							};
							await jobMatchingDbContext.CandidateCollection.InsertOneAsync(candidate);
							totalCandidates++;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			return totalCandidates;
		}

		public async Task<int> InsertCandidateToOfferService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
		{
			var offerDbContext = new OfferDbContext(configuration);
			int totalCandidates = 0;
			if (candidates != null)
			{
				try
				{
					foreach (var data in candidates)
					{
						if (!offerDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
						{
							var candidate = new MongoDatabase.Domain.Offer.AggregatesModel.Candidate()
							{
								Id = data.Id.ToString(),
								FirstName = data.FirstName,
								LastName = data.LastName,
								Email = data.Email
							};
							await offerDbContext.CandidateCollection.InsertOneAsync(candidate);
							totalCandidates++;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			return totalCandidates;
		}

		public async Task<int> InsertCandidateToScheduleService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
		{
			var scheduleDbContext = new ScheduleDbContext(configuration);
			int totalCandidates = 0;
			if (candidates != null)
			{
				try
				{
					foreach (var data in candidates)
					{
						if (!scheduleDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
						{
							var candidate = new MongoDatabase.Domain.Schedule.AggregatesModel.Candidate()
							{
								Id = data.Id.ToString(),
								FirstName = data.FirstName,
								LastName = data.LastName,
								Email = data.Email
							};
							await scheduleDbContext.CandidateCollection.InsertOneAsync(candidate);
							totalCandidates++;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			return totalCandidates;
		}

		private int? ConvertGender(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				switch (value.ToLower())
				{
					case "male":
						return 0;
					case "female":
						return 1;
				}
			}
			return null;
		}
	}
}
