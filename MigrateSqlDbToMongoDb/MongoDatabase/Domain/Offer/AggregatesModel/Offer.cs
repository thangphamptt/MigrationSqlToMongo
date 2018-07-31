using MongoDatabase.Domain.Common;
using System;

namespace MongoDatabase.Domain.Offer.AggregatesModel
{
	public class Offer : IAggregateRoot, IEntity
    {
        public string Id { get; set; }
        public string CandidateId { get; set; }
        public string ApplicationId { get; set; }
        public string JobId { get; set; }
        public bool? Status { get; set; }
        public string OrganizationalUnitId { get; set; }
        public string ReportTo { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string CurrencyId { get; set; }
        public bool IsUpdated { get; set; }
        public DateTime? SentDate { get; set; }
        public string SentByUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedByUserId { get; set; }
    }
}
