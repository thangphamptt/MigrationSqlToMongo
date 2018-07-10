using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class ContractAllowances
    {
        public int CtrAlwCtrId { get; set; }
        public int CtrAlwAlwId { get; set; }

        public Allowances CtrAlwAlw { get; set; }
        public Contract CtrAlwCtr { get; set; }
    }
}
