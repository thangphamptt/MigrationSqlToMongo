using System.Collections.Generic;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
    public class ThankYouEmailTemplate: Template
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public string Type { get; set; }

        public IList<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
