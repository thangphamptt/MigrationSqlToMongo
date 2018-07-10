using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Schedule.AggregatesModel
{
	public class Location : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
