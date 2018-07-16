using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateInterviewToInterviewService
    {
        private HrToolv1DbContext hrToolDbContext;

        public async Task<int> InsertInterviewToInterviewService(IConfiguration configuration)
        {
            hrToolDbContext = new HrToolv1DbContext(configuration);
            var interviewDbContext = new InterviewDbContext(configuration);
            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var userId = configuration.GetSection("AdminUser:Id")?.Value;
            int totalInterview = 0;
           
            
            var originalInterviews = hrToolDbContext.Interviews.ToList();
            try
            {
                foreach (var item in originalInterviews)
                {
                    var candidateApplication = (from jobApplication in hrToolDbContext.JobApplications
                                                 join candidate in hrToolDbContext.Candidates on jobApplication.CandidateId equals candidate.ExternalId
                                                 join job in hrToolDbContext.Jobs on jobApplication.JobId equals job.ExternalId
                                                 where jobApplication.ExternalId == item.Id
                                                select new CandidateApplicationModel
                                                {
                                                    CandidateId = candidate.Id.ToString(),
                                                    ApplicationId = jobApplication.Id.ToString(),
                                                    JobId = job.Id.ToString()
                                                }).FirstOrDefault();

                    if (!interviewDbContext.Interviews.Any(w => w.Id == item.Id.ToString()))
                    {
                        
                        var interview = new MongoDatabase.Domain.Interview.AggregatesModel.Interview
                        {
                            Id = item.Id.ToString(),
                            CandidateId = candidateApplication.CandidateId,
                            ApplicationId = candidateApplication.ApplicationId,
                            JobId = candidateApplication.JobId,
                            TypeId = "Phone Interview",
                            OrganizationalUnitId = organizationalUnitId
                        };
                        await interviewDbContext.InterviewCollection.InsertOneAsync(interview);
                        totalInterview++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return totalInterview;
        }


    }

    public class CandidateApplicationModel
    {
        public string CandidateId { get; set; }
        public string ApplicationId { get; set; }
        public string JobId { get; set; }
    }
}
