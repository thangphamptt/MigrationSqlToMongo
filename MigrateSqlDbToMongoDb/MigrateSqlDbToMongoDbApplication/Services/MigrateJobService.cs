using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobDomainModel = MongoDatabase.Domain.Job.AggregatesModel;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;
using InterviewDomainModel = MongoDatabase.Domain.Interview.AggregatesModel;
using OfferDomainModel = MongoDatabase.Domain.Offer.AggregatesModel;
using JobMatchingDomainModel = MongoDatabase.Domain.JobMatching.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateJobService
    {
        private HrToolv1DbContext _hrToolDbContext;
        private JobDbContext _jobDbContext;
        private CandidateDbContext _candidateDbContext;
        private InterviewDbContext _interviewDbContext;
        private OfferDbContext _offerDbContext;
        private JobMatchingDbContext _jobMatchingDbContext;

        private string organizationalUnitId;
        private string userId;
        private List<MongoDatabaseHrToolv1.Model.Job> jobData;

        public MigrateJobService(IConfiguration configuration,
            HrToolv1DbContext hrToolDbContext,
            JobDbContext jobDbContext,
            CandidateDbContext candidateDbContext,
            InterviewDbContext interviewDbContext,
            OfferDbContext offerDbContext,
            JobMatchingDbContext jobMatchingDbContext)
        {
            _hrToolDbContext = hrToolDbContext;
            _jobDbContext = jobDbContext;
            _candidateDbContext = candidateDbContext;
            _interviewDbContext = interviewDbContext;
            _offerDbContext = offerDbContext;
            _jobMatchingDbContext = jobMatchingDbContext;

            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            userId = configuration.GetSection("AdminUser:Id")?.Value;
            jobData = _hrToolDbContext.Jobs.ToList();
        }

        public async Task ExecuteAsync()
        {
            await MigrateJobToJobService();
            await MigrateJobToCandidateService();
            await MigrateJobToInterviewService();
            await MigrateJobToJobMatchingService();
        }

        private async Task MigrateJobToJobService()
        {
            Console.WriteLine("Migrate [job] to [Job service] => Starting...");          

            var jobIdsDestination = _jobDbContext.Jobs.Select(s => s.Id).ToList();
            var jobSource = jobData.Where(w => !jobIdsDestination.Contains(w.Id.ToString())).ToList();

            if (jobSource != null && jobSource.Count > 0)
            {
                int count = 0;
                foreach (var job in jobSource)
                {
                    var category = GetCategoryJobDomain();
                    var template = GetRecruitmentTemplate(job.ExternalId);
                    var jobStatus = GetStatus(job.ExternalId);
                    var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);

                    var data = new JobDomainModel.Job
                    {
                        Id = job.Id.ToString(),
                        Name = title,
                        Vacancies = job.Quantity,
                        OrganizationalUnitId = organizationalUnitId,
                        Summary = string.Empty,
                        Description = template?.JobDescription,
                        Publications = new List<JobDomainModel.Publication>
                               {
                                   new JobDomainModel.Publication
                                   {
                                       ExpirationDate = job.EndDate,
                                       PublishedDate = job.StartDate
                                   }
                               },
                        CreatedDate = template?.CreatedDate ?? DateTime.Now,
                        JobType = JobDomainModel.JobType.FullTime,
                        PositionLevel = JobDomainModel.PositionLevel.Experienced,
                        Status = Helper.JobStatusToJobService(jobStatus, job.ExternalId),
                        CreatedByUserId = userId,
                        CategoryIds = category != null ? new List<string> { category?.Id } : new List<string>()
                    };

                    await _jobDbContext.JobCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{jobSource.Count}");
                }
                Console.WriteLine($"\n Migrate [job] to [Job service] => DONE: inserted {jobSource.Count} applications. \n");
            }
            else
            {
                Console.WriteLine("Migrate [job] to [Job service] => DONE: data exsited. \n");
            }
        }

        private async Task MigrateJobToCandidateService()
        {
            Console.WriteLine("Migrate [job] to [Candidate service] => Starting...");          

            var jobIdsDestination = _candidateDbContext.Jobs.Select(s => s.Id).ToList();
            var jobSource = jobData.Where(w => !jobIdsDestination.Contains(w.Id.ToString())).ToList();

            if (jobSource != null && jobSource.Count > 0)
            {
                int count = 0;
                foreach (var job in jobSource)
                {
                    var category = GetCategoryCandidateDomain();
                    var template = GetRecruitmentTemplate(job.ExternalId);
                    var jobStatus = GetStatus(job.ExternalId);
                    var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);

                    var data = new CandidateDomainModel.Job
                    {
                        Id = job.Id.ToString(),
                        Name = title,
                        Vacancies = job.Quantity,
                        OrganizationalUnitId = organizationalUnitId,
                        Description = template?.JobDescription,
                        PositionLevel = string.Empty,
                        Status = Helper.JobStatusToCandidateService(jobStatus, job.ExternalId),
                        JobCategoryIds = category != null ? new List<string> { category?.Id } : new List<string>()
                    };

                    await _candidateDbContext.JobCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{jobSource.Count}");
                }
                Console.WriteLine($"\n Migrate [job] to [Candidate service] => DONE: inserted {jobSource.Count} applications. \n");
            }
            else
            {
                Console.WriteLine("Migrate [job] to [Candidate service] => DONE: data exsited. \n");
            }
        }

        private async Task MigrateJobToInterviewService()
        {
            Console.WriteLine("Migrate [job] to [Interview service] => Starting...");           

            var jobIdsDestination = _interviewDbContext.Jobs.Select(s => s.Id).ToList();
            var jobSource = jobData.Where(w => !jobIdsDestination.Contains(w.Id.ToString())).ToList();

            if (jobSource != null && jobSource.Count > 0)
            {
                int count = 0;
                foreach (var job in jobSource)
                {
                    var jobStatus = GetStatus(job.ExternalId);
                    var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);

                    var data = new InterviewDomainModel.Job
                    {
                        Id = job.Id.ToString(),
                        Name = title,
                        OrganizationalUnitId = organizationalUnitId,
                        Status = Helper.JobStatusToInterviewService(jobStatus, job.ExternalId)
                    };

                    await _interviewDbContext.JobCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{jobSource.Count}");
                }
                Console.WriteLine($"\n Migrate [job] to [Interview service] => DONE: inserted {jobSource.Count} applications. \n");
            }
            else
            {
                Console.WriteLine("Migrate [job] to [Interview service] => DONE: data exsited. \n");
            }
        }

        private async Task MigrateJobToOfferService()
        {
            Console.WriteLine("Migrate [job] to [Offer service] => Starting...");         

            var jobIdsDestination = _offerDbContext.Jobs.Select(s => s.Id).ToList();
            var jobSource = jobData.Where(w => !jobIdsDestination.Contains(w.Id.ToString())).ToList();

            if (jobSource != null && jobSource.Count > 0)
            {
                int count = 0;
                foreach (var job in jobSource)
                {
                    var jobStatus = GetStatus(job.ExternalId);
                    var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);

                    var data = new OfferDomainModel.Job
                    {
                        Id = job.Id.ToString(),
                        Name = title,
                        OrganizationalUnitId = organizationalUnitId,
                        Status = Helper.JobStatusToOfferService(jobStatus, job.ExternalId)
                    };

                    await _offerDbContext.JobCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{jobSource.Count}");
                }
                Console.WriteLine($"\n Migrate [job] to [Offer service] => DONE: inserted {jobSource.Count} applications. \n");
            }
            else
            {
                Console.WriteLine("Migrate [job] to [Offer service] => DONE: data exsited. \n");
            }
        }

        private async Task MigrateJobToJobMatchingService()
        {
            Console.WriteLine("Migrate [job] to [Job Matching service] => Starting...");

            var jobIdsDestination = _jobMatchingDbContext.Jobs.Select(s => s.Id).ToList();
            var jobSource = jobData.Where(w => !jobIdsDestination.Contains(w.Id.ToString())).ToList();

            if (jobSource != null && jobSource.Count > 0)
            {
                int count = 0;
                foreach (var job in jobSource)
                {
                    var jobStatus = GetStatus(job.ExternalId);
                    var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);
                    var template = GetRecruitmentTemplate(job.ExternalId);
                    var category = GetCategoryJobMatchingDomain();

                    var data = new JobMatchingDomainModel.Job
                    {
                        Id = job.Id.ToString(),
                        Name = title,
                        OrganizationalUnitId = organizationalUnitId,
                        Summary = string.Empty,
                        Description = template?.JobDescription,
                        JobType = JobMatchingDomainModel.JobType.FullTime,
                        PositionLevel = JobMatchingDomainModel.PositionLevel.Experienced,
                        Status = Helper.JobStatusToJobMatchingService(jobStatus, job.ExternalId),
                        CategoryIds = category != null ? new List<string> { category?.Id } : new List<string>()
                    };

                    await _jobMatchingDbContext.JobCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{jobSource.Count}");
                }
                Console.WriteLine($"\n Migrate [job] to [Job Matching service] => DONE: inserted {jobSource.Count} applications. \n");
            }
            else
            {
                Console.WriteLine("Migrate [job] to [Job Matching service] => DONE: data exsited. \n");
            }
        }


        #region Job Domain Helper
        private JobDomainModel.Category GetCategoryJobDomain()
        {
            return _jobDbContext.Categories.FirstOrDefault(x => x.Code == "other_others");
        }

        #endregion

        #region Candidate Domain Helper
        private CandidateDomainModel.JobCategory GetCategoryCandidateDomain()
        {
            return _candidateDbContext.JobCategories.FirstOrDefault(x => x.Code == "other_others");
        }

        #endregion
        

        private MongoDatabaseHrToolv1.Model.JobStatus GetStatus(int jobExternalId)
        {
            var jobStatus = _hrToolDbContext.JobStatuses?.Where(x => x.JobId == jobExternalId)
                .OrderBy(x => x.Id)
                .FirstOrDefault();
            return jobStatus;
        }

        private MongoDatabaseHrToolv1.Model.RecruitmentTemplate GetRecruitmentTemplate(int jobExternalId)
        {
            return _hrToolDbContext.RecruitmentTemplates
                .Where(w => jobExternalId == w.JobId)
                .OrderBy(o => o.ExternalId)
                .FirstOrDefault();
        }

        private string GetPositionName(int positionId)
        {
            return _hrToolDbContext.Positions?.FirstOrDefault(f => f.ExternalId == positionId)?.PositionName;
        }

        private JobMatchingDomainModel.Category GetCategoryJobMatchingDomain()
        {
            return _jobMatchingDbContext.Categories.FirstOrDefault(x => x.Code == "other_others");
        }
    }
}
