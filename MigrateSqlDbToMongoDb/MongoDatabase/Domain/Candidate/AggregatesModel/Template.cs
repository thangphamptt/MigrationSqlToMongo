using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
    public abstract class Template : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsSystem { get; set; }

        public string OrganizationalUnitId { get; set; }

        public bool IsDisabled { get; set; }
    }
}
