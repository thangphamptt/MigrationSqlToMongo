using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;
using System.Linq;
using OfferDomainModel = MongoDatabase.Domain.Offer.AggregatesModel;
using HrToolDomainModel = MongoDatabaseHrToolv1.Model;
using System;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateJobToOfferService
    {
        private HrToolv1DbContext hrToolDbContext;

        public async Task<int> Execute(IConfiguration configuration)
        {
            hrToolDbContext = new HrToolv1DbContext(configuration);
            var offerDbContext = new OfferDbContext(configuration);

            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var userId = configuration.GetSection("AdminUser:Id")?.Value;
            var dataInserted = 0;

            try
            {
                var jobs = hrToolDbContext.Jobs.ToList();
                foreach (var job in jobs)
                {
                    if (!offerDbContext.Jobs.Any(w => w.Id == job.Id.ToString()))
                    {
                        var template = GetRecruitmentTemplate(job.ExternalId);
                        var jobStatus = GetStatus(job.ExternalId);
                        var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);

                        var jobToOfferService = new OfferDomainModel.Job
                        {
                            Id = job.Id.ToString(),
                            Name = title,
                            OrganizationalUnitId = organizationalUnitId,
                            Status = Helper.JobStatusToOfferService(jobStatus, job.ExternalId)
                        };
                        //Migrate job to Job service
                        await offerDbContext.JobCollection.InsertOneAsync(jobToOfferService);
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
