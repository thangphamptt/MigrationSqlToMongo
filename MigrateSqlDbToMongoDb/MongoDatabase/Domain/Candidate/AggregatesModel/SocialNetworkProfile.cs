namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
    public class SocialNetworkProfile
    {
        public string Label { get; set; }
        public string Url { get; set; }
        public SocialNetworkType SocialNetworkType { get; set; }
    }
}
