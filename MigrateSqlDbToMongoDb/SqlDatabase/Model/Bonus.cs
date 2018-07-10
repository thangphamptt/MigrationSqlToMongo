using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Bonus
    {
        public Bonus()
        {
            ContractBonus = new HashSet<ContractBonus>();
        }

        public int BnsId { get; set; }
        public string BnsName { get; set; }
        public decimal? Money { get; set; }
        public string Note { get; set; }

        public ICollection<ContractBonus> ContractBonus { get; set; }
    }
}
