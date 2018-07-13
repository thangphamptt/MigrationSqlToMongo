using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDatabaseHrToolv1.Model
{
    public class LetterTemplate
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("Id")]
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Parameter { get; set; }
        public string Note { get; set; }
        public string Type { get; set; }
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string CcEmail { get; set; }
        public string Subject { get; set; }
        public string BccEmail { get; set; }
        public object CompanyId { get; set; }
        public long RowID { get; set; }
    }
}
