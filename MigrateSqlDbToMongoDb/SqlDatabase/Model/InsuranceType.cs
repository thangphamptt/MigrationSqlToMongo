using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class InsuranceType
    {
        public InsuranceType()
        {
            Insurance = new HashSet<Insurance>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameVn { get; set; }
        public string Note { get; set; }

        public ICollection<Insurance> Insurance { get; set; }
    }
}
