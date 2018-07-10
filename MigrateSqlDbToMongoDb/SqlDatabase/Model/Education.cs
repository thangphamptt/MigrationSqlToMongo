using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Education
    {
        public int Id { get; set; }
        public string School { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? CandidateId { get; set; }
        public int? EmployeeId { get; set; }
        public string Field { get; set; }
        public string SchoolLevel { get; set; }
        public string Country { get; set; }
    }
}
