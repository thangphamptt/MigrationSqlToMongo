using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EmployeeTaxDependent
    {
        public int EmplTaxId { get; set; }
        public int DepId { get; set; }

        public Dependents Dep { get; set; }
        public EmployeeTax EmplTax { get; set; }
    }
}
