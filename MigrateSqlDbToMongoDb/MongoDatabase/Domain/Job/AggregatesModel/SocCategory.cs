using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Job.AggregatesModel
{
	public class SocCategory : IEntity
    {
        public string Id { get; set; }
        public string CategoryCode { get; set; }
        public string SocCode { get; set; }
    }
}
