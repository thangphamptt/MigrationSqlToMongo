using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class ProbationAppraisal
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AppraisalTime { get; set; }
        public int JobKnowledge { get; set; }
        public int WorkProductivity { get; set; }
        public int WorkQuality { get; set; }
        public int PolicyPerformance { get; set; }
        public int CommunicationSkill { get; set; }
        public int TeamworkAbility { get; set; }
        public int Analyzing { get; set; }
        public int Adaptability { get; set; }
        public int CustomerCare { get; set; }
        public int GeneralAppraisal { get; set; }
        public string Comment { get; set; }
        public int Agree { get; set; }
        public string Reason { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime AppraisalDate { get; set; }
        public byte? ProbationPeriod { get; set; }

        public Employee Employee { get; set; }
    }
}
