using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Actions
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public string ActionApi { get; set; }
        public string ActionName { get; set; }
        public string MethodName { get; set; }

        public Permissions Permission { get; set; }
    }
}
