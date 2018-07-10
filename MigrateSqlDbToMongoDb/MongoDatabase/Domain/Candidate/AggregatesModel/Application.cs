using MongoDatabase.Domain.Common;
using System;
using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class Application : IEntity
	{
		public Application()
		{
			CV = new CV();
		}

		public string Id { get; set; }
		public string ExternalId { get; set; }
		public string CandidateId { get; set; }
		public string JobId { get; set; }
		public string OrganizationalUnitId { get; set; }
		public DateTime AppliedDate { get; set; }
		public CurrentPipelineStage CurrentPipelineStage { get; set; }
		public CV CV { get; set; }
		public bool IsRejected { get; set; }
        public bool? IsSentEmail { get; set; }
		public IList<File> Attachments { get; set; } = new List<File>();
		public string CvSource { get; set; }
	}
}
