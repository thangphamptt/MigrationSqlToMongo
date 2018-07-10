namespace MongoDatabase.Domain.JobMatching.AggregatesModel
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
