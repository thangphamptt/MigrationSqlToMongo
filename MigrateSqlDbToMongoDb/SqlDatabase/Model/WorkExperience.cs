using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class WorkExperience
    {
        public int WkeId { get; set; }
        public int WkeEmplId { get; set; }
        public int WkePstId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Note { get; set; }

        public Employee WkeEmpl { get; set; }
        public Position WkePst { get; set; }
    }
}
