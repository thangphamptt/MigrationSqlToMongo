using MongoDatabase.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDatabase.Domain.Job.AggregatesModel
{
    public class Job : IAggregateRoot, IEntity
    {    
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Vacancies { get; set; }
        public string OrganizationalUnitId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public JobType JobType { get; set; }
        public PositionLevel PositionLevel { get; set; }
        public JobStatus Status { get; set; }
        public string ContactPerson { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public IList<string> CategoryIds { get; set; } = new List<string>();
        public IList<string> FollowedByUserIds { get; set; } = new List<string>();
        public IList<string> OwnedByUserIds { get; set; } = new List<string>();
        public IList<string> ReadByUserIds { get; set; } = new List<string>();
        public IList<Publication> Publications { get; set; } = new List<Publication>();
        public IList<string> PublishingPortalIds { get; set; } = new List<string>();
    }
}
