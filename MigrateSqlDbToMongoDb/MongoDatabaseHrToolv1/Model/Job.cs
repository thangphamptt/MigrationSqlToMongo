using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDatabaseHrToolv1.Model
{
    public class Job
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("Id")]
        public int ExternalId { get; set; }
        public int PositionId { get; set; }
        public int Quantity { get; set; }
        public string JobCode { get; set; }
        public string Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CompanyId { get; set; }
        public string JobTitle { get; set; }
        public long RowID { get; set; }
    }
}
