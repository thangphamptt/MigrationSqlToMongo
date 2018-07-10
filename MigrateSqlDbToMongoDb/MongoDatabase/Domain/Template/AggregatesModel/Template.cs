using MongoDatabase.Domain.Common;
using System;

namespace MongoDatabase.Domain.Template.AggregatesModel
{
	public abstract class Template : IAggregateRoot, IEntity
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public bool IsSystem { get; set; }

		public string OrganizationalUnitId { get; set; }

		public TemplateStatus Status { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? CreatedDate { get; set; }

		public string CreatedByUserId { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public string ModifiedByUserId { get; set; }
	}
}
