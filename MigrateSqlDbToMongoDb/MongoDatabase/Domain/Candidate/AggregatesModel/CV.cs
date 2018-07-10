using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class CV
	{
		public string CoverLetter { get; set; }
		public string Summary { get; set; }
		public IList<Certification> Certifications { get; set; } = new List<Certification>();
		public IList<Course> Courses { get; set; } = new List<Course>();
		public IList<Education> Education { get; set; } = new List<Education>();
		public IList<Language> Languages { get; set; } = new List<Language>();
		public IList<Project> Projects { get; set; } = new List<Project>();
		public IList<Reference> References { get; set; } = new List<Reference>();
		public IList<Skill> Skills { get; set; } = new List<Skill>();
		public IList<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
	}
}
