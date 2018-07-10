using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace MongoDatabase.Domain.JobMatching.AggregatesModel
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PositionLevel
    {
        [Description("Student Job")]
        StudentJob = 1,

        [Description("Entry Level")]
        EntryLevel,

        [Description("Experienced")]
        Experienced,

        [Description("Manager")]
        Manager,

        [Description("Senior Manager")]
        SeniorManager,

        [Description("Top Management")]
        TopManagement
    }
}
