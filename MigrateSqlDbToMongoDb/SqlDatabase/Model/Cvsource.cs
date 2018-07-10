using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Cvsource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }
    }
}
