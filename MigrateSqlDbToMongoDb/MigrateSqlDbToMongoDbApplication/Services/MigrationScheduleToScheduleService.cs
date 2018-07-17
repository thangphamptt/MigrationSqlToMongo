using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Linq;
using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
	public class MigrationScheduleToScheduleService
	{
		private readonly HrToolv1DbContext hrToolDbContext;
		private readonly InterviewDbContext interviewDbContext;
		private readonly ScheduleDbContext scheduleDbContext;

		private readonly string organizationalUnitId;
		private readonly CurrentUser user;
		public MigrationScheduleToScheduleService(IConfiguration configuration)
		{
			hrToolDbContext = new HrToolv1DbContext(configuration);
			scheduleDbContext = new ScheduleDbContext(configuration);
			interviewDbContext = new InterviewDbContext(configuration);
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

		public async Task<int> ExecuteAsync()
		{
			var totalRecords = 0;

			var data = hrToolDbContext.InterviewSchedules.ToList();
			var currentTime = DateTime.Now;
			foreach (var item in data)
			{
					if (!scheduleDbContext.Appointments.Any(x => x.Id == item.Id.ToString()))
					{
						await AddSchedule(item);
						totalRecords++;
					}
			}
			return totalRecords;
		}

		private async Task AddSchedule(MongoDatabaseHrToolv1.Model.InterviewSchedule item)
		{
			var interview = hrToolDbContext.Interviews.FirstOrDefault(x => x.ExternalId == item.InterviewId);
			var appointmentType = GetAppointmentType(interview);

			var application = hrToolDbContext.JobApplications.FirstOrDefault(x => x.ExternalId == interview.JobApplicationId);
			var candidate = hrToolDbContext.Candidates.FirstOrDefault(x => x.ExternalId == application.CandidateId);

			var interviewType = scheduleDbContext.InterviewAptTypes.FirstOrDefault(x => x.Name == "Onsite Interview");
			var fromDate = ConvertDateTime(item.FromBookRoomDate, item.FromBookRoomTime);
			var toDate = ConvertDateTime(item.ToBookRoomDate, item.ToBookRoomTime);

			await scheduleDbContext.AppointmentCollection.InsertOneAsync(new MongoDatabase.Domain.Schedule.AggregatesModel.Appointment
			{
				AppointmentType = appointmentType,
				Attendees = new List<MongoDatabase.Domain.Schedule.AggregatesModel.User>
				{
					new MongoDatabase.Domain.Schedule.AggregatesModel.User
					{
						Email = user.Email,
						FirstName = user.FirstName,
						Id = user.Id,
						LastName = user.LastName,
						OrganizationalUnitIds = new List<string>{organizationalUnitId },
						UserName = user.Username
					}
				},
				Id = item.Id.ToString(),
				CandidateId = candidate.Id.ToString(),
				Interviewer = user.Email,
				InterviewType = interviewType.Id.ToString(),
				CreatedDate = fromDate.AddDays(-7),
				Description = item.ContentSchedule,
				Duration = CalculateDuration(fromDate, toDate),
				End = toDate,
				Location = GetLocation(item.RoomId),
				OrganizerId = organizationalUnitId,
				ScheduleId = interview.Id.ToString(),
				Start = fromDate
			});
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
			return scheduleDbContext.Locations.FirstOrDefault(x => x.Name == locationName)?.Id;
		}

		private string GetAppointmentType(MongoDatabaseHrToolv1.Model.Interview interview)
		{
			switch (interview.InterviewRoundId)
			{
				case 6:
					return scheduleDbContext.AppointmentTypes.FirstOrDefault(x => x.Name == "Culture Assessment")?.Id;
				case 7:
					return scheduleDbContext.AppointmentTypes.FirstOrDefault(x => x.Name == "Final Decision Assessment")?.Id;
				default:
					return scheduleDbContext.AppointmentTypes.FirstOrDefault(x => x.Name == "Domain Knowledge Assessment")?.Id;
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
