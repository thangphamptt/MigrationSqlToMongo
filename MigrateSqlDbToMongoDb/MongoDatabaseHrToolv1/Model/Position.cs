using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDatabaseHrToolv1.Model
{
    public class Position
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("Id")]
        public int ExternalId { get; set; }
        public string PositionName { get; set; }
        public string PositionNameVN { get; set; }
        public string CareerNameVN { get; set; }
        public string Note { get; set; }
        public string Code { get; set; }
        public bool CEOGroup { get; set; }
        public int? CompanyId { get; set; }
        public long RowID { get; set; }
    }
}
