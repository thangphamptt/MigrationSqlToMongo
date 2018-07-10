namespace MongoDatabase.Domain.Job.AggregatesModel
{
    public enum JobStatus
    {
        Draft = 1,
		Ready,
        Published,
        Internal,
        Closed
    }
}
