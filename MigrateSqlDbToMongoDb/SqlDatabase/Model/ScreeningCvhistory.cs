using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class ScreeningCvhistory
    {
        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }

        public JobApplication JobApplication { get; set; }
        public Employee ModifiedUserNavigation { get; set; }
    }
}
