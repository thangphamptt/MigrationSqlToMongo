using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class ResultOnlineTest
    {
        public int Id { get; set; }
        public int? JobApplicationId { get; set; }
        public int? QuestionId { get; set; }
        public int? ResultType { get; set; }
        public string Answer { get; set; }
        public bool? IsCorrect { get; set; }
        public bool? IsMark { get; set; }
    }
}
