using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Common.Services;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;
using InterviewDomainModel = MongoDatabase.Domain.Interview.AggregatesModel;
using JobMatchingDomainModel = MongoDatabase.Domain.JobMatching.AggregatesModel;
using OfferDomainModel = MongoDatabase.Domain.Offer.AggregatesModel;
using ScheduleDomainModel = MongoDatabase.Domain.Schedule.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateCandidateService
    {
        private IConfiguration _configuration;
        private UploadFileFromLink uploadFileFromLink;
        private HrToolv1DbContext _hrToolDbContext;
        private CandidateDbContext _candidateDbContext;
        private InterviewDbContext _interviewDbContext;
        private JobMatchingDbContext _jobMatchingDbContext;
        private OfferDbContext _offerDbContext;
        private ScheduleDbContext _scheduleDbContext;

        private string organizationalUnitId;
        private string userId;
        private string profileImageContainerName;
        private string oldHrtoolStoragePath;
        private List<MongoDatabaseHrToolv1.Model.Candidate> candidateData;
        List<MongoDatabaseHrToolv1.Model.JobApplication> applicationsData;

        public MigrateCandidateService(IConfiguration configuration,
            HrToolv1DbContext hrToolDbContext,
            CandidateDbContext candidateDbContext,
            InterviewDbContext interviewDbContext,
            JobMatchingDbContext jobMatchingDbContext,
            OfferDbContext offerDbContext,
            ScheduleDbContext scheduleDbContext)
        {
            _configuration = configuration;
            _hrToolDbContext = hrToolDbContext;
            _candidateDbContext = candidateDbContext;
            _interviewDbContext = interviewDbContext;
            _jobMatchingDbContext = jobMatchingDbContext;
            _offerDbContext = offerDbContext;
            _scheduleDbContext = scheduleDbContext;

            organizationalUnitId = _configuration.GetSection("CompanySetting:Id")?.Value;
            userId = configuration.GetSection("AdminUser:Id")?.Value;
            var azuareStorageConnectionString = _configuration.GetSection("AzureStorage:StorageConnectionString")?.Value;
            uploadFileFromLink = new UploadFileFromLink(azuareStorageConnectionString);
            profileImageContainerName = _configuration.GetSection("AzureStorage:ProfileImageContainerName")?.Value;
            oldHrtoolStoragePath = _configuration.GetSection("OldHrtoolStoragePath")?.Value;
        }

        public async Task ExecuteAsync()
        {
            candidateData = _hrToolDbContext.Candidates.ToList();
            applicationsData = _hrToolDbContext.JobApplications.ToList();

            await MigrateCandidateToCandidateService();
            await MigrateCandidateToInterviewService();
            await MigrateCandidateToJobMatchingService();
            await MigrateCandidateToOfferService();
            await MigrateCandidateToScheduleService();
        }

        private async Task MigrateCandidateToCandidateService()
        {
            try
            {
                Console.WriteLine("Migrate [candidate] to [Candidate service] => Starting...");
                var candidateIdsDestination = _candidateDbContext.Candidates.Select(s => s.Id).ToList();
                var candidatesSource = candidateData
                    .Where(w => !candidateIdsDestination.Contains(w.Id.ToString()))
                    .ToList();

                if (candidatesSource != null && candidatesSource.Count > 0)
                {
                    int count = 0;
                    foreach (var source in candidatesSource)
                    {
                        var applicationIds = applicationsData
                        .Where(x => x.CandidateId == source.ExternalId)
                        .Select(x => x.Id.ToString())
                        .ToList();
                        var data = new CandidateDomainModel.Candidate()
                        {
                            Id = source.Id.ToString(),
                            Firstname = source.FirstName,
                            Lastname = source.LastName,
                            OrganizationalUnitId = organizationalUnitId,
                            PhoneNumber = source.Phone,
                            Email = source.Email,
                            Gender = (CandidateDomainModel.Gender?)ConvertGender(source.Gender),
                            DateOfBirth = !string.IsNullOrEmpty(source.BirthDay.ToString()) ?
                                            Convert.ToDateTime(source.BirthDay) : new DateTime?(),
                            Address = new CandidateDomainModel.Address { City = source.City, StreetAddress = source.Address },
                            CreatedDate = !string.IsNullOrEmpty(source.CreateDate.ToString()) ? (DateTime)source.CreateDate : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-7),
                            ReadByUserIds = new List<string> { userId },
                            ApplicationIds = applicationIds,
                            ProfileImagePath = await UploadProfileImage(source.ImagePath,
                                                          source.Id.ToString(),
                                                          organizationalUnitId,
                                                          profileImageContainerName)
                        };
                        await _candidateDbContext.CandidateCollection.InsertOneAsync(data);

                        count++;
                        Console.Write($"\r {count}/{candidatesSource.Count}");
                    }
                    Console.WriteLine($"\n Migrate [candidate] to [Candidate service] => DONE: inserted {candidatesSource.Count} candidates. \n");
                }
                else
                {
                    Console.WriteLine($"Migrate [candidate] to [Candidate service] => DONE: data exsited. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }            
        }

        private async Task MigrateCandidateToInterviewService()
        {
            Console.WriteLine("Migrate [candidate] to [Interview service] => Starting...");

            var candidateIdsDestination = _interviewDbContext.Candidates.Select(s => s.Id).ToList();
            var candidatesSource = candidateData
                .Where(w => !candidateIdsDestination.Contains(w.Id.ToString()))
                .ToList();
            if (candidatesSource != null && candidatesSource.Count > 0)
            {
                int count = 0;
                foreach (var source in candidatesSource)
                {
                    var applicationIds = applicationsData
                      .Where(x => x.CandidateId == source.ExternalId)
                      .Select(x => x.Id.ToString())
                      .ToList();
                    var data = new InterviewDomainModel.Candidate()
                    {
                        Id = source.Id.ToString(),
                        FirstName = source.FirstName,
                        LastName = source.LastName,
                        Email = source.Email
                    };
                    await _interviewDbContext.CandidateCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{candidatesSource.Count}");
                }
                Console.WriteLine($"\n Migrate [candidate] to [Interview service] => DONE: inserted {candidatesSource.Count} candidates. \n");
            }
            else
            {
                Console.WriteLine($"Migrate [candidate] to [Interview service] => DONE: data exsited. \n");
            }
        }

        private async Task MigrateCandidateToJobMatchingService()
        {
            Console.WriteLine("Migrate [candidate] to [Job Matching service] => Starting...");

            var candidateIdsDestination = _jobMatchingDbContext.Candidates.Select(s => s.Id).ToList();
            var candidatesSource = candidateData
               .Where(w => !candidateIdsDestination.Contains(w.Id.ToString()))
               .ToList();
            if (candidatesSource != null && candidatesSource.Count > 0)
            {
                int count = 0;
                foreach (var source in candidatesSource)
                {
                    var applicationIds = applicationsData
                        .Where(x => x.CandidateId == source.ExternalId)
                        .Select(x => x.Id.ToString())
                        .ToList();
                    var data = new JobMatchingDomainModel.Candidate()
                    {
                        Id = source.Id.ToString(),
                        Email = source.Email,
                        OrganizationalUnitId = organizationalUnitId
                    };

                    await _jobMatchingDbContext.CandidateCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{candidatesSource.Count}");
                }
                Console.WriteLine($"\n Migrate [candidate] to [Job Matching service] => DONE: inserted {candidatesSource.Count} candidates. \n");
            }
            else
            {
                Console.WriteLine($"Migrate [candidate] to [Job Matching service] => DONE: data exsited. \n");
            }
        }

        private async Task MigrateCandidateToOfferService()
        {
            Console.WriteLine("Migrate [candidate] to [Offer service] => Starting...");

            var candidateIdsDestination = _offerDbContext.Candidates.Select(s => s.Id).ToList();
            var candidatesSource = candidateData
               .Where(w => !candidateIdsDestination.Contains(w.Id.ToString()))
               .ToList();
            if (candidatesSource != null && candidatesSource.Count > 0)
            {
                int count = 0;
                foreach (var source in candidatesSource)
                {
                    var applicationIds = applicationsData
                        .Where(x => x.CandidateId == source.ExternalId)
                        .Select(x => x.Id.ToString())
                        .ToList();
                    var data = new OfferDomainModel.Candidate()
                    {
                        Id = source.Id.ToString(),
                        FirstName = source.FirstName,
                        LastName = source.LastName,
                        Email = source.Email
                    };
                    await _offerDbContext.CandidateCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{candidatesSource.Count}");
                }
                Console.WriteLine($"\n Migrate [candidate] to [Offer service] => DONE: inserted {candidatesSource.Count} candidates. \n");
            }
            else
            {
                Console.WriteLine($"Migrate [candidate] to [Offer service] => DONE: data exsited. \n");
            }
        }

        private async Task MigrateCandidateToScheduleService()
        {
            Console.WriteLine("Migrate [candidate] to [Schedule service] => Starting...");

            var candidateIdsDestination = _scheduleDbContext.Candidates.Select(s => s.Id).ToList();
            var candidatesSource = candidateData
                .Where(w => !candidateIdsDestination.Contains(w.Id.ToString()))
                .ToList();
            if (candidatesSource != null && candidatesSource.Count > 0)
            {
                int count = 0;
                foreach (var source in candidatesSource)
                {
                    var applicationIds = applicationsData
                        .Where(x => x.CandidateId == source.ExternalId)
                        .Select(x => x.Id.ToString())
                        .ToList();
                    var data = new ScheduleDomainModel.Candidate()
                    {
                        Id = source.Id.ToString(),
                        FirstName = source.FirstName,
                        LastName = source.LastName,
                        Email = source.Email
                    };
                    await _scheduleDbContext.CandidateCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{candidatesSource.Count}");
                }
                Console.WriteLine($"\n Migrate [candidate] to [Schedule service] => DONE: inserted {candidatesSource.Count} candidates. \n");
            }
            else
            {
                Console.WriteLine($"Migrate [candidate] to [Schedule service] => DONE: data exsited. \n");
            }
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
    }
}
