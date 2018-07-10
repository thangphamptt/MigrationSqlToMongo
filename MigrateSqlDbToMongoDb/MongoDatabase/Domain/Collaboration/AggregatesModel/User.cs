using MongoDatabase.Domain.Common;
using System.Collections.Generic;

namespace MongoDatabase.Domain.Collaboration.AggregatesModel
{
	public class User : IAggregateRoot, IEntity
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ImagePath { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public IList<string> OrganizationalUnitIds { get; set; }

        public string Email { get; set; }

    }
}
