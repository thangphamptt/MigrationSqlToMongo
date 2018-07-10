using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class JobApplicationAttachment
    {
        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public string Path { get; set; }
        public string Filename { get; set; }
        public bool? IsIndex { get; set; }
        public string FileType { get; set; }

        public JobApplication JobApplication { get; set; }
    }
}
