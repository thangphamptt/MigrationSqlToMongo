using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class WorkExperience
	{
		public WorkExperience()
		{
			Attachments = new List<File>();
		}
		public string Id { get; set; }
		public string Title { get; set; }
		public string Company { get; set; }
		public int? FromMonth { get; set; }
		public int? FromYear { get; set; }
		public int? ToMonth { get; set; }
		public int? ToYear { get; set; }
		public string Description { get; set; }
		public IList<File> Attachments { get; set; }
	}
}
