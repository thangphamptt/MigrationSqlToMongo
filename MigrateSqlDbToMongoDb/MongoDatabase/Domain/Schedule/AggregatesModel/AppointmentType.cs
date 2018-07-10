using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Schedule.AggregatesModel
{
	public class AppointmentType : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

}
