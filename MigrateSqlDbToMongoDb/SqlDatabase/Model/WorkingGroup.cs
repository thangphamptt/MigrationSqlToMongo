using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class WorkingGroup
    {
        public WorkingGroup()
        {
            WorkingGroupEmployee = new HashSet<WorkingGroupEmployee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? GroupStartDate { get; set; }
        public DateTime? GroupEndDate { get; set; }
        public string Note { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<WorkingGroupEmployee> WorkingGroupEmployee { get; set; }
    }
}
