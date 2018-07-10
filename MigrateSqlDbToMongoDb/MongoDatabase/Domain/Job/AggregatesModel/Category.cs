using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Job.AggregatesModel
{
	public class Category : IEntity
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string ParentId { get; set; }
	}
}
