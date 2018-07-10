using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class PersonalIncomeTax
    {
        public PersonalIncomeTax()
        {
            EmployeeTax = new HashSet<EmployeeTax>();
        }

        public int PerTaxId { get; set; }
        public decimal? PercentTax { get; set; }
        public decimal? PercentReductFarmilyCir { get; set; }
        public decimal? DependentReduct { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<EmployeeTax> EmployeeTax { get; set; }
    }
}
