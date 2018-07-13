using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDatabaseHrToolv1.Model
{
	public partial class JobApplication
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public int CandidateId { get; set; }
		public object PositionId { get; set; }
		public string UrlDocument { get; set; }
		public DateTime CreatedDate { get; set; }
		public object ValidTo { get; set; }
		public object ExperienceYear { get; set; }
		public string WorkExperience { get; set; }
		public object ApplicationStatus { get; set; }
		public object SalaryOffer { get; set; }
		public string Note { get; set; }
		public int CompanyId { get; set; }
		public object CVSourceId { get; set; }
		public object IsCandidateUpdated { get; set; }
		public object GroupIQ { get; set; }
		public object GroupTechnical { get; set; }
		public object NumberRoundInterview { get; set; }
		public object StartDateSuggest { get; set; }
		public string NoteStartDateSuggest { get; set; }
		public object JobId { get; set; }
		public object ModifiedDate { get; set; }
		public object CurrentSalary { get; set; }
		public object ExpectedSalary { get; set; }
		public object SuggestedSalary { get; set; }
		public object OverallStatus { get; set; }
		public object RoundStatus { get; set; }
		public object Rating { get; set; }
		public object IsSendMail { get; set; }
		public string OfferPositionName { get; set; }
		public object RatingUpdatedDate { get; set; }
		public long RowID { get; set; }		
	}
}
