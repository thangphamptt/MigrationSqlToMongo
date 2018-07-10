using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Company
    {
        public Company()
        {
            Employee = new HashSet<Employee>();
            EmployeeEmail = new HashSet<EmployeeEmail>();
            EtagHandler = new HashSet<EtagHandler>();
            Job = new HashSet<Job>();
            JobApplication = new HashSet<JobApplication>();
            LetterTemplate = new HashSet<LetterTemplate>();
            Position = new HashSet<Position>();
            PositionTemplate = new HashSet<PositionTemplate>();
            Role = new HashSet<Role>();
            Template = new HashSet<Template>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EmailAllEmpl { get; set; }
        public string Email { get; set; }
        public string PassEmail { get; set; }
        public string UserEmail { get; set; }
        public string Host { get; set; }
        public string Mobile { get; set; }
        public string Address2 { get; set; }
        public string Hrmail { get; set; }
        public string PassHrmail { get; set; }
        public string Itmail { get; set; }
        public string PassItmail { get; set; }
        public string OffterTemplatePath { get; set; }
        public string AgreementTemplatePath { get; set; }
        public string ProbationTemplatePath { get; set; }
        public string ContractTemplatePath { get; set; }
        public int LastJobCode { get; set; }
        public string DescriptionFooterEmail { get; set; }
        public string Website { get; set; }
        public int LastRefCode { get; set; }
        public string CompanyBenefit { get; set; }
        public int? CustomerAdminId { get; set; }
        public string HrDepartmentEmail { get; set; }
        public string HrDepartmentDisplayName { get; set; }
        public string EmailNameForSend { get; set; }

        public ICollection<Employee> Employee { get; set; }
        public ICollection<EmployeeEmail> EmployeeEmail { get; set; }
        public ICollection<EtagHandler> EtagHandler { get; set; }
        public ICollection<Job> Job { get; set; }
        public ICollection<JobApplication> JobApplication { get; set; }
        public ICollection<LetterTemplate> LetterTemplate { get; set; }
        public ICollection<Position> Position { get; set; }
        public ICollection<PositionTemplate> PositionTemplate { get; set; }
        public ICollection<Role> Role { get; set; }
        public ICollection<Template> Template { get; set; }
    }
}
