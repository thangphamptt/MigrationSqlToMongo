using MongoDatabase.Domain.Common;
using System;
using System.Collections.Generic;

namespace MongoDatabase.Domain.Schedule.AggregatesModel
{
	public class Appointment : IAggregateRoot, IEntity
    {
        public string Id { get; set; }

        public string OrganizerId { get; set; }

        public string Subject { get; set; }

        public string Location { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Description { get; set; }

        public string AppointmentType { get; set; }

        public int Duration { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string Status { get; set; }

        public IList<User> Attendees { get; set; }

        public string CandidateId { get; set; }

        public string ScheduleId { get; set; }

        public string Interviewer { get; set; }

        public string InterviewType { get; set; }
    }
}
