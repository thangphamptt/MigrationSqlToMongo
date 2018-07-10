using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class DayOffType
    {
        public DayOffType()
        {
            DayOff = new HashSet<DayOff>();
        }

        public int DtpId { get; set; }
        public string DotName { get; set; }
        public string Note { get; set; }

        public ICollection<DayOff> DayOff { get; set; }
    }
}
