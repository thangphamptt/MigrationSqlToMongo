using System.Collections.Generic;

namespace MongoDatabase.Domain.Interview.AggregatesModel
{
	public class InterviewPrepTemplate : Template
	{
		public string InterviewType { get; set; }
		public IList<string> JobCategoryIds { get; set; }
		public string Description { get; set; }
	}
}
