using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using InterviewDomainModel = MongoDatabase.Domain.Interview.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateInterviewService
    {
        private HrToolv1DbContext _hrToolDbContext;
        private InterviewDbContext _interviewDbContext;

        private string organizationalUnitId;
        private string userId;
        private string email;
        private List<MongoDatabaseHrToolv1.Model.Interview> interviewData;

        public MigrateInterviewService(IConfiguration configuration,
            HrToolv1DbContext hrToolDbContext,
           InterviewDbContext interviewDbContext)
        {
            _hrToolDbContext = hrToolDbContext;
            _interviewDbContext = interviewDbContext;

            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            userId = configuration.GetSection("AdminUser:Id")?.Value;
            email = configuration.GetSection("AdminUser:Email")?.Value;

            interviewData = _hrToolDbContext.Interviews.ToList();
        }

        public async Task ExecuteAsync()
        {
            await MigrateInterviewToInterviewService();
        }

        private async Task MigrateInterviewToInterviewService()
        {
            Console.WriteLine("Migrate [interview] to [Interview service] => Starting...");      

            var interviewIdsDestination = _interviewDbContext.Interviews.Select(s => s.Id).ToList();
            var interviewsSource = _hrToolDbContext.Interviews.ToList()
                .Where(w => !interviewIdsDestination.Contains(w.Id.ToString()))
                .ToList();
            if (interviewsSource != null && interviewsSource.Count > 0)
            {
                int count = 0;
                foreach (var interview in interviewsSource)
                {
                    var candidateApplication = GetCandidateApplication(interview);
                    var interviewSchedules = GetInterviewSchedule(interview);
                    var interviewSchedule = interviewSchedules?.FirstOrDefault();
                    var interviewType = GetInterviewType();

                    var data = new InterviewDomainModel.Interview()
                    {
                        Id = interview.Id.ToString(),
                        CandidateId = candidateApplication?.CandidateId.ToString(),
                        ApplicationId = candidateApplication?.ApplicationId.ToString(),
                        JobId = candidateApplication?.JobId.ToString(),
                        TypeId = interviewType?.Id.ToString() ?? string.Empty,
                        OrganizationalUnitId = organizationalUnitId,
                        StartTime = !string.IsNullOrEmpty(interviewSchedule?.FromBookRoomDate.ToString()) ? DateTime.Parse(interviewSchedule?.FromBookRoomDate.ToString(), CultureInfo.InvariantCulture) : DateTime.Now,
                        EndTime = !string.IsNullOrEmpty(interviewSchedule?.ToBookRoomDate.ToString()) ? DateTime.Parse(interviewSchedule?.ToBookRoomDate.ToString(), CultureInfo.InvariantCulture) : DateTime.Now,
                        CreatedByUserId = userId,
                        CreatedDate = DateTime.Now,
                        Schedules = interviewSchedules?.Select(x => new InterviewDomainModel.Schedule
                        {
							Id = x.Id.ToString(),
                            TimeFrom = !string.IsNullOrEmpty(x.FromBookRoomDate.ToString()) ? DateTime.Parse(x.FromBookRoomDate.ToString(), CultureInfo.InvariantCulture) : DateTime.Now,
                            Duration = !string.IsNullOrEmpty(x.FromBookRoomTime.ToString()) && !string.IsNullOrEmpty(x.FromBookRoomTime.ToString())
                                ? (int)(DateTime.Parse(x.ToBookRoomTime.ToString(), CultureInfo.InvariantCulture) - DateTime.Parse(x.FromBookRoomTime.ToString(), CultureInfo.InvariantCulture)).TotalMinutes : 0,
                            AssessmentType = GetAssessmentType(interview),
                            Location = GetLocation(x.RoomId),
                            Interviewer = email
                        }).ToList()
                    };
                    await _interviewDbContext.InterviewCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{interviewsSource.Count}");
                }
                Console.WriteLine($"\n Migrate [interview] to [Interview service] => DONE: inserted {interviewsSource.Count} applications. \n");
            }
            else
            {
                Console.WriteLine($"Migrate [interview] to [Interview service] => DONE: data exsited. \n");
            }
        }

        #region Interview Domain Helper
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
            return _interviewDbContext.Locations.FirstOrDefault(x => x.Name == locationName)?.Id;
        }

        private string GetAssessmentType(MongoDatabaseHrToolv1.Model.Interview interview)
        {
            switch (interview.InterviewRoundId)
            {
                case 6:
                    return _interviewDbContext.AssessmentTypes.FirstOrDefault(x => x.Name == "Culture Assessment")?.Id;
                case 7:
                    return _interviewDbContext.AssessmentTypes.FirstOrDefault(x => x.Name == "Final Decision Assessment")?.Id;
                default:
                    return _interviewDbContext.AssessmentTypes.FirstOrDefault(x => x.Name == "Domain Knowledge Assessment")?.Id;
            }
        }

        private CandidateApplicationModel GetCandidateApplication(MongoDatabaseHrToolv1.Model.Interview interview)
        {
            return (from jobApplication in _hrToolDbContext.JobApplications
                    join candidate in _hrToolDbContext.Candidates on jobApplication.CandidateId equals candidate.ExternalId
                    join job in _hrToolDbContext.Jobs on jobApplication.JobId equals job.ExternalId
                    where jobApplication.ExternalId == interview.JobApplicationId
                    select new CandidateApplicationModel
                    {
                        CandidateId = candidate.Id,
                        ApplicationId = jobApplication.Id,
                        JobId = job.Id
                    }).FirstOrDefault();
        }

        private List<MongoDatabaseHrToolv1.Model.InterviewSchedule> GetInterviewSchedule(MongoDatabaseHrToolv1.Model.Interview interview)
        {
            return _hrToolDbContext.InterviewSchedules
                .Where(x => x.InterviewId == interview.ExternalId)
                .OrderBy(x => x.InterviewId)
                .ThenByDescending(x => x.ExternalId)
                .ToList();
        }

        private InterviewDomainModel.InterviewType GetInterviewType()
        {
            var interviewType = _interviewDbContext.InterviewTypes.FirstOrDefault(x => x.Name == "Onsite Interview");
            return interviewType;
        }        

        #endregion

        public class CandidateApplicationModel
        {
            public ObjectId CandidateId { get; set; }
            public ObjectId ApplicationId { get; set; }
            public ObjectId JobId { get; set; }
        }
    }
}
