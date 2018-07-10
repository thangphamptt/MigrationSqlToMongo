using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class RolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Permissions Permission { get; set; }
        public Role Role { get; set; }
    }
}
