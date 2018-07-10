using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public int CompanyId { get; set; }
        public string Type { get; set; }

        public Company Company { get; set; }
    }
}
