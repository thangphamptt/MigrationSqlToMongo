using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Interview
    {
        public Interview()
        {
            InterviewComment = new HashSet<InterviewComment>();
            InterviewSchedule = new HashSet<InterviewSchedule>();
        }

        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public int InterviewRoundId { get; set; }
        public int JobId { get; set; }
        public int? Result { get; set; }
        public string Note { get; set; }
        public DateTime? DateInterview { get; set; }
        public DateTime? DateFeedbackPhone { get; set; }
        public DateTime? DateFeedbackEmail { get; set; }
        public DateTime? ValidTo { get; set; }
        public int? Interviewer { get; set; }
        public int? Creater { get; set; }
        public string ExpectedSalary { get; set; }
        public string SuggestedSalary { get; set; }
        public string RecordingPath { get; set; }
        public string Summary { get; set; }
        public int? Rating { get; set; }
        public bool? IsFinished { get; set; }
        public string Comment { get; set; }
        public int? Match { get; set; }
        public double? DurationTime { get; set; }
        public bool? IsCanceled { get; set; }

        public InterviewRound InterviewRound { get; set; }
        public Job Job { get; set; }
        public JobApplication JobApplication { get; set; }
        public ICollection<InterviewComment> InterviewComment { get; set; }
        public ICollection<InterviewSchedule> InterviewSchedule { get; set; }
    }
}
