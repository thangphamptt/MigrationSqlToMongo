using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDatabaseHrToolv1.Model
{
	public class Candidate
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public object BirthDay { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string EmailOther { get; set; }
		public string Phone { get; set; }
		public string Mobile { get; set; }
		public string EducationSchool { get; set; }
		public string GraduationYear { get; set; }
		public object CreateDate { get; set; }
		public object ValidTo { get; set; }
		public object IsAccepted { get; set; }
		public object IssuingDate { get; set; }
		public object SendingDate { get; set; }
		public object ConfirmDate { get; set; }
		public object SigningDate { get; set; }
		public object WorkingStartDate { get; set; }
		public string Note { get; set; }
		public string Gender { get; set; }
		public object SalaryOffer { get; set; }
		public object BasicSalaryOffer { get; set; }
		public object PercentageSalaryOffer { get; set; }
		public string NoteOffer { get; set; }
		public object IsUpdated { get; set; }
		public string SessionId { get; set; }
		public string Password { get; set; }
		public string ImagePath { get; set; }
		public string IdNo { get; set; }
		public string Domain { get; set; }
		public object IsDelete { get; set; }
		public string Profession { get; set; }
		public string Token { get; set; }
		public object ResponsiveEmpId { get; set; }
		public object ModifiedDate { get; set; }
		public string City { get; set; }
		public object IsFirstLogin { get; set; }
		public long RowID { get; set; }
	}
}
