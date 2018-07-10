using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.JobMatching.AggregatesModel
{
	public class Candidate : IAggregateRoot, IEntity
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string ExternalId { get; set; }

        public string OrganizationalUnitId { get; set; }

        public string UserObjectId { get; set; }
    }
}
