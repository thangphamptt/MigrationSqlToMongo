using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.JobMatching.AggregatesModel
{
	public class Category : IAggregateRoot, IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentId { get; set; }
    }
}
