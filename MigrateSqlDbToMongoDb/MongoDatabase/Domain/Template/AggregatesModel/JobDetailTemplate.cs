using System.Collections.Generic;

namespace MongoDatabase.Domain.Template.AggregatesModel
{
	public class JobDetailTemplate : Template
	{
		public string Description { get; set; }

		public string Summary { get; set; }

		public string ContactPerson { get; set; }

		public PositionLevel? PositionLevel { get; set; } = null;

		public JobType? JobType { get; set; } = null;

		public IList<string> JobCategoryIds { get; set; } = new List<string>();
	}
}
