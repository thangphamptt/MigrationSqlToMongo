using Microsoft.Extensions.Configuration;
using MongoDatabase.Domain.Schedule.AggregatesModel;
using MongoDB.Driver;
using System.Linq;

namespace MongoDatabase.DbContext
{
	public class ScheduleDbContext
    {
        private readonly IMongoDatabase _database;

        public ScheduleDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            _database = client.GetDatabase(configuration.GetSection("MongoDB:ScheduleDatabaseName").Value);
        }

        public IMongoCollection<Appointment> AppointmentCollection => _database.GetCollection<Appointment>(nameof(Appointment));
        public IQueryable<Appointment> Appointments => AppointmentCollection.AsQueryable();

        public IMongoCollection<User> UserCollection => _database.GetCollection<User>(nameof(User));
        public IQueryable<User> Users => UserCollection.AsQueryable();

        public IMongoCollection<Domain.Schedule.AggregatesModel.Candidate> CandidateCollection => _database.GetCollection<Domain.Schedule.AggregatesModel.Candidate>(nameof(Domain.Schedule.AggregatesModel.Candidate));
        public IQueryable<Domain.Schedule.AggregatesModel.Candidate> Candidates => CandidateCollection.AsQueryable();

        public IMongoCollection<AppointmentType> AppointmentTypeCollection => _database.GetCollection<AppointmentType>(nameof(AppointmentType));
        public IQueryable<AppointmentType> AppointmentTypes => AppointmentTypeCollection.AsQueryable();

        public IMongoCollection<Location> LocationCollection => _database.GetCollection<Location>(nameof(Location));
        public IQueryable<Location> Locations => LocationCollection.AsQueryable();

        public IMongoCollection<InterviewAptType> InterviewAptTypeCollection => _database.GetCollection<InterviewAptType>(nameof(InterviewAptType));
        public IQueryable<InterviewAptType> InterviewAptTypes => InterviewAptTypeCollection.AsQueryable();
    }
}
