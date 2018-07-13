using System;
using System.Collections.Generic;

namespace MongoDatabaseHrToolv1.Model
{
    public partial class InterviewRound
    {
        public InterviewRound()
        {
            Interview = new HashSet<Interview>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Round { get; set; }
        public bool IsEnd { get; set; }

        public ICollection<Interview> Interview { get; set; }
    }
}
