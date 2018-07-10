using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class RecruitmentTemplate
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int CompanyId { get; set; }
        public int PositionId { get; set; }
        public string CompanyName { get; set; }
        public string LogoUrl { get; set; }
        public string JobTitle { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanySize { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public string Cvlanguage { get; set; }
        public string JobLevel { get; set; }
        public string JobCategory { get; set; }
        public string Location { get; set; }
        public string Salary { get; set; }
        public string JobType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ValidTo { get; set; }
        public string JobNote { get; set; }
        public string Phone { get; set; }

        public Job Job { get; set; }
    }
}
