using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;
using System.Linq;
using MongoDatabase.Domain.Job.AggregatesModel;
using System;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateJob
    {
        public async Task<int> Execute(IConfiguration configuration)
        {
            var hrToolDbContext = new HrToolv1DbContext(configuration);
            var jobDbContext = new JobDbContext(configuration);

            try
            {
                var jobs = hrToolDbContext.Jobs
                    .ToList()
                   .Select(s => new Job
                   {
                       Id = s.Id.ToString(),
                       Name = s.JobTitle,
                       Vacancies = s.Quantity,
                       OrganizationalUnitId = "132354657",
                       Summary = string.Empty,
                       Description = GetDescriptions(hrToolDbContext, s.ExternalId)
                   })
                   .ToList();

                //await jobDbContext.JobCollection.InsertManyAsync(jobs);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return jobDbContext.Jobs.Count();
        }

        private string GetDescriptions(HrToolv1DbContext hrToolDbContext, int jobExternalId)
        {
            return hrToolDbContext.RecruitmentTemplates
                .Where(w => jobExternalId == w.JobId)
                .OrderBy(o => o.ExternalId)
                .FirstOrDefault()?
                .JobDescription;
        }
    }
}
