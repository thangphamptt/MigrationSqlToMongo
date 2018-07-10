using System;

namespace MongoDatabase.Domain.Job.AggregatesModel
{
    public class Publication
    {
        public DateTime? PublishedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
