using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Contract
    {
        public Contract()
        {
            ContractAllowances = new HashSet<ContractAllowances>();
            ContractBonus = new HashSet<ContractBonus>();
        }

        public int Id { get; set; }
        public int ContractTypeId { get; set; }
        public int ContractNumber { get; set; }
        public string Code1 { get; set; }
        public string RefCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int RepresentativeEmployeeId { get; set; }
        public int LabourEmployeeId { get; set; }
        public string ReportTo { get; set; }
        public string ProbationSalary { get; set; }
        public string NetSalary { get; set; }
        public string InsuranceSalary { get; set; }
        public string Note { get; set; }
        public string UrlContract { get; set; }
        public bool? IsFreshGraduated { get; set; }
        public int? MonthTrail { get; set; }
        public string SalaryIncrease { get; set; }
        public int? Month { get; set; }
        public bool? IsValid { get; set; }

        public ContractType ContractType { get; set; }
        public Employee LabourEmployee { get; set; }
        public Employee RepresentativeEmployee { get; set; }
        public ICollection<ContractAllowances> ContractAllowances { get; set; }
        public ICollection<ContractBonus> ContractBonus { get; set; }
    }
}
