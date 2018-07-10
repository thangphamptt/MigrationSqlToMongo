using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Template.AggregatesModel
{
	public class OrganizationalUnit : IAggregateRoot, IEntity
	{
		public string Id { get; set; }

		public string Name { get; set; }
	}
}
