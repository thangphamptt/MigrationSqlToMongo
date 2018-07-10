using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class PipelineStage : IEntity
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string DefaultIconColor { get; set; }
		public string DefaultIconPath { get; set; }
		public int Order { get; set; }
		public StageType StageType { get; set; }
	}
}
