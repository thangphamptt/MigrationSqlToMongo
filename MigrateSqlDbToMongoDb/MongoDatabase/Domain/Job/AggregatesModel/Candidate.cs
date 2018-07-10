using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Job.AggregatesModel
{
	public class Candidate : IEntity
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ExternalId { get; set; }

        public string OrganizationalUnitId { get; set; }

        public string UserObjectId { get; set; }
    }
}
