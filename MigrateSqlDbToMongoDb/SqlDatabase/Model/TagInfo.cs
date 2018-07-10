using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class TagInfo
    {
        public TagInfo()
        {
            CandidateTag = new HashSet<CandidateTag>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CompanyId { get; set; }

        public ICollection<CandidateTag> CandidateTag { get; set; }
    }
}
