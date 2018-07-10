using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EmployeeHistoryAction
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsAccept { get; set; }
        public string Note { get; set; }
        public int EmployeeId { get; set; }
        public string ErrorStatus { get; set; }
        public DateTime? ErrorDate { get; set; }
        public DateTime ActionDate { get; set; }
        public bool AutoSend { get; set; }

        public Employee Employee { get; set; }
    }
}
