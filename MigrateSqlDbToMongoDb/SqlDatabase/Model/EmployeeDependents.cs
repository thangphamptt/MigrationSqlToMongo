using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EmployeeDependents
    {
        public int EdpId { get; set; }
        public int EdpEmplId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ValidTo { get; set; }

        public Employee EdpEmpl { get; set; }
    }
}
