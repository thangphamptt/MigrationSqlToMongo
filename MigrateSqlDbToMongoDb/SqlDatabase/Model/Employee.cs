using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Employee
    {
        public Employee()
        {
            Candidate = new HashSet<Candidate>();
            CandidateNote = new HashSet<CandidateNote>();
            ContractCode = new HashSet<ContractCode>();
            ContractLabourEmployee = new HashSet<Contract>();
            ContractRepresentativeEmployee = new HashSet<Contract>();
            DayOffDofEmplIdapprovedNavigation = new HashSet<DayOff>();
            DayOffDofEmplIdrequestedNavigation = new HashSet<DayOff>();
            DayOffDofEmplIdverifiedNavigation = new HashSet<DayOff>();
            Dependents = new HashSet<Dependents>();
            EmployeeDependents = new HashSet<EmployeeDependents>();
            EmployeeEmail = new HashSet<EmployeeEmail>();
            EmployeeHistoryAction = new HashSet<EmployeeHistoryAction>();
            EmployeeInsurance = new HashSet<EmployeeInsurance>();
            EmployeeRole = new HashSet<EmployeeRole>();
            EmployeeTax = new HashSet<EmployeeTax>();
            History = new HashSet<History>();
            InterviewSchedule = new HashSet<InterviewSchedule>();
            InverseCreatedUserNavigation = new HashSet<Employee>();
            ProbationAppraisal = new HashSet<ProbationAppraisal>();
            ScreeningCvhistory = new HashSet<ScreeningCvhistory>();
            WorkExperience = new HashSet<WorkExperience>();
            WorkingGroupEmployee = new HashSet<WorkingGroupEmployee>();
        }

        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string TimeSheetCode { get; set; }
        public bool? IsActive { get; set; }
        public string FirstName { get; set; }
        public string FirstNameEn { get; set; }
        public string LastName { get; set; }
        public string LastNameEn { get; set; }
        public DateTime? Birthday { get; set; }
        public string BirthPlace { get; set; }
        public string PermanentAddress { get; set; }
        public string TemporaryAddress { get; set; }
        public string Origin { get; set; }
        public string IdCard { get; set; }
        public DateTime? IdProvidedDate { get; set; }
        public string IdProvidedPlace { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyContact { get; set; }
        public string Skype { get; set; }
        public string PrivateEmail { get; set; }
        public string CompanyEmail { get; set; }
        public string Education { get; set; }
        public string TaxCode { get; set; }
        public string SocialInsuranceCode { get; set; }
        public DateTime? SocialInsuranceIssue { get; set; }
        public string SocialInsurancePlace { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public DateTime? WorkEndDate { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string Note { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? EmplNatId { get; set; }
        public int? JobApplicationId { get; set; }
        public int? CompanyId { get; set; }
        public int? ContractCodeId { get; set; }
        public string Photograph { get; set; }
        public int PositionId { get; set; }
        public string AgreementAdd { get; set; }
        public string OfferLetterAddSigned { get; set; }
        public string Status { get; set; }
        public int? RefCode { get; set; }
        public string FooterEmail { get; set; }
        public bool HasInsuranceAccident { get; set; }
        public int EmplTshEmplCode { get; set; }
        public string Profession { get; set; }
        public string Domain { get; set; }
        public byte? ProbationPeriod { get; set; }
        public int? CandidateId { get; set; }
        public string Salt { get; set; }
        public int FlagUserName { get; set; }
        public decimal? AnnualLeave { get; set; }
        public decimal? SickLeave { get; set; }
        public string Branch { get; set; }
        public decimal? AnnualLeaveRequest { get; set; }
        public decimal? SickLeaveRequest { get; set; }
        public decimal? UnpaidLeave { get; set; }
        public decimal? UnpaidLeaveRequest { get; set; }
        public string BalanceNote { get; set; }

        public Company Company { get; set; }
        public ContractCode ContractCodeNavigation { get; set; }
        public Employee CreatedUserNavigation { get; set; }
        public JobApplication JobApplication { get; set; }
        public Position Position { get; set; }
        public ICollection<Candidate> Candidate { get; set; }
        public ICollection<CandidateNote> CandidateNote { get; set; }
        public ICollection<ContractCode> ContractCode { get; set; }
        public ICollection<Contract> ContractLabourEmployee { get; set; }
        public ICollection<Contract> ContractRepresentativeEmployee { get; set; }
        public ICollection<DayOff> DayOffDofEmplIdapprovedNavigation { get; set; }
        public ICollection<DayOff> DayOffDofEmplIdrequestedNavigation { get; set; }
        public ICollection<DayOff> DayOffDofEmplIdverifiedNavigation { get; set; }
        public ICollection<Dependents> Dependents { get; set; }
        public ICollection<EmployeeDependents> EmployeeDependents { get; set; }
        public ICollection<EmployeeEmail> EmployeeEmail { get; set; }
        public ICollection<EmployeeHistoryAction> EmployeeHistoryAction { get; set; }
        public ICollection<EmployeeInsurance> EmployeeInsurance { get; set; }
        public ICollection<EmployeeRole> EmployeeRole { get; set; }
        public ICollection<EmployeeTax> EmployeeTax { get; set; }
        public ICollection<History> History { get; set; }
        public ICollection<InterviewSchedule> InterviewSchedule { get; set; }
        public ICollection<Employee> InverseCreatedUserNavigation { get; set; }
        public ICollection<ProbationAppraisal> ProbationAppraisal { get; set; }
        public ICollection<ScreeningCvhistory> ScreeningCvhistory { get; set; }
        public ICollection<WorkExperience> WorkExperience { get; set; }
        public ICollection<WorkingGroupEmployee> WorkingGroupEmployee { get; set; }
    }
}
