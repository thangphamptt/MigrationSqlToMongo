using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDatabaseHrToolv1.Model
{
	public class EmailTracking
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public int JobApplicationId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public object SendingTime { get; set; }
        public string TypeOfEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Inbox { get; set; }
		public long RowID { get; set; }
	}
}
