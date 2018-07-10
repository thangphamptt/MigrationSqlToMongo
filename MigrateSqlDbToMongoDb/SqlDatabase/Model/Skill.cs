using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Skill
    {
        public int Id { get; set; }
        public int? CandidateId { get; set; }
        public int? EmployeeId { get; set; }
        public string SkillName { get; set; }
        public byte? LevelId { get; set; }
        public byte? Year { get; set; }
        public int? LastUsed { get; set; }
    }
}
