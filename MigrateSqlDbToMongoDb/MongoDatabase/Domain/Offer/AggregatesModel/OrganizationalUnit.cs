using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Offer.AggregatesModel
{
	public class OrganizationalUnit : IEntity
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
}
