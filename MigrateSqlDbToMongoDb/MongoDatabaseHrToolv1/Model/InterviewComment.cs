using System;
using System.Collections.Generic;

namespace MongoDatabaseHrToolv1.Model
{
    public partial class InterviewComment
    {
        public int Id { get; set; }
        public int InterviewId { get; set; }
        public string Comment { get; set; }
        public int? Ordinate { get; set; }
        public string Icon { get; set; }
        public double? CommentTime { get; set; }
        public string InterviewerName { get; set; }

        public Interview Interview { get; set; }
    }
}
