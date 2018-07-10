using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class InterviewSchedule
    {
        public int Id { get; set; }
        public DateTime? FromBookRoomDate { get; set; }
        public DateTime? ToBookRoomDate { get; set; }
        public string ContentSchedule { get; set; }
        public string Location { get; set; }
        public int? RoomId { get; set; }
        public string Title { get; set; }
        public TimeSpan? FromBookRoomTime { get; set; }
        public TimeSpan? ToBookRoomTime { get; set; }
        public int InterviewId { get; set; }
        public int CandidateId { get; set; }
        public DateTime? FromTechnicalDate { get; set; }
        public DateTime? ToTechnicalDate { get; set; }
        public TimeSpan? FromTechnicalTime { get; set; }
        public TimeSpan? ToTechnicalTime { get; set; }
        public int? EmployeeId { get; set; }
        public int SendMailToCandidate { get; set; }
        public int SendMailToInterviewer { get; set; }
        public string AppointmentId { get; set; }

        public Candidate Candidate { get; set; }
        public Employee Employee { get; set; }
        public Interview Interview { get; set; }
        public Room Room { get; set; }
    }
}
