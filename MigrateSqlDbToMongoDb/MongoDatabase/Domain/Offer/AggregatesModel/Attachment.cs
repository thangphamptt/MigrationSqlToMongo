﻿using MongoDatabase.Domain.Common;

namespace MongoDatabase.Domain.Offer.AggregatesModel
{
	public class Attachment: IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
