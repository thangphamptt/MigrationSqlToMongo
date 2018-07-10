using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Insurance
    {
        public Insurance()
        {
            EmployeeInsurance = new HashSet<EmployeeInsurance>();
        }

        public int Id { get; set; }
        public int InsuranceTypeId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? PercentCompanyPay { get; set; }
        public decimal? PercentEmployeePay { get; set; }
        public int? CompanyId { get; set; }

        public InsuranceType InsuranceType { get; set; }
        public ICollection<EmployeeInsurance> EmployeeInsurance { get; set; }
    }
}
