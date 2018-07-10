using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class JobApplication
    {
        public JobApplication()
        {
            CandidateSignProcess = new HashSet<CandidateSignProcess>();
            ContractCode = new HashSet<ContractCode>();
            EmailTracking = new HashSet<EmailTracking>();
            Employee = new HashSet<Employee>();
            Interview = new HashSet<Interview>();
            JobApplicationAttachment = new HashSet<JobApplicationAttachment>();
            ScreeningCvhistory = new HashSet<ScreeningCvhistory>();
        }

        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int? PositionId { get; set; }
        public string UrlDocument { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ValidTo { get; set; }
        public decimal? ExperienceYear { get; set; }
        public string WorkExperience { get; set; }
        public int? ApplicationStatus { get; set; }
        public decimal? SalaryOffer { get; set; }
        public string Note { get; set; }
        public int CompanyId { get; set; }
        public int? CvsourceId { get; set; }
        public bool? IsCandidateUpdated { get; set; }
        public int? GroupIq { get; set; }
        public int? GroupTechnical { get; set; }
        public byte? NumberRoundInterview { get; set; }
        public DateTime? StartDateSuggest { get; set; }
        public string NoteStartDateSuggest { get; set; }
        public int? JobId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? CurrentSalary { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public decimal? SuggestedSalary { get; set; }
        public int? OverallStatus { get; set; }
        public int? RoundStatus { get; set; }
        public int? Rating { get; set; }
        public bool? IsSendMail { get; set; }
        public string OfferPositionName { get; set; }
        public DateTime? RatingUpdatedDate { get; set; }

        public Candidate Candidate { get; set; }
        public Company Company { get; set; }
        public Job Job { get; set; }
        public Position Position { get; set; }
        public ICollection<CandidateSignProcess> CandidateSignProcess { get; set; }
        public ICollection<ContractCode> ContractCode { get; set; }
        public ICollection<EmailTracking> EmailTracking { get; set; }
        public ICollection<Employee> Employee { get; set; }
        public ICollection<Interview> Interview { get; set; }
        public ICollection<JobApplicationAttachment> JobApplicationAttachment { get; set; }
        public ICollection<ScreeningCvhistory> ScreeningCvhistory { get; set; }
    }
}
