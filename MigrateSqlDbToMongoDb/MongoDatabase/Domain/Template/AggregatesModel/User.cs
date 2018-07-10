﻿using MongoDatabase.Domain.Common;
using System.Collections.Generic;

namespace MongoDatabase.Domain.Template.AggregatesModel
{
	public class User : IAggregateRoot, IEntity
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public IList<string> OrganizationalUnitIds { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
	}
}
