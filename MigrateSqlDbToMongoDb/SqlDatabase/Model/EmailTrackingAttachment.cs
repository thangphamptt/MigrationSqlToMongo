using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EmailTrackingAttachment
    {
        public int Id { get; set; }
        public int EmailTrackingId { get; set; }
        public string Path { get; set; }
        public string Filename { get; set; }

        public EmailTracking EmailTracking { get; set; }
    }
}
