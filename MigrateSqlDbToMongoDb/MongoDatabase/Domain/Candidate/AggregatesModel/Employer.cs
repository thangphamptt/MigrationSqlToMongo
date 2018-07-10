using System.Collections.Generic;
using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	class Employer : IEntity
	{
		public string Id { get; set; }
		public string DefaultPipelineId { get; set; }
		public IList<string> PipelineIds { get; set; }
	}
}
