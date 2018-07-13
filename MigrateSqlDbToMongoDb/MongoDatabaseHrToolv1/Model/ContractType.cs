using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDatabaseHrToolv1.Model
{
    public class ContractType
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("Id")]
        public int ExternalId { get; set; }
        public string Code { get; set; }
        public string CodeView { get; set; }
        public string Name { get; set; }
        public string NameView { get; set; }
        public int? Month { get; set; }
        public string Note { get; set; }
        public long RowID { get; set; }
    }
}
