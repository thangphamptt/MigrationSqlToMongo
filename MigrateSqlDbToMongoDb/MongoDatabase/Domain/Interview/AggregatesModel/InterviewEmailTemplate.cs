using System.Collections.Generic;

namespace MongoDatabase.Domain.Interview.AggregatesModel
{
	public class InterviewEmailTemplate : Template
	{
		public string Subject { get; set; }
		public string Body { get; set; }
		public string Type { get; set; }
		public IList<Attachment> Attachments { get; set; } = new List<Attachment>();
	}
}
