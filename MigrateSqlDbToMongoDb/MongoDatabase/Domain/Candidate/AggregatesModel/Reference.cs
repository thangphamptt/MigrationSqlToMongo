using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class Reference
	{
		public Reference()
		{
			Attachments = new List<File>();
		}
		public string Id { get; set; }
		public string Fullname { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public IList<File> Attachments { get; set; }
	}
}
