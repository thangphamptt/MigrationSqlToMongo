using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class MarkOnlineTestBackup
    {
        public int Id { get; set; }
        public int? JobApplicationId { get; set; }
        public string Iqmark { get; set; }
        public string TechnicalMark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
    }
}
