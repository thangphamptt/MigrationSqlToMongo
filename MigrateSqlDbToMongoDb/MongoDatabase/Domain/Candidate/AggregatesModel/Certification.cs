using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class Certification
	{
		public Certification()
		{
			Attachments = new List<File>();
		}
		public string Id { get; set; }
		public string Name { get; set; }
		public string Authority { get; set; }
		public string LicenseNumber { get; set; }
		public string Url { get; set; }

		public int? FromMonth { get; set; }
		public int? FromYear { get; set; }
		public int? ToMonth { get; set; }
		public int? ToYear { get; set; }
		
		public IList<File> Attachments { get; set; }
	}
}
