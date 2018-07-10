using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class Education
	{
		public Education()
		{
			Attachments = new List<File>();
		}

		public string Id { get; set; }
		public string School { get; set; }
		public string Degree { get; set; }
		public int? FromMonth { get; set; }
		public int? FromYear { get; set; }
		public int? ToMonth { get; set; }
		public int? ToYear { get; set; }
		public IList<File> Attachments { get; set; }
	}
}
