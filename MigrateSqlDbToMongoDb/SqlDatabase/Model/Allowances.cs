using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Allowances
    {
        public Allowances()
        {
            ContractAllowances = new HashSet<ContractAllowances>();
        }

        public int AlwId { get; set; }
        public string AlwName { get; set; }
        public string Note { get; set; }

        public ICollection<ContractAllowances> ContractAllowances { get; set; }
    }
}
