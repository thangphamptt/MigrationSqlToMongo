using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EmployeeInsurance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int InsuranceId { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TotalMoney { get; set; }
        public decimal? PercentCompanyPay { get; set; }
        public decimal? PercentEmployeePay { get; set; }
        public decimal? MoneyCompanyPay { get; set; }
        public decimal? MoneyEmployeePay { get; set; }
        public string Note { get; set; }

        public Employee Employee { get; set; }
        public Insurance Insurance { get; set; }
    }
}
