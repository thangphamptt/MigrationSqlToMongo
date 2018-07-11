using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDatabaseHrToolv1.Model
{
    public class RecruitmentTemplate
    {

        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("Id")]
        public int ExternalId { get; set; }
        public int JobId { get; set; }
        public int CompanyId { get; set; }
        public int PositionId { get; set; }
        public string CompanyName { get; set; }
        public string LogoUrl { get; set; }
        public string JobTitle { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanySize { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public string CVLanguage { get; set; }
        public string JobLevel { get; set; }
        public string JobCategory { get; set; }
        public string Location { get; set; }
        public string Salary { get; set; }
        public string JobType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ValidTo { get; set; }
        public string JobNote { get; set; }
        public string Phone { get; set; }
        public long RowID { get; set; }
    }
}
