using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class ReductFaminlyCir
    {
        public int RfaId { get; set; }
        public decimal Money { get; set; }
        public decimal Percent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
