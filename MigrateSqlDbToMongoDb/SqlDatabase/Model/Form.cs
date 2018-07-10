using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Form
    {
        public Form()
        {
            History = new HashSet<History>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int? RefFormId { get; set; }
        public int? Index { get; set; }
        public string DisplayName { get; set; }
        public string ImagePath { get; set; }

        public ICollection<History> History { get; set; }
    }
}
