using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class LetterTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Parameter { get; set; }
        public string Note { get; set; }
        public string Type { get; set; }
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string CcEmail { get; set; }
        public string Subject { get; set; }
        public string BccEmail { get; set; }
        public int? CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
