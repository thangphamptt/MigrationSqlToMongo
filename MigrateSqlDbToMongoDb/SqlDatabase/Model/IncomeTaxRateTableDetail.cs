using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class IncomeTaxRateTableDetail
    {
        public int IncDtlId { get; set; }
        public int LevelTax { get; set; }
        public decimal? PercentTax { get; set; }
        public decimal? FromIncome { get; set; }
        public decimal? ToIncome { get; set; }
        public int IncDtlIncId { get; set; }

        public IncomeTaxRateTable IncDtlInc { get; set; }
    }
}
