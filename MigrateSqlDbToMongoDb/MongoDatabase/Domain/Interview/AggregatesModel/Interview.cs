using MongoDatabase.Domain.Common;
using System;
using System.Collections.Generic;

namespace MongoDatabase.Domain.Interview.AggregatesModel
{
	public class Interview : IAggregateRoot, IEntity
	{
		public string Id { get; set; }
        public string Subject { get; set; }
		public string CandidateId { get; set; }
		public string ApplicationId { get; set; }
		public string JobId { get; set; }
		public string TypeId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public DateTime? SentDate { get; set; }
		public string CreatedByUserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public string ModifiedByUserId { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string Description { get; set; }
		public string OrganizationalUnitId { get; set; }
        public IList<Schedule> Schedules { get; set; }		
    }
}
