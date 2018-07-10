using System.Collections.Generic;
using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class Pipeline : IAggregateRoot, IEntity
	{
		public string Id { get; set; }
		public string OrganizationalUnitId { get; set; }
		public IList<PipelineStage> Stages { get; set; }
	}
}
