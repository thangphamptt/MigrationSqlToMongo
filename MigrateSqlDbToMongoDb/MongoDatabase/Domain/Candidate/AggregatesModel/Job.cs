using MongoDatabase.Domain.Common;
using System;
using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class Job : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PositionLevel { get; set; }
        public string Category { get; set; }
        public string CategoryCode { get; set; }
        public int? Vacancies { get; set; }
        public string Location { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string OrganizationalUnitId { get; set; }
        public JobStatus Status { get; set; }
        public IList<string> JobCategoryIds { get; set; } = new List<string>();
    }
}
