using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDatabaseHrToolv1.Model
{
	public class JobStatus
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public int JobId { get; set; }
		public object Status { get; set; }
		public object CreatedDate { get; set; }
		public object ValidTo { get; set; }
		public object Note { get; set; }
		public object ModifiedUserId { get; set; }
		public object ModifiedDate { get; set; }
		public object ModifiedUsername { get; set; }
		public long RowID { get; set; }
	}
}
