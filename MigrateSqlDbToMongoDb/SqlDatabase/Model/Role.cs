using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Role
    {
        public Role()
        {
            CandidateRole = new HashSet<CandidateRole>();
            EmployeeRole = new HashSet<EmployeeRole>();
            RolePermission = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Note { get; set; }
        public string RoleType { get; set; }
        public int? CompanyId { get; set; }

        public Company Company { get; set; }
        public ICollection<CandidateRole> CandidateRole { get; set; }
        public ICollection<EmployeeRole> EmployeeRole { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
    }
}
