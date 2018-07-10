using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class History
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int FormId { get; set; }
        public bool IsCreated { get; set; }
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateAction { get; set; }
        public string Ipaction { get; set; }
        public string ActionId { get; set; }

        public Employee Employee { get; set; }
        public Form Form { get; set; }
    }
}
