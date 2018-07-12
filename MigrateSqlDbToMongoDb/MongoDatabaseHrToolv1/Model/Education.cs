using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDatabaseHrToolv1.Model
{
	public class Education
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public string School { get; set; }
        public object From { get; set; }
        public object To { get; set; }
        public object CandidateId { get; set; }
        public object EmployeeId { get; set; }
        public string Field { get; set; }
        public string SchoolLevel { get; set; }
        public string Country { get; set; }
		public long RowID { get; set; }
	}
}
