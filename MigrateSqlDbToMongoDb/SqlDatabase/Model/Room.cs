using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Room
    {
        public Room()
        {
            InterviewSchedule = new HashSet<InterviewSchedule>();
        }

        public int Id { get; set; }
        public string RoomName { get; set; }
        public string RoomCode { get; set; }
        public string Email { get; set; }

        public ICollection<InterviewSchedule> InterviewSchedule { get; set; }
    }
}
