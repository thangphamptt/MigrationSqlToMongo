using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateInterviewToInterviewService
    {
        private HrToolv1DbContext hrToolDbContext;
        private InterviewDbContext interviewDbContext;
        public async Task<int> InsertInterviewToInterviewService(IConfiguration configuration)
        {
            hrToolDbContext = new HrToolv1DbContext(configuration);
            interviewDbContext = new InterviewDbContext(configuration);

            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var userId = configuration.GetSection("AdminUser:Id")?.Value;

            var email = configuration.GetSection("AdminUser:Email")?.Value;
            int totalInterview = 0;


            var originalInterviews = hrToolDbContext.Interviews.ToList();
            try
            {
                foreach (var item in originalInterviews)
                {
                    if (!interviewDbContext.Interviews.Any(w => w.Id == item.Id.ToString()))
                    {
                        var candidateApplication = (from jobApplication in hrToolDbContext.JobApplications
                                                    join candidate in hrToolDbContext.Candidates on jobApplication.CandidateId equals candidate.ExternalId
                                                    join job in hrToolDbContext.Jobs on jobApplication.JobId equals job.ExternalId
                                                    where jobApplication.ExternalId == item.JobApplicationId
                                                    select new CandidateApplicationModel
                                                    {
                                                        CandidateId = candidate.Id,
                                                        ApplicationId = jobApplication.Id,
                                                        JobId = job.Id
                                                    }).FirstOrDefault();

                        var interviewSchedules = hrToolDbContext.InterviewSchedules.Where(x => x.InterviewId == item.ExternalId).OrderBy(x => x.InterviewId).ThenByDescending(x => x.ExternalId).ToList();

                        var interviewSchedule = interviewSchedules.FirstOrDefault();
                        var interviewType = interviewDbContext.InterviewTypes.FirstOrDefault(x => x.Name == "Onsite Interview");

                        var interview = new MongoDatabase.Domain.Interview.AggregatesModel.Interview()
                        {
                            Id = item.Id.ToString(),
                            CandidateId = candidateApplication.CandidateId.ToString(),
                            ApplicationId = candidateApplication.ApplicationId.ToString(),
                            JobId = candidateApplication.JobId.ToString(),
                            TypeId = interviewType?.Id.ToString() ?? string.Empty,
                            OrganizationalUnitId = organizationalUnitId,
                            StartTime = !string.IsNullOrEmpty(interviewSchedule?.FromBookRoomDate.ToString()) ? DateTime.Parse(interviewSchedule.FromBookRoomDate.ToString(), CultureInfo.InvariantCulture) : DateTime.Now,
                            EndTime = !string.IsNullOrEmpty(interviewSchedule?.ToBookRoomDate.ToString()) ? DateTime.Parse(interviewSchedule.ToBookRoomDate.ToString(), CultureInfo.InvariantCulture) : DateTime.Now,
                            CreatedByUserId = userId,
                            CreatedDate = DateTime.Now,
                            Schedules = interviewSchedules.Select(x => new MongoDatabase.Domain.Interview.AggregatesModel.Schedule
                            {
                                TimeFrom = !string.IsNullOrEmpty(x.FromBookRoomDate.ToString()) ? DateTime.Parse(x.FromBookRoomDate.ToString(), CultureInfo.InvariantCulture) : DateTime.Now,
                                Duration = !string.IsNullOrEmpty(x.FromBookRoomTime.ToString()) && !string.IsNullOrEmpty(x.FromBookRoomTime.ToString())
                                    ? (int)(DateTime.Parse(x.ToBookRoomTime.ToString(), CultureInfo.InvariantCulture) - DateTime.Parse(x.FromBookRoomTime.ToString(), CultureInfo.InvariantCulture)).TotalMinutes : 0,
                                AssessmentType = GetAssessmentType(item),
                                Location = GetLocation(x.RoomId),
                                Interviewer = email
                            }).ToList()
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


        private string GetLocation(object roomId)
        {
            var locationName = "Mountain Room";
            if (roomId is int)
            {
                switch ((int)roomId)
                {
                    case 5:
                        locationName = "Mountain Room";
                        break;
                    case 6:
                        locationName = "Ocean Room";
                        break;
                    case 10:
                        locationName = "Forest Room";
                        break;
                    case 12:
                        locationName = "Valley Room";
                        break;
                }
            }
            return interviewDbContext.Locations.FirstOrDefault(x => x.Name == locationName)?.Id;
        }

        private string GetAssessmentType(MongoDatabaseHrToolv1.Model.Interview interview)
        {
            switch (interview.InterviewRoundId)
            {
                case 6:
                    return interviewDbContext.AssessmentTypes.FirstOrDefault(x => x.Name == "Culture Assessment")?.Id;
                case 7:
                    return interviewDbContext.AssessmentTypes.FirstOrDefault(x => x.Name == "Final Decision Assessment")?.Id;
                default:
                    return interviewDbContext.AssessmentTypes.FirstOrDefault(x => x.Name == "Domain Knowledge Assessment")?.Id;
            }
        }


    }




    public class CandidateApplicationModel
    {
        public ObjectId CandidateId { get; set; }
        public ObjectId ApplicationId { get; set; }
        public ObjectId JobId { get; set; }
    }
}
