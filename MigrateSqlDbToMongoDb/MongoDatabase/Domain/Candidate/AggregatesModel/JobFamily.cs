using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
	public class JobFamily : IEntity
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string GroupName { get; set; }
        public string Label { get; set; }
    }
}
