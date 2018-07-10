using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class ContractType
    {
        public ContractType()
        {
            Contract = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeView { get; set; }
        public string Name { get; set; }
        public string NameView { get; set; }
        public int? Month { get; set; }
        public string Note { get; set; }

        public ICollection<Contract> Contract { get; set; }
    }
}
