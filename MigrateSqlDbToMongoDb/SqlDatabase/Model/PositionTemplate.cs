using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class PositionTemplate
    {
        public int PositionId { get; set; }
        public int CompanyId { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public string Note { get; set; }
        public string JobTitle { get; set; }

        public Company Company { get; set; }
        public Position Position { get; set; }
    }
}
