using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class JobStatus
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Note { get; set; }
        public int? ModifiedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedUsername { get; set; }

        public Job Job { get; set; }
    }
}
