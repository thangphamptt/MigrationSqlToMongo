using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Schedule.AggregatesModel
{
	public class Candidate : IEntity
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}
}
