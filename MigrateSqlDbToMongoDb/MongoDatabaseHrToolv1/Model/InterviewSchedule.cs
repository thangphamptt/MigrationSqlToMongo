using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDatabaseHrToolv1.Model
{
	public partial class InterviewSchedule
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public object FromBookRoomDate { get; set; }
        public object ToBookRoomDate { get; set; }
        public string ContentSchedule { get; set; }
        public string Location { get; set; }
        public object RoomId { get; set; }
        public string Title { get; set; }
        public object FromBookRoomTime { get; set; }
        public object ToBookRoomTime { get; set; }
        public int InterviewId { get; set; }
        public int CandidateId { get; set; }
        public object FromTechnicalDate { get; set; }
        public object ToTechnicalDate { get; set; }
        public object FromTechnicalTime { get; set; }
        public object ToTechnicalTime { get; set; }
        public object EmployeeId { get; set; }
        public int SendMailToCandidate { get; set; }
        public int SendMailToInterviewer { get; set; }
        public string AppointmentId { get; set; }
		public long RowID { get; set; }
	}
}
