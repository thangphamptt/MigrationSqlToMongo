using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Interview.AggregatesModel
{
	public class OrganizationalUnit : IEntity
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
}
