using MongoDatabase.Domain.Common;
using System.Collections.Generic;

namespace MongoDatabase.Domain.JobMatching.AggregatesModel
{
	public class Job : IAggregateRoot, IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OrganizationalUnitId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public JobType JobType { get; set; }
        public PositionLevel PositionLevel { get; set; }
        public JobStatus Status { get; set; }
        public IList<string> CategoryIds { get; set; }
    }
}
