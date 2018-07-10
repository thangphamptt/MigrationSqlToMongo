using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class WorkingGroupEmployee
    {
        public int WorkingGroupId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Employee Employee { get; set; }
        public WorkingGroup WorkingGroup { get; set; }
    }
}
