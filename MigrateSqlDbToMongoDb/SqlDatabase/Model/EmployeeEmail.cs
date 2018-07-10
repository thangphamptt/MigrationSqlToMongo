using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EmployeeEmail
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }

        public Company Company { get; set; }
        public Employee Employee { get; set; }
    }
}
