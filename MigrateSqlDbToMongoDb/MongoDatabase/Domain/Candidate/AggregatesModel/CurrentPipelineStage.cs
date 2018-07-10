namespace MongoDatabase.Domain.Candidate.AggregatesModel
{ 
	public class CurrentPipelineStage
	{
		public string PipelineId { get; set; }
		public string PipelineStageId { get; set; }
		public string PipelineStageName { get; set; }
	}
}
