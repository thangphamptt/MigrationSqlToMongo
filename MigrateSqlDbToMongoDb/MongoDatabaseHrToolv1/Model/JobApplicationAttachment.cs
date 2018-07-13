using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDatabaseHrToolv1.Model
{
	public class JobApplicationAttachment
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		[BsonElement("Id")]
		public int ExternalId { get; set; }
		public int JobApplicationId { get; set; }
        public string Path { get; set; }
        public string Filename { get; set; }
        public object IsIndex { get; set; }
        public string FileType { get; set; }
		public long RowID { get; set; }
	}
}
