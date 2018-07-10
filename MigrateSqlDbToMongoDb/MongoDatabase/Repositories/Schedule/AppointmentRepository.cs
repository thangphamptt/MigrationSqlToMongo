using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoDatabase.Repositories.Schedule
{
	public class AppointmentRepository
    {
        private readonly ScheduleDbContext _dbContext;

        public AppointmentRepository(IConfiguration configuration)
		{
			_dbContext = new ScheduleDbContext(configuration);
		}

		public async Task<Domain.Schedule.AggregatesModel.Appointment> GetAppointmentByIdAsync(string id)
        {
            return await _dbContext.AppointmentCollection.AsQueryable().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateQuickAppointmentAsync(Domain.Schedule.AggregatesModel.Appointment appointmentCommand)
        {
            await _dbContext.AppointmentCollection.InsertOneAsync(appointmentCommand);
        }

        public async Task UpdateAppointmentAsync(Domain.Schedule.AggregatesModel.Appointment appointment)
        {
            var filter = Builders<Domain.Schedule.AggregatesModel.Appointment>.Filter.Where(x => x.Id == appointment.Id);
            UpdateDefinition<Domain.Schedule.AggregatesModel.Appointment> update = Builders<Domain.Schedule.AggregatesModel.Appointment>.Update.Set(x => x.Start, appointment.Start)
                                                            .Set(x => x.End, appointment.End)
                                                            .Set(x => x.OrganizerId, appointment.OrganizerId);

            await _dbContext.AppointmentCollection.UpdateOneAsync(filter, update);
        }
    }
}
