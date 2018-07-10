using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class HistoryActionCandidate
    {
        public int CanHisId { get; set; }
        public string Type { get; set; }
        public DateTime DateConfirm { get; set; }
        public DateTime? ValidTo { get; set; }
        public int CanHisCanId { get; set; }
        public int CanHisCandtlId { get; set; }
        public int CanHisRcmId { get; set; }
    }
}
