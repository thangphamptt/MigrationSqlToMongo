using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class ContractCode
    {
        public ContractCode()
        {
            CandidateSignProcess = new HashSet<CandidateSignProcess>();
            EmployeeNavigation = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public int JobId { get; set; }
        public int Number { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string Type { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? IssuingDate { get; set; }
        public string NoteIssuing { get; set; }
        public DateTime? SendingDate { get; set; }
        public string NoteSending { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? SigningDate { get; set; }
        public DateTime? WorkingStartDate { get; set; }
        public string NoteWorkingStartDate { get; set; }
        public bool ViewScreenCandidate { get; set; }
        public string SalaryOffer { get; set; }
        public string BasicSalaryOffer { get; set; }
        public int? PercentageSalaryOffer { get; set; }
        public string NoteOffer { get; set; }
        public string AnotherAgree { get; set; }
        public string ReportTo { get; set; }
        public bool? IsAcceptConfirm { get; set; }
        public string NoteConfirm { get; set; }
        public bool? IsAcceptSigning { get; set; }
        public string NoteSigning { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool? IsFreshGraduated { get; set; }
        public int? MonthTrail { get; set; }
        public string SalaryIncrease { get; set; }
        public string Allowance { get; set; }
        public int? MonthProbation { get; set; }
        public DateTime? AcceptDate { get; set; }
        public string NoteAccept { get; set; }
        public string OfferLetterFile { get; set; }

        public Employee Employee { get; set; }
        public Job Job { get; set; }
        public JobApplication JobApplication { get; set; }
        public ICollection<CandidateSignProcess> CandidateSignProcess { get; set; }
        public ICollection<Employee> EmployeeNavigation { get; set; }
    }
}
