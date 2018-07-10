using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class CandidateNote
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string Source { get; set; }
        public string Note { get; set; }
        public int CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Candidate Candidate { get; set; }
        public Employee CreatedUserNavigation { get; set; }
    }
}
