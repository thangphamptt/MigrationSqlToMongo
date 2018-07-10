using System;

namespace MongoDatabase.Domain.Interview.AggregatesModel
{
	public class Schedule
    {
        public DateTime TimeFrom { get; set; }
        public int Duration { get; set; }
        public string AssessmentType{ get; set; }
        public string Interviewer { get; set; }
        public string Location { get; set; }
    }
}
