using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class IncomeTaxRateTable
    {
        public IncomeTaxRateTable()
        {
            IncomeTaxRateTableDetail = new HashSet<IncomeTaxRateTableDetail>();
        }

        public int IncId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<IncomeTaxRateTableDetail> IncomeTaxRateTableDetail { get; set; }
    }
}
