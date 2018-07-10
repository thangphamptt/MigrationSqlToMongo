using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class ContractBonus
    {
        public int CtrBnsCtrId { get; set; }
        public int CtrBnsBnsId { get; set; }

        public Bonus CtrBnsBns { get; set; }
        public Contract CtrBnsCtr { get; set; }
    }
}
