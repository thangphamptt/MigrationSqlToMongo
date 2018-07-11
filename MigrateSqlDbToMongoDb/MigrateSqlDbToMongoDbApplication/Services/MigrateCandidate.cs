using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Candidate.AggregatesModel;
using SqlDatabase.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using MongoModel = MongoDatabase.Domain.Candidate.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateCandidate
    {
        public async Task<int> Execute(IConfiguration configuration)
        {
            var candidateDbContext = new CandidateDbContext(configuration);
            var sqlConnectionString = configuration.GetSection("SQLDB:ConnectionString").Value;
            using (var dbContext = HrToolDbContextFactory.CreateDbContext(sqlConnectionString))
            {
                var data = dbContext.Candidate.ToList();
                foreach (var candidate in data)
                {
                    try
                    {
                        var candididateModel = new MongoModel.Candidate()
                        {
                            Id = candidate.Id.ToString(),
                            ExternalId = string.Empty,
                            Firstname = candidate.FirstName,
                            Lastname = candidate.LastName,
                            PreviousJob = string.Empty,
                            PreviousCompany = string.Empty,
                            ProfileImagePath = candidate.ImagePath,
                            OrganizationalUnitId = string.Empty,
                            PhoneNumber = candidate.Phone,
                            Email = candidate.Email,
                            Gender = GetGender(candidate.Gender),
                            DateOfBirth = candidate.BirthDay,
                            Address = new Address()
                            {
                                StreetAddress = candidate.Address,
                                City = candidate.City,
                                Country = string.Empty,
                                Province = string.Empty,
                                ZipCode = string.Empty
                            },
                            CreatedDate = candidate.CreateDate ?? DateTime.Now,
                            ModifiedDate = candidate.ModifiedDate ?? DateTime.Now,
                            ModifiedByUserId = string.Empty,
                            ApplicationIds = candidate.JobApplication.Select(ja => ja.Id.ToString()).ToList(),
                            Tags = null,
                            ReadByUserIds = null,
                            FollowedByUserIds = null,
                            OwnedByUserIds = null,
                            InterestProfileCodes = null,
                            InterestProfileImage = null,
                            UserObjectId = null,
                            SuitableJobCategories = null,
                            SocialNetWorkProfiles = null
                        };
                        await candidateDbContext.CandidateCollection.InsertOneAsync(candididateModel);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Transfer candidate error at candidate id: {0}", candidate.Id);
                    }
                }
            }
            return candidateDbContext.Candidates.Count();
        }

        private Gender? GetGender(string gender)
        {
            if (string.IsNullOrEmpty(gender)) { return null; }
            if (gender == "FeMale")
            {
                gender = "Female";
            }
            return (Gender)Enum.Parse(typeof(Gender), gender);
        }
    }
}
