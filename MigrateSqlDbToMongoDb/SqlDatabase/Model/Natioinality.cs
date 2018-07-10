using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Natioinality
    {
        public int NatId { get; set; }
        public string NatCode { get; set; }
        public string NatName { get; set; }
        public string Note { get; set; }
    }
}
