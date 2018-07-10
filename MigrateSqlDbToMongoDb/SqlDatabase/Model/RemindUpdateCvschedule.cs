using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class RemindUpdateCvschedule
    {
        public int Id { get; set; }
        public DateTime SentDate { get; set; }
        public string Status { get; set; }
        public int? TotalEmailSent { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
