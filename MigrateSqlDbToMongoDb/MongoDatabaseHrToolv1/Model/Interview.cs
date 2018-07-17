using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MongoDatabaseHrToolv1.Model
{
    public partial class Interview
	{
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("Id")]
        public int ExternalId { get; set; }
		public int JobApplicationId { get; set; }
        public int InterviewRoundId { get; set; }
        public int JobId { get; set; }
        public object Result { get; set; }
        public string Note { get; set; }
        public object DateInterview { get; set; }
        public object DateFeedbackPhone { get; set; }
        public object DateFeedbackEmail { get; set; }
        public object ValidTo { get; set; }
        public object Interviewer { get; set; }
        public object Creater { get; set; }
        public string ExpectedSalary { get; set; }
        public string SuggestedSalary { get; set; }
        public string RecordingPath { get; set; }
        public string Summary { get; set; }
        public object Rating { get; set; }
        public object IsFinished { get; set; }
        public string Comment { get; set; }
        public object Match { get; set; }
        public object DurationTime { get; set; }
        public object IsCanceled { get; set; }
		public long RowID { get; set; }
	}
}
