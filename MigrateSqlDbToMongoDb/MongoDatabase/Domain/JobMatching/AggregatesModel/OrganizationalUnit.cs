using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.JobMatching.AggregatesModel
{
	public class OrganizationalUnit : IAggregateRoot, IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
