using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace MongoDatabase.Domain.JobMatching.AggregatesModel
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JobType
    {
        [Description("Full Time")]
        FullTime = 1,

        [Description("Part Time")]
        PartTime,

        [Description("Temporary")]
        Temporary,

        [Description("Internship")]
        Internship
    }
}
