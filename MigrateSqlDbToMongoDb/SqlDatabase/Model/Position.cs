using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Position
    {
        public Position()
        {
            Employee = new HashSet<Employee>();
            Job = new HashSet<Job>();
            JobApplication = new HashSet<JobApplication>();
            PositionTemplate = new HashSet<PositionTemplate>();
            WorkExperience = new HashSet<WorkExperience>();
        }

        public int Id { get; set; }
        public string PositionName { get; set; }
        public string PositionNameVn { get; set; }
        public string CareerNameVn { get; set; }
        public string Note { get; set; }
        public string Code { get; set; }
        public bool Ceogroup { get; set; }
        public int? CompanyId { get; set; }

        public Company Company { get; set; }
        public ICollection<Employee> Employee { get; set; }
        public ICollection<Job> Job { get; set; }
        public ICollection<JobApplication> JobApplication { get; set; }
        public ICollection<PositionTemplate> PositionTemplate { get; set; }
        public ICollection<WorkExperience> WorkExperience { get; set; }
    }
}
