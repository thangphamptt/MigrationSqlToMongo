using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDatabaseHrToolv1.Model
{
	public class OutstandingProject
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public string ProjectName { get; set; }
        public object StartTime { get; set; }
        public object EndTime { get; set; }
        public string Position { get; set; }
        public string GeneralInformation { get; set; }
        public string Description { get; set; }
        public string InterfaceSystem { get; set; }
        public string TechnologyUsed { get; set; }
        public object CandidateId { get; set; }
        public object EmployeeId { get; set; }
		public long RowID { get; set; }
	}
}
