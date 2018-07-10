using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EmailTracking
    {
        public EmailTracking()
        {
            EmailTrackingAttachment = new HashSet<EmailTrackingAttachment>();
        }

        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime? SendingTime { get; set; }
        public string TypeOfEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Inbox { get; set; }

        public JobApplication JobApplication { get; set; }
        public ICollection<EmailTrackingAttachment> EmailTrackingAttachment { get; set; }
    }
}
