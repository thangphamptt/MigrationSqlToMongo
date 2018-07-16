using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDatabaseHrToolv1.Model
{
	public class Skill
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public object CandidateId { get; set; }
        public object EmployeeId { get; set; }
        public string SkillName { get; set; }
        public object LevelId { get; set; }
        public object Year { get; set; }
        public object LastUsed { get; set; }
		public long RowID { get; set; }
	}
}
