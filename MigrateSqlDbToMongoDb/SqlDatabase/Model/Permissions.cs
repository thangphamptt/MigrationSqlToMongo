using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Permissions
    {
        public Permissions()
        {
            Actions = new HashSet<Actions>();
            RolePermission = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public int FormId { get; set; }
        public string PermissionName { get; set; }
        public bool? IsActive { get; set; }
        public int? Order { get; set; }

        public Forms Form { get; set; }
        public ICollection<Actions> Actions { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
    }
}
