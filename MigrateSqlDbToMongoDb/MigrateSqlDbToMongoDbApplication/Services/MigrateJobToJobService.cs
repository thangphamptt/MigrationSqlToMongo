using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;
using System.Linq;
using JobDomainModel = MongoDatabase.Domain.Job.AggregatesModel;
using HrToolDomainModel = MongoDatabaseHrToolv1.Model;
using System;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateJobToJobService
    {
        private HrToolv1DbContext hrToolDbContext;

        public async Task<int> Execute(IConfiguration configuration)
        {
            hrToolDbContext = new HrToolv1DbContext(configuration);
            var jobDbContext = new JobDbContext(configuration);

            var category = GetCategory(jobDbContext);
            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var userId = configuration.GetSection("AdminUser:Id")?.Value;
            var dataInserted = 0;

            var jobsToJobService = new List<JobDomainModel.Job>();
            try
            {
                var jobs = hrToolDbContext.Jobs.ToList();
                foreach (var job in jobs)
                {
                    if (!jobDbContext.Jobs.Any(w => w.Id == job.Id.ToString()))
                    {
                        var template = GetRecruitmentTemplate(job.ExternalId);
                        var jobStatus = GetStatus(job.ExternalId);
                        var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);

                        var jobToJobService = new JobDomainModel.Job
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
                            CategoryIds = new List<string> { category.Id }
                        };
                        //Migrate job to Job service
                        await jobDbContext.JobCollection.InsertOneAsync(jobToJobService);
                        dataInserted++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return dataInserted;
        }

        private JobDomainModel.Category GetCategory(JobDbContext jobDbContext)
        {
            return jobDbContext.Categories.FirstOrDefault(x => x.Code == "other_others");
        }

        private HrToolDomainModel.JobStatus GetStatus(int jobExternalId)
        {
            var jobStatus = hrToolDbContext.JobStatuses?.Where(x => x.JobId == jobExternalId)
                .OrderBy(x => x.Id)
                .FirstOrDefault();
            return jobStatus;
        }

        private HrToolDomainModel.RecruitmentTemplate GetRecruitmentTemplate(int jobExternalId)
        {
            return hrToolDbContext.RecruitmentTemplates
                .Where(w => jobExternalId == w.JobId)
                .OrderBy(o => o.ExternalId)
                .FirstOrDefault();
        }

        private string GetPositionName(int positionId)
        {
            try
            {
                return hrToolDbContext.Positions?.FirstOrDefault(f => f.ExternalId == positionId)?.PositionName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
