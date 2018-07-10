using System.Collections.Generic;
using System;
using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Collaboration.AggregatesModel
{
	public class Activity : IEntity
    {
        public string Id { get; set; }
        public Dictionary<string, string> ActivityFor { get; set; } = new Dictionary<string, string>();
        public string ActivityType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        public string Text { get; set; }
        public string ReplyToActivityId { get; set; }
        public string UserId { get; set; }
        public IList<string> AgreedUserIds { get; set; } = new List<string>();
    }
}
