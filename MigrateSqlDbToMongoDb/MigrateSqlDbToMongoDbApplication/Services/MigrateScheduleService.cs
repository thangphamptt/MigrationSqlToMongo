using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleDomainModel = MongoDatabase.Domain.Schedule.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateScheduleService
    {
        private HrToolv1DbContext _hrToolDbContext;
        private ScheduleDbContext _scheduleDbContext;
        private InterviewDbContext _interviewDbContext;

        private string organizationalUnitId;
        private CurrentUser user;
        private List<MongoDatabaseHrToolv1.Model.InterviewSchedule> interviewScheduleData;

        public MigrateScheduleService(IConfiguration configuration,
            HrToolv1DbContext hrToolDbContext,
            ScheduleDbContext scheduleDbContext,
            InterviewDbContext interviewDbContext)
        {
            _hrToolDbContext = hrToolDbContext;
            _scheduleDbContext = scheduleDbContext;
            _interviewDbContext = interviewDbContext;

            interviewScheduleData = hrToolDbContext.InterviewSchedules.ToList();
            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            user = new CurrentUser
            {
                Email = configuration.GetSection("AdminUser:Email")?.Value,
                FirstName = configuration.GetSection("AdminUser:FirstName")?.Value,
                LastName = configuration.GetSection("AdminUser:LastName")?.Value,
                Id = configuration.GetSection("AdminUser:Id")?.Value,
                Username = configuration.GetSection("AdminUser:Username")?.Value,
                OrganizationUnitIds = new List<string> { organizationalUnitId }
            };
        }

        public async Task ExecuteAsync()
        {
            await MigrateScheduleToScheduleService();
        }

        private async Task MigrateScheduleToScheduleService()
        {
            Console.WriteLine("Migrate [schedule] to [Schedule service] => Starting...");           

            var appoinmentIdsDestination = _scheduleDbContext.Appointments.Select(s => s.Id).ToList();
            var interviewScheduleSource = interviewScheduleData
                .Where(w => !appoinmentIdsDestination.Contains(w.Id.ToString())).ToList();
            var currentTime = DateTime.Now;
            if (interviewScheduleSource != null && interviewScheduleSource.Count > 0)
            {
                int count = 0;
                foreach (var interviewSchedule in interviewScheduleSource)
                {
                    var interview = _hrToolDbContext.Interviews.FirstOrDefault(x => x.ExternalId == interviewSchedule.InterviewId);
                    var appointmentType = GetAppointmentType(interview);

                    var application = _hrToolDbContext.JobApplications.FirstOrDefault(x => x.ExternalId == interview.JobApplicationId);
                    var candidate = _hrToolDbContext.Candidates.FirstOrDefault(x => x.ExternalId == application.CandidateId);

                    var interviewType = _scheduleDbContext.InterviewAptTypes.FirstOrDefault(x => x.Name == "Onsite Interview");
                    var fromDate = ConvertDateTime(interviewSchedule.FromBookRoomDate, interviewSchedule.FromBookRoomTime);
                    var toDate = ConvertDateTime(interviewSchedule.ToBookRoomDate, interviewSchedule.ToBookRoomTime);

                    var data = new ScheduleDomainModel.Appointment
                    {
                        AppointmentType = appointmentType,
                        Attendees = new List<ScheduleDomainModel.User>
                        {
                            new ScheduleDomainModel.User
                            {
                                Email = user.Email,
                                FirstName = user.FirstName,
                                Id = user.Id,
                                LastName = user.LastName,
                                OrganizationalUnitIds = new List<string>{organizationalUnitId },
                                UserName = user.Username
                            }
                        },
                        Id = interviewSchedule.Id.ToString(),
                        CandidateId = candidate.Id.ToString(),
                        Interviewer = user.Email,
                        InterviewType = interviewType?.Id.ToString(),
                        CreatedDate = fromDate.AddDays(-7),
                        Description = interviewSchedule.ContentSchedule,
                        Duration = CalculateDuration(fromDate, toDate),
                        End = toDate,
                        Location = GetLocation(interviewSchedule.RoomId),
                        OrganizerId = organizationalUnitId,
                        ScheduleId = interview.Id.ToString(),
                        Start = fromDate
                    };

                    await _scheduleDbContext.AppointmentCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{interviewScheduleSource.Count}");
                }
                Console.WriteLine($"\n Migrate [schedule] to [Schedule service] => DONE: inserted {interviewScheduleSource.Count} schedules. \n");
            }
            else
            {
                Console.WriteLine("Migrate [schedule] to [Schedule service] => DONE: data existed. \n");
            }
        }

        #region Helper
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
            return _scheduleDbContext.Locations.FirstOrDefault(x => x.Name == locationName)?.Id;
        }

        private string GetAppointmentType(MongoDatabaseHrToolv1.Model.Interview interview)
        {
            switch (interview.InterviewRoundId)
            {
                case 6:
                    return _scheduleDbContext.AppointmentTypes.FirstOrDefault(x => x.Name == "Culture Assessment")?.Id;
                case 7:
                    return _scheduleDbContext.AppointmentTypes.FirstOrDefault(x => x.Name == "Final Decision Assessment")?.Id;
                default:
                    return _scheduleDbContext.AppointmentTypes.FirstOrDefault(x => x.Name == "Domain Knowledge Assessment")?.Id;
            }
        }

        private DateTime ConvertDateTime(object date, object time)
        {
            if (date is DateTime newDate)
            {
                var newTime = TimeSpan.Parse((string)time);
                return new DateTime(newDate.Year, newDate.Month, newDate.Day, newTime.Hours, newTime.Minutes, newTime.Seconds);
            }
            return DateTime.Now;
        }

        private int CalculateDuration(DateTime fromDate, DateTime toDate)
        {
            return (int)Math.Floor((toDate - fromDate).TotalMinutes);
        }
        #endregion

        private class CurrentUser
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public IList<string> OrganizationUnitIds { get; set; } = new List<string>();
        }
    }
}
