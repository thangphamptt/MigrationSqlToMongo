using System.Collections.Generic;

namespace MongoDatabase.Domain.Template.AggregatesModel
{
    public class EmailTemplate : Template
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public EmailTemplateType? Type { get; set; }

        public string SubType { get; set; }

        public IList<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
