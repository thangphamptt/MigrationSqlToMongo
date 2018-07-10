using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Interview.AggregatesModel
{
	public class Job : IEntity
	{
		public string Id { get; set; }
		public string OrganizationalUnitId { get; set; }
		public string Name { get; set; }
		public JobStatus Status { get; set; }
	}
}
