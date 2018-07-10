using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Forms
    {
        public Forms()
        {
            Permissions = new HashSet<Permissions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool? IsActive { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }
        public int? Order { get; set; }

        public ICollection<Permissions> Permissions { get; set; }
    }
}
