using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Dependents
    {
        public Dependents()
        {
            EmployeeTaxDependent = new HashSet<EmployeeTaxDependent>();
        }

        public int DepId { get; set; }
        public string DepName { get; set; }
        public string Relationship { get; set; }
        public int DepEmplId { get; set; }

        public Employee DepEmpl { get; set; }
        public ICollection<EmployeeTaxDependent> EmployeeTaxDependent { get; set; }
    }
}
