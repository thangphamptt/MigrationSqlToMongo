using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Job
    {
        public Job()
        {
            CandidateSignProcess = new HashSet<CandidateSignProcess>();
            ContractCode = new HashSet<ContractCode>();
            Interview = new HashSet<Interview>();
            JobApplication = new HashSet<JobApplication>();
            JobStatus = new HashSet<JobStatus>();
            RecruitmentTemplate = new HashSet<RecruitmentTemplate>();
        }

        public int Id { get; set; }
        public int PositionId { get; set; }
        public int Quantity { get; set; }
        public string JobCode { get; set; }
        public string Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CompanyId { get; set; }
        public string JobTitle { get; set; }

        public Company Company { get; set; }
        public Position Position { get; set; }
        public ICollection<CandidateSignProcess> CandidateSignProcess { get; set; }
        public ICollection<ContractCode> ContractCode { get; set; }
        public ICollection<Interview> Interview { get; set; }
        public ICollection<JobApplication> JobApplication { get; set; }
        public ICollection<JobStatus> JobStatus { get; set; }
        public ICollection<RecruitmentTemplate> RecruitmentTemplate { get; set; }
    }
}
