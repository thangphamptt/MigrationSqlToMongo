using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Common.Services;
using MigrateSqlDbToMongoDbApplication.Constants;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;
using InterviewDomainModel = MongoDatabase.Domain.Interview.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateApplicationService
    {
        private HrToolv1DbContext _hrToolDbContext;
        private CandidateDbContext _candidateDbContext;
        private InterviewDbContext _interviewDbContext;

        private IConfiguration _configuration;
        private UploadFileFromLink uploadFileFromLink;
        private string organizationalUnitId;
        private List<MongoDatabaseHrToolv1.Model.JobApplication> jobApplicationsData;
        private string cvAttachmentFolderName;
        private string oldHrtoolStoragePath;
        private string userId;

        public MigrateApplicationService(IConfiguration configuration,
            HrToolv1DbContext hrToolDbContext,
            CandidateDbContext candidateDbContext,
            InterviewDbContext interviewDbContext)
        {
            _configuration = configuration;
            _hrToolDbContext = hrToolDbContext;
            _candidateDbContext = candidateDbContext;
            _interviewDbContext = interviewDbContext;

            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var azureStoregeConnectionString = configuration.GetSection("AzureStorage:StorageConnectionString")?.Value;
            uploadFileFromLink = new UploadFileFromLink(azureStoregeConnectionString);
            jobApplicationsData = _hrToolDbContext.JobApplications.ToList();
            cvAttachmentFolderName = configuration.GetSection("AzureStorage:CvAttachmentContainerName")?.Value;
            oldHrtoolStoragePath = configuration.GetSection("OldHrtoolStoragePath")?.Value;
            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            userId = configuration.GetSection("AdminUser:Id")?.Value;
        }

        public async Task ExecuteAsync()
        {
            await MigrateApplicationToCandidateService();
            await MigrateApplicationToInterviewService();
        }

        private async Task MigrateApplicationToCandidateService()
        {
            Console.WriteLine("Migrate [application] to [Candidate service] => Starting...");
            try
            {
                var applicationsJobNotNullOrEmpty = jobApplicationsData.Where(w => !string.IsNullOrEmpty(w.JobId?.ToString())).ToList();
                var applicationsJobNullOrEmpty = jobApplicationsData.Where(w => string.IsNullOrEmpty(w.JobId?.ToString())).ToList();
                if (applicationsJobNullOrEmpty != null)
                {
                    var applications = applicationsJobNullOrEmpty?.Distinct(new ApplicationCandidateComparer()).ToList();
                    if (applications != null)
                    {
                        applicationsJobNotNullOrEmpty?.AddRange(applications);
                    }
                }

                var jobApplicationIdsDestination = _candidateDbContext.Applications.Select(s => s.Id).ToList();
                var applicationSource = applicationsJobNotNullOrEmpty
                    .Where(w => !jobApplicationIdsDestination.Contains(w.Id.ToString())).ToList();
                if (applicationSource != null && applicationSource.Count > 0)
                {
                    var pipeline = _candidateDbContext.Pipelines
                        .FirstOrDefault(x => x.OrganizationalUnitId == organizationalUnitId);
                    if (pipeline == null)
                    {
                        Console.WriteLine($"Migrate [application] to [Candidate service] => FAIL: Pipeline is NULL . \n");
                    }
                    else
                    {
                        int count = 0;
                        foreach (var application in applicationSource)
                        {
                            var candidate = GetCandidate(application.CandidateId);
                            var job = (application.JobId is int) ? GetJob((int)application.JobId) : null;
                            var isReject = (application.OverallStatus is int) ? IsReject((int)application.OverallStatus) : false;

                            var currentPipelineStage = (application.OverallStatus is int) ?
                                GetPipelineCandidateDomain(pipeline, (int)application.OverallStatus) :
                                pipeline.Stages.FirstOrDefault(x => x.StageType == CandidateDomainModel.StageType.Sourced);

                            var attachments = await GetAttachmentsCandidateDomain(new AttachmentInfo
                            {
                                ContainerFolder = cvAttachmentFolderName,
                                NewApplicationId = application.Id,
                                NewCandidateId = candidate.Id,
                                OldApplicationId = application.ExternalId,
                                OldStoragePath = oldHrtoolStoragePath
                            });
                            var data = new CandidateDomainModel.Application
                            {
                                AppliedDate = application.CreatedDate,
                                CandidateId = candidate.Id.ToString(),
                                IsSentEmail = application.IsSendMail is bool ?
                                    (bool)application.IsSendMail : false,
                                Id = application.Id.ToString(),
                                OrganizationalUnitId = organizationalUnitId,
                                JobId = job?.Id.ToString() ?? null,
                                IsRejected = isReject,
                                CurrentPipelineStage = new CandidateDomainModel.CurrentPipelineStage
                                {
                                    PipelineId = pipeline.Id,
                                    PipelineStageId = currentPipelineStage.Id,
                                    PipelineStageName = currentPipelineStage.Name
                                },
                                CV = new CandidateDomainModel.CV
                                {
                                    Education = GetEducationsCandidateDomain(candidate.ExternalId),
                                    Projects = GetProjectsCandidateDomain(candidate.ExternalId),
                                    Skills = GetSkillsCandidateDomain(candidate.ExternalId),
                                    WorkExperiences = GetWorkExperiencesCandidateDomain(candidate.ExternalId)
                                },
                                Attachments = attachments
                            };
                            await _candidateDbContext.ApplicationCollection.InsertOneAsync(data);

                            count++;
                            Console.Write($"\r {count}/{applicationSource.Count}");

                        }
                        Console.WriteLine($"\n Migrate [application] to [Candidate service] => DONE: inserted {applicationSource.Count} applications. \n");
                    }
                }
                else
                {
                    Console.WriteLine($"Migrate [application] to [Candidate service] => DONE: data exsited. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task MigrateApplicationToInterviewService()
        {
            try
            {
                Console.WriteLine("Migrate [application] to [Interview service] => Starting...");
                var applicationsJobNotNullOrEmpty = jobApplicationsData.Where(w => !string.IsNullOrEmpty(w.JobId?.ToString())).ToList();
                var applicationsJobNullOrEmpty = jobApplicationsData.Where(w => string.IsNullOrEmpty(w.JobId?.ToString())).ToList();
                if (applicationsJobNullOrEmpty != null)
                {
                    var applications = applicationsJobNullOrEmpty?.Distinct(new ApplicationCandidateComparer()).ToList();
                    if (applications != null)
                    {
                        applicationsJobNotNullOrEmpty?.AddRange(applications);
                    }
                }

                var jobApplicationIdsDestination = _interviewDbContext.Applications.Select(s => s.Id).ToList();
                var applicationSource = applicationsJobNotNullOrEmpty
                    .Where(w => !jobApplicationIdsDestination.Contains(w.Id.ToString())).ToList();

                if (applicationSource != null && applicationSource.Count > 0)
                {
                    int count = 0;
                    foreach (var application in applicationSource)
                    {
                        var data = new InterviewDomainModel.Application
                        {
                            Id = application.Id.ToString()

                        };

                        await _interviewDbContext.ApplicationCollection.InsertOneAsync(data);

                        count++;
                        Console.Write($"\r {count}/{applicationSource.Count}");
                    }
                    Console.WriteLine($"\n Migrate [application] to [Interview service] => DONE: inserted {applicationSource.Count} applications. \n");

                }
                else
                {
                    Console.WriteLine($"Migrate [application] to [Interview service] => DONE: data exsited. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        #region Candidate Domain Helper

        private MongoDatabaseHrToolv1.Model.Candidate GetCandidate(int externalId)
        {
            return _hrToolDbContext.Candidates.FirstOrDefault(x => x.ExternalId == externalId);
        }

        private MongoDatabaseHrToolv1.Model.Job GetJob(int externalId)
        {
            return _hrToolDbContext.Jobs.FirstOrDefault(x => x.ExternalId == externalId);
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

        private CandidateDomainModel.PipelineStage GetPipelineCandidateDomain(CandidateDomainModel.Pipeline pipeline, int? status)
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
                        return pipeline.Stages.FirstOrDefault(x => x.StageType == CandidateDomainModel.StageType.New);

                    case (int)EnumResult.OnHold:
                        return pipeline.Stages.FirstOrDefault(x => x.StageType == CandidateDomainModel.StageType.Lead);

                    case (int)EnumResult.Open:
                    case (int)EnumScreeningCv.Open:
                    case (int)EnumResult.Passed:
                    case (int)EnumScreeningCv.Passed:
                        return pipeline.Stages.FirstOrDefault(x => x.StageType == CandidateDomainModel.StageType.Lead);

                    case (int)EnumFirstInterview.New:
                    case (int)EnumFirstInterview.Passed:
                    case (int)EnumFirstInterview.Rejected:

                    case (int)EnumSecondInterview.New:
                    case (int)EnumSecondInterview.Passed:
                    case (int)EnumSecondInterview.Rejected:

                    case (int)EnumThirdInterview.New:
                    case (int)EnumThirdInterview.Passed:
                    case (int)EnumThirdInterview.Rejected:
                        return pipeline.Stages.FirstOrDefault(x => x.StageType == CandidateDomainModel.StageType.Interviewing);

                    case (int)EnumOfferStatus.AcceptOffer:
                    case (int)EnumOfferStatus.AlreadySent:
                    case (int)EnumOfferStatus.HasOffer:
                    case (int)EnumOfferStatus.RejectOffer:
                        return pipeline.Stages.FirstOrDefault(x => x.StageType == CandidateDomainModel.StageType.Offered);

                    case (int)EnumResult.Shortlist:
                        return pipeline.Stages.FirstOrDefault(x => x.StageType == CandidateDomainModel.StageType.Interviewing);

                    case (int)EnumBecomeEmployee.Completed:
                        return pipeline.Stages.FirstOrDefault(x => x.StageType == CandidateDomainModel.StageType.Hired);
                }
            }
            return pipeline.Stages.FirstOrDefault(x => x.StageType == CandidateDomainModel.StageType.New);
        }

        private async Task<IList<CandidateDomainModel.File>> GetAttachmentsCandidateDomain(AttachmentInfo info)
        {
            var results = new List<CandidateDomainModel.File>();
            var attachments = _hrToolDbContext.JobApplicationAttachments
                .Where(x => x.JobApplicationId == info.OldApplicationId)
                .ToList();
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
                var newAttachment = new CandidateDomainModel.File
                {
                    Id = attachment.Id.ToString(),
                    Name = attachment.Filename,
                    Path = path
                };
                results.Add(newAttachment);
            }
            return results;
        }

        private List<CandidateDomainModel.Education> GetEducationsCandidateDomain(object externalId)
        {
            return _hrToolDbContext.Educations.Where(x => x.CandidateId == externalId).ToList()
                .Select(x => MapEducationToEducationCandidateDomain(x)).ToList();
        }

        private List<CandidateDomainModel.Project> GetProjectsCandidateDomain(object externalId)
        {
            return _hrToolDbContext.OutstandingProjects.Where(x => x.CandidateId == externalId).ToList()
                .Select(x => MapProjectToProjectCandidateDomain(x)).ToList();
        }

        private List<CandidateDomainModel.Skill> GetSkillsCandidateDomain(object externalId)
        {
            return _hrToolDbContext.Skills.Where(x => x.CandidateId == externalId).ToList()
                .Select(x => MapSkillToCandidateDomain(x)).ToList();
        }

        private List<CandidateDomainModel.WorkExperience> GetWorkExperiencesCandidateDomain(object externalId)
        {
            return _hrToolDbContext.EmploymentHistories.Where(x => x.CandidateId == externalId).ToList()
                .Select(x => MapWorkExperienceToCandidateDomain(x)).ToList();
        }

        private CandidateDomainModel.Education MapEducationToEducationCandidateDomain(MongoDatabaseHrToolv1.Model.Education data)
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

            return new CandidateDomainModel.Education
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

        private CandidateDomainModel.Project MapProjectToProjectCandidateDomain(MongoDatabaseHrToolv1.Model.OutstandingProject data)
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
            return new CandidateDomainModel.Project
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

        private CandidateDomainModel.Skill MapSkillToCandidateDomain(MongoDatabaseHrToolv1.Model.Skill data)
        {
            return new CandidateDomainModel.Skill
            {
                Id = data.Id.ToString(),
                Name = data.SkillName
            };
        }

        private CandidateDomainModel.WorkExperience MapWorkExperienceToCandidateDomain(MongoDatabaseHrToolv1.Model.EmploymentHistory data)
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
            return new CandidateDomainModel.WorkExperience
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

        #endregion      

        private class AttachmentInfo
        {
            public int OldApplicationId { get; set; }
            public MongoDB.Bson.ObjectId NewCandidateId { get; set; }
            public MongoDB.Bson.ObjectId NewApplicationId { get; set; }
            public string ContainerFolder { get; set; }
            public string OldStoragePath { get; set; }
        }

        public class ApplicationCandidateComparer : IEqualityComparer<MongoDatabaseHrToolv1.Model.JobApplication>
        {
            public bool Equals(MongoDatabaseHrToolv1.Model.JobApplication obj1, MongoDatabaseHrToolv1.Model.JobApplication obj2)
            {
                return obj1.CandidateId == obj2.CandidateId;
            }

            public int GetHashCode(MongoDatabaseHrToolv1.Model.JobApplication obj)
            {
                return obj.CandidateId.GetHashCode();
            }
        }
    }
}
