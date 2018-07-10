using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class MarkOnlineTest
    {
        public int JobApplicationId { get; set; }
        public string Iqmark { get; set; }
        public string TechnicalMark { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
