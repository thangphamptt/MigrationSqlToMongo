using System.Collections.Generic;

namespace MongoDatabase.Domain.Template.AggregatesModel
{
	public class InterviewPrepTemplate : Template
	{
		public string InterviewType { get; set; }
		public IList<string> JobCategoryIds { get; set; }
		public string Description { get; set; }
	}
}
