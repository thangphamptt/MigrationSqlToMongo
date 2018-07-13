using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDatabaseHrToolv1.Model
{
	public class EmploymentHistory
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public string Position { get; set; }
        public string Company { get; set; }
        public object StartTime { get; set; }
        public object EndTime { get; set; }
        public string Address { get; set; }
        public string Supervisor { get; set; }
        public string SupervisorEmail { get; set; }
        public string SupervisorPhoneNumber { get; set; }
        public object Salary { get; set; }
        public string ReasonForLeaving { get; set; }
        public string Responsibilities { get; set; }
        public object CandidateId { get; set; }
        public object EmployeeId { get; set; }
        public string Website { get; set; }
		public long RowID { get; set; }
	}
}
