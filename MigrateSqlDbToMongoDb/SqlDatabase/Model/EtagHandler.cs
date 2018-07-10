using System;
using System.Collections.Generic;

namespace SqlDatabase.Model
{
    public partial class EtagHandler
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Action { get; set; }
        public string UrlEncryption { get; set; }
        public string Etag { get; set; }
        public int? CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
