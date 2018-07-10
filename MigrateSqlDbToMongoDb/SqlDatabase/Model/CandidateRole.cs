using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class CandidateRole
    {
        public int CandidateId { get; set; }
        public int RoleId { get; set; }

        public Candidate Candidate { get; set; }
        public Role Role { get; set; }
    }
}
