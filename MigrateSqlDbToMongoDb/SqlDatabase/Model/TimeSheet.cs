using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class TimeSheet
    {
        public int TmsId { get; set; }
        public string TmsCode { get; set; }
        public DateTime TmsDate { get; set; }
        public TimeSpan TmsTime { get; set; }
        public long Type { get; set; }
        public string TmsMachineCode { get; set; }
    }
}
