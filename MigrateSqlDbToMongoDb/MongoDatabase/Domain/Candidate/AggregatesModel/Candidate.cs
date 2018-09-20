using MongoDatabase.Domain.Common;
using System.Collections.Generic;
using System;

namespace MongoDatabase.Domain.Candidate.AggregatesModel
{
    public class Candidate : IAggregateRoot, IEntity
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PreviousJob { get; set; }
        public string PreviousCompany { get; set; }
        public string ProfileImagePath { get; set; }
        public string OrganizationalUnitId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedByUserId { get; set; }
        public IList<string> ApplicationIds { get; set; } = new List<string>();
        public IList<Application> Applications { get; set; } = new List<Application>();
        public IList<Tag> Tags { get; set; } = new List<Tag>();
        public IList<string> ReadByUserIds { get; set; } = new List<string>();
        public IList<string> FollowedByUserIds { get; set; } = new List<string>();
        public IList<string> OwnedByUserIds { get; set; } = new List<string>();
        public IList<string> InterestProfileCodes { get; set; } = new List<string>();
        public byte[] InterestProfileImage { get; set; }
        public string UserObjectId { get; set; }
        public List<string> FullName { get; set; }
        public IList<SuitableJobCategory> SuitableJobCategories { get; set; } = new List<SuitableJobCategory>();
        public IList<SocialNetworkProfile> SocialNetWorkProfiles { get; set; } = new List<SocialNetworkProfile>();
    }
}
