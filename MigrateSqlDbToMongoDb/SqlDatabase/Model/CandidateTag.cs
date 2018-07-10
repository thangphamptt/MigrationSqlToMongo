using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class CandidateTag
    {
        public int TagId { get; set; }
        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }
        public CandidateTag CandidateTagNavigation { get; set; }
        public TagInfo Tag { get; set; }
        public CandidateTag InverseCandidateTagNavigation { get; set; }
    }
}
