using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;
using System.Linq;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;
using HrToolDomainModel = MongoDatabaseHrToolv1.Model;
using System;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateJobToCandidateService
    {
        private HrToolv1DbContext hrToolDbContext;

        public async Task<int> Execute(IConfiguration configuration)
        {
            hrToolDbContext = new HrToolv1DbContext(configuration);
            var candidateDbContext = new CandidateDbContext(configuration);

            var category = GetCategory(candidateDbContext);
            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var userId = configuration.GetSection("AdminUser:Id")?.Value;
            var dataInserted = 0;

            try
            {
                var jobs = hrToolDbContext.Jobs.ToList();
                foreach (var job in jobs)
                {
                    if (!candidateDbContext.Jobs.Any(w => w.Id == job.Id.ToString()))
                    {
                        var template = GetRecruitmentTemplate(job.ExternalId);
                        var jobStatus = GetStatus(job.ExternalId);
                        var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);
                        var jobToCandidateService = new CandidateDomainModel.Job
                        {
                            Id = job.Id.ToString(),
                            Name = title,
                            Vacancies = job.Quantity,
                            OrganizationalUnitId = organizationalUnitId,
                            Description = template?.JobDescription,
                            PositionLevel = string.Empty,
                            Status = Helper.JobStatusToCandidateService(jobStatus, job.ExternalId),
                            JobCategoryIds = new List<string> { category.Id }
                        };
                        //Migrate job to Candidate service
                        await candidateDbContext.JobCollection.InsertOneAsync(jobToCandidateService);
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

        private CandidateDomainModel.JobCategory GetCategory(CandidateDbContext candidateDbContext)
        {
            return candidateDbContext.JobCategories.FirstOrDefault(x => x.Code == "other_others");
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
            return hrToolDbContext.Positions?.FirstOrDefault(f => f.ExternalId == positionId)?.PositionName;
        }
    }
}
