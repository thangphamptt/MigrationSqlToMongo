using MongoDatabase.Domain.Common;
using System;
using System.Collections.Generic;

namespace MongoDatabase.Domain.Email.AggregatesModel
{
	public abstract class Email : IAggregateRoot, IEntity
	{
        public string Id { get; set; }

		public string Body { get; set; }

		public string Subject { get; set; }

		public string Sender { get; set; }

		public IList<string> Recipients { get; set; }

		public IList<string> Ccs { get; set; } = new List<string>();

		public IList<string> Bccs { get; set; } = new List<string>();

		public IList<Attachment> Attachments { get; set; }

		public DateTime SentDate { get; set; } = DateTime.Now;

		public IList<string> ReadByUserIds { get; set; } = new List<string>();
	}
}
