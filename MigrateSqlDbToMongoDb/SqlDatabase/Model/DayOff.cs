using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class DayOff
    {
        public int DofId { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartHour { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndHour { get; set; }
        public int DofEmplIdrequested { get; set; }
        public int DofEmplIdverified { get; set; }
        public int DofEmplIdapproved { get; set; }
        public bool? IsCancel { get; set; }
        public int DofDtpId { get; set; }

        public DayOffType DofDtp { get; set; }
        public Employee DofEmplIdapprovedNavigation { get; set; }
        public Employee DofEmplIdrequestedNavigation { get; set; }
        public Employee DofEmplIdverifiedNavigation { get; set; }
    }
}
