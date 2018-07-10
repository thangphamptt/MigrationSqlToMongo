using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class OrganizationalUnit : IAggregateRoot, IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
