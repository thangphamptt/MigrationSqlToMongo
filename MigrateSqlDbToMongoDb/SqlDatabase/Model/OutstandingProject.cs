using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class OutstandingProject
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Position { get; set; }
        public string GeneralInformation { get; set; }
        public string Description { get; set; }
        public string InterfaceSystem { get; set; }
        public string TechnologyUsed { get; set; }
        public int? CandidateId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
