namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class File
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
		public bool AllowDelete { get; set; }
	}
}
