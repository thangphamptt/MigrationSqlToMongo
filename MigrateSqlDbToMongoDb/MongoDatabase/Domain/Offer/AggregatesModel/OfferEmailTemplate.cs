using MongoDatabase.Domain.Common;
using System.Collections.Generic;

namespace MongoDatabase.Domain.Offer.AggregatesModel
{
	public class OfferEmailTemplate : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public string OrganizationalUnitId { get; set; }
        public bool IsDisabled { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IList<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
