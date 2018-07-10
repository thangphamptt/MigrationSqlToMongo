using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.JobMatching.AggregatesModel
{
	public class SocCategory : IAggregateRoot, IEntity
    {
        public string Id { get; set; }
        public string CategoryCode { get; set; }
        public string SocCode { get; set; }
    }
}
