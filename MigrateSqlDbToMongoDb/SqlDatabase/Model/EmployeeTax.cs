using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EmployeeTax
    {
        public EmployeeTax()
        {
            EmployeeTaxDependent = new HashSet<EmployeeTaxDependent>();
        }

        public int EmplTaxId { get; set; }
        public int? EmplTaxEmplId { get; set; }
        public int? EmplTaxPerTaxId { get; set; }
        public decimal? TotalMoney { get; set; }
        public decimal? MoneyTax { get; set; }
        public decimal MoneyReductFamilyCir { get; set; }
        public decimal? MoneyDependentReduct { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Employee EmplTaxEmpl { get; set; }
        public PersonalIncomeTax EmplTaxPerTax { get; set; }
        public ICollection<EmployeeTaxDependent> EmployeeTaxDependent { get; set; }
    }
}
