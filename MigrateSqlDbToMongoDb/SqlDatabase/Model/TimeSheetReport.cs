using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class TimeSheetReport
    {
        public int TmsRptId { get; set; }
        public string TmsRptCode { get; set; }
        public DateTime TmsRptDate { get; set; }
        public TimeSpan TimeIn { get; set; }
        public DateTime DateOut { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public TimeSpan TotalWorkingTime { get; set; }
        public string TmsRptMachineCode { get; set; }
    }
}
