using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Linq;
using MigrateSqlDbToMongoDbApplication.Constants;
using System;
using MigrateSqlDbToMongoDbApplication.Common.Services;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
	public class MigrationScheduleService
	{
		private readonly HrToolv1DbContext hrToolDbContext;
		private readonly ScheduleDbContext scheduleDbContext;
		private readonly string organizationalUnitId;
		private readonly string userId;
		public MigrationScheduleService(IConfiguration configuration)
		{
			hrToolDbContext = new HrToolv1DbContext(configuration);
			scheduleDbContext = new ScheduleDbContext(configuration);
			organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			userId = configuration.GetSection("AdminUser:Id")?.Value;
		}

		public async Task<int> ExecuteAsync()
		{
			var totalRecords = 0;

			var data = hrToolDbContext.InterviewSchedules.ToList();
			var defaultUser = scheduleDbContext.Users.FirstOrDefault(x => x.Email == "lai.nguyen@orientsoftware.net");
			foreach (var item in data)
			{
				await scheduleDbContext.AppointmentCollection.InsertOneAsync(new MongoDatabase.Domain.Schedule.AggregatesModel.Appointment
				{
				AppointmentType	= GetAppointmentType(),
				Attendees = new List<MongoDatabase.Domain.Schedule.AggregatesModel.User>
				{
					new MongoDatabase.Domain.Schedule.AggregatesModel.User
					{
						Email = defaultUser.Email,
						FirstName = defaultUser.FirstName,
						Id = defaultUser.Id,
						LastName = defaultUser.LastName,
						OrganizationalUnitIds = defaultUser.OrganizationalUnitIds,
						UserName = defaultUser.UserName
					}
				},
				Id = item.Id.ToString(),
				CandidateId = "",
				CreatedDate = new DateTime(),
				Description = item.ContentSchedule,
				Duration = 0,
				//End = item.ToBookRoomTime is DateTime?(item)
				});
			}
			return totalRecords;
		}

		private string GetAppointmentType()
		{
			var type = string.Empty;

			return type;
		}

		private DateTime ConvertDateTime(DateTime date, DateTime time)
		{
			return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
		}

		private int CalculateDuration(DateTime fromDate, DateTime fromTime, DateTime toDate, DateTime toTime)
		{
			var from = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, fromTime.Hour, fromTime.Minute, fromTime.Second);
			var to = new DateTime(toDate.Year, toDate.Month, toDate.Day, toTime.Hour, toTime.Minute, toTime.Second);

			return 0;
		}
	}
}
