using MongoDatabase.DbContext;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateNestedApplicationIntoCandidateService
    {
        private CandidateDbContext _candidateDbContext;

        public MigrateNestedApplicationIntoCandidateService(CandidateDbContext candidateDbContext)
        {
            _candidateDbContext = candidateDbContext;
        }

        public async Task ExecuteAsync()
        {
            await MigrateApplicationNestedToCandidateService();
        }

        private async Task MigrateApplicationNestedToCandidateService()
        {
            Console.WriteLine("Migrate [application] NESTED to [Candidate service] => Starting...");
            try
            {
                var candidates = _candidateDbContext.Candidates.ToList();
                if (candidates != null && candidates.Count > 0)
                {
                    int count = 0;
                    foreach (var candidate in candidates)
                    {
                        var applications = GetApplicationToInsert(candidate.Id);
                        if (applications != null && applications.Count > 0)
                        {
                            var filter = Builders<CandidateDomainModel.Candidate>.Filter.Where(t => t.Id == candidate.Id);
                            var update = Builders<CandidateDomainModel.Candidate>.Update
                                .Set(t => t.Applications, applications);
                            await _candidateDbContext.CandidateCollection.UpdateOneAsync(filter, update);

                            count++;
                            Console.Write($"\r {count}/{candidates.Count}");
                        }
                    }

                    Console.WriteLine($"\n Migrate [application] NESTED to [Candidate service] => DONE: updated {candidates.Count} candidates. \n");
                }
                else
                {
                    Console.WriteLine($"Migrate [application] NESTED to [Candidate service] => DONE: data exsited. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private List<CandidateDomainModel.Application> GetApplicationFromApplicationCollection(string candidateId)
        {
            if (string.IsNullOrEmpty(candidateId)) return null;
            return _candidateDbContext.Applications.Where(w => w.CandidateId == candidateId).ToList();
        }

        private List<string> GetApplicationFromCandidateCollection(string candidateId)
        {
            if (string.IsNullOrEmpty(candidateId)) return null;
            return _candidateDbContext.Candidates
                .Where(w => w.Id == candidateId && w.Applications.Any())
                .AsEnumerable()
                .SelectMany(s => s.Applications.Select(a => a.Id).ToList()).ToList();
        }

        private List<CandidateDomainModel.Application> GetApplicationToInsert(string candidateId)
        {
            var applications = GetApplicationFromApplicationCollection(candidateId);
            var applicationIdsExisted = GetApplicationFromCandidateCollection(candidateId).ToList();
            if (applicationIdsExisted !=null && applicationIdsExisted.Count > 0) {
                return applications.Where(w => !applicationIdsExisted.Contains(w.Id)).ToList();
            }         
            return applications;
        }
    }
}
