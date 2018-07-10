using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class CandidateJob
    {
        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public int JobId { get; set; }
        public DateTime? StartDateSuggest { get; set; }
        public string NoteStartDateSuggest { get; set; }
    }
}
