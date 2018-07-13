using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Candidate.AggregatesModel;
using MongoDatabaseHrToolv1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateCandidateToCandidateService
    {
        public async Task<int> InsertCandidateToCandidateService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
        {
            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var candidateDbContext = new CandidateDbContext(configuration);
            int totalCandidates = 0;
            if (candidates != null)
            {
                try
                {
                    foreach (var data in candidates)
                    {
                        if (!candidateDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
                        {

                            var candidate = new MongoDatabase.Domain.Candidate.AggregatesModel.Candidate()
                            {
                                Id = data.Id.ToString(),
                                Firstname = data.FirstName,
                                Lastname = data.LastName,
                                OrganizationalUnitId = organizationalUnitId,
                                PhoneNumber = data.Phone,
                                Email = data.Email,
                                Gender = (Gender?)ConvertGender(data.Gender),
                                DateOfBirth = !string.IsNullOrEmpty(data.BirthDay.ToString()) ? Convert.ToDateTime(data.BirthDay) :
                                new DateTime?(),
                                Address = new Address { City = data.City, StreetAddress = data.Address },
                                CreatedDate = !string.IsNullOrEmpty(data.CreateDate.ToString()) ? (DateTime)data.CreateDate : DateTime.Now
                            };
                            await candidateDbContext.CandidateCollection.InsertOneAsync(candidate);
                            totalCandidates ++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return totalCandidates;
        }
        
        public async Task<int> InsertCandidateToInterviewService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
        {
            var interviewDbContext = new InterviewDbContext(configuration);
            int totalCandidates = 0;
            if (candidates != null)
            {
                try
                {
                    
                    foreach (var data in candidates)
                    {
                        if (!interviewDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
                        {
                            var candidate = new MongoDatabase.Domain.Interview.AggregatesModel.Candidate()
                            {
                                Id = data.Id.ToString(),
                                FirstName = data.FirstName,
                                LastName = data.LastName,
                                Email = data.Email
                            };
                            await interviewDbContext.CandidateCollection.InsertOneAsync(candidate);
                            totalCandidates ++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return totalCandidates;
        }

        public async Task<int> InsertCandidateToJobService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
        {

            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var jobDbContext = new JobDbContext(configuration);
            int totalCandidates = 0;
            if (candidates != null)
            {
                try
                {
                    foreach (var data in candidates)
                    {
                        if (!jobDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
                        {
                            var candidate = new MongoDatabase.Domain.Job.AggregatesModel.Candidate()
                            {
                                Id = data.Id.ToString(),
                                FirstName = data.FirstName,
                                LastName = data.LastName,
                                Email = data.Email,
                                OrganizationalUnitId = organizationalUnitId
                            };
                            await jobDbContext.CandidateCollection.InsertOneAsync(candidate);
                            totalCandidates++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return totalCandidates;
        }

        public async Task<int> InsertCandidateToJobMatchingService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
        {

            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var jobMatchingDbContext = new JobMatchingDbContext(configuration);
            int totalCandidates = 0;
            if (candidates != null)
            {
                try
                {
                    foreach (var data in candidates)
                    {
                        if (!jobMatchingDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
                        {
                            var candidate = new MongoDatabase.Domain.JobMatching.AggregatesModel.Candidate()
                            {
                                Id = data.Id.ToString(),
                                Email = data.Email,
                                OrganizationalUnitId = organizationalUnitId
                            };
                            await jobMatchingDbContext.CandidateCollection.InsertOneAsync(candidate);
                            totalCandidates++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return totalCandidates;
        }

        public async Task<int> InsertCandidateToOfferService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
        {
            var offerDbContext = new OfferDbContext(configuration);
            int totalCandidates = 0;
            if (candidates != null)
            {
                try
                {
                    foreach (var data in candidates)
                    {
                        if (!offerDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
                        {
                            var candidate = new MongoDatabase.Domain.Offer.AggregatesModel.Candidate()
                            {
                                Id = data.Id.ToString(),
                                FirstName = data.FirstName,
                                LastName = data.LastName,
                                Email = data.Email
                            };
                            await offerDbContext.CandidateCollection.InsertOneAsync(candidate);
                            totalCandidates++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return totalCandidates;
        }

        public async Task<int> InsertCandidateToScheduleService(IConfiguration configuration, List<MongoDatabaseHrToolv1.Model.Candidate> candidates)
        {
            var scheduleDbContext = new ScheduleDbContext(configuration);
            int totalCandidates = 0;
            if (candidates != null)
            {
                try
                {
                    foreach (var data in candidates)
                    {
                        if (!scheduleDbContext.Candidates.Any(x => x.Id == data.Id.ToString()))
                        {
                            var candidate = new MongoDatabase.Domain.Schedule.AggregatesModel.Candidate()
                            {
                                Id = data.Id.ToString(),
                                FirstName = data.FirstName,
                                LastName = data.LastName,
                                Email = data.Email
                            };
                            await scheduleDbContext.CandidateCollection.InsertOneAsync(candidate);
                            totalCandidates++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return totalCandidates;
        }

        private int? ConvertGender(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                switch (value.ToLower())
                {
                    case "male":
                        return 0;
                    case "female":
                        return 1;
                }
            }
            return null;
        }
    }
}
