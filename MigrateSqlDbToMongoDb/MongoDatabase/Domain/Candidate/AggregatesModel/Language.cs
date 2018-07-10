using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{ 
	public class Language
	{
		public Language()
		{
			Attachments = new List<File>();
		}
		public string Id { get; set; }
		public string Name { get; set; }
		public string Proficiency { get; set; }
		public IList<File> Attachments { get; set; }
	}
}
