using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EmploymentHistory
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Address { get; set; }
        public string Supervisor { get; set; }
        public string SupervisorEmail { get; set; }
        public string SupervisorPhoneNumber { get; set; }
        public int? Salary { get; set; }
        public string ReasonForLeaving { get; set; }
        public string Responsibilities { get; set; }
        public int? CandidateId { get; set; }
        public int? EmployeeId { get; set; }
        public string Website { get; set; }
    }
}
