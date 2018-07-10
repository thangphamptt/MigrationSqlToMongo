using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class Course
	{
		public Course()
		{
			Attachments = new List<File>();
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public int? FromMonth { get; set; }
		public int? FromYear { get; set; }
		public int? ToMonth { get; set; }
		public int? ToYear { get; set; }
		public string Comment { get; set; }
		public IList<File> Attachments { get; set; }
	}
}
