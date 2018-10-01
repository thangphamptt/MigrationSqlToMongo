using MongoDatabase.DbContext;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class RemoveCandidateWithoutApplicationService
    {
        private CandidateDbContext _candidateDbContext;
        private InterviewDbContext _interviewDbContext;
        private JobMatchingDbContext _jobMatchingDbContext;
        private OfferDbContext _offerDbContext;
        private ScheduleDbContext _scheduleDbContext;

        public RemoveCandidateWithoutApplicationService(CandidateDbContext candidateDbContext, InterviewDbContext interviewDbContext,
            JobMatchingDbContext jobMatchingDbContext, OfferDbContext offerDbContext,
            ScheduleDbContext scheduleDbContext)
        {
            _candidateDbContext = candidateDbContext;
            _interviewDbContext = interviewDbContext;
            _jobMatchingDbContext = jobMatchingDbContext;
            _offerDbContext = offerDbContext;
            _scheduleDbContext = scheduleDbContext;
        }

        public async Task ExecuteAsync()
        {
            var candidateIds = await RemoveCandidateWithoutApplicationOnCandidateService();
            await RemoveCandidateWithoutApplicationOnInterviewService(candidateIds);
            await RemoveCandidateWithoutApplicationOnJobMathchingService(candidateIds);
            await RemoveCandidateWithoutApplicationOnOfferService(candidateIds);
            await RemoveCandidateWithoutApplicationOnScheduleService(candidateIds);
        }

        private async Task<List<string>> RemoveCandidateWithoutApplicationOnCandidateService()
        {
            Console.WriteLine("[Candidate] Detlete candidate without application => Starting...");
            var candidates = _candidateDbContext.Candidates.ToList();
            var candidatesWithoutApplication = candidates.Where(x => x.Applications.Count() == 0).Select(x => x.Id).ToList();

            await _candidateDbContext.CandidateCollection.DeleteManyAsync(FilterCandidateIdInCandidateService(candidatesWithoutApplication));
            Console.WriteLine($"[Candidate] Removed: {candidatesWithoutApplication.Count()}");
            return candidatesWithoutApplication;
        }

        private async Task RemoveCandidateWithoutApplicationOnInterviewService(List<string> candidates)
        {
            Console.WriteLine("[Interview] Detlete candidate without application => Starting...");
            var result = await _interviewDbContext.CandidateCollection.DeleteManyAsync(FilterCandidateIdInInterviewService(candidates));
            Console.WriteLine(string.Format("[Interview] Deleted {0}/{1}", result.DeletedCount, candidates.Count));
        }

        private async Task RemoveCandidateWithoutApplicationOnJobMathchingService(List<string> candidates)
        {
            Console.WriteLine("[JobMatching] Detlete candidate without application => Starting...");
            var result = await _jobMatchingDbContext.CandidateCollection.DeleteOneAsync(ItemWithListOfJobMatching(candidates));
            Console.WriteLine(string.Format("[JobMatching] Deleted {0}/{1}", result.DeletedCount, candidates.Count));
        }

        private async Task RemoveCandidateWithoutApplicationOnOfferService(List<string> candidates)
        {
            Console.WriteLine("[Offer] Detlete candidate without application => Starting...");
            var result = await _offerDbContext.CandidateCollection.DeleteOneAsync(ItemWithListOfOffer(candidates));
            Console.WriteLine(string.Format("[Offer] Deleted {0}/{1}", result.DeletedCount, candidates.Count));
        }

        private async Task RemoveCandidateWithoutApplicationOnScheduleService(List<string> candidates)
        {
            Console.WriteLine("[Schedule] Detlete candidate without application => Starting...");
            var result = await _scheduleDbContext.CandidateCollection.DeleteOneAsync(ItemWithListOfSchedule(candidates));
            Console.WriteLine(string.Format("[Schedule] Deleted {0}/{1}", result.DeletedCount, candidates.Count));
        }

        private FilterDefinition<CandidateDomainModel.Candidate> FilterCandidateIdInCandidateService(List<string> ids)
        {
            return Builders<CandidateDomainModel.Candidate>.Filter.In("_id", ids);
        }

        private FilterDefinition<MongoDatabase.Domain.Interview.AggregatesModel.Candidate> FilterCandidateIdInInterviewService(List<string> ids)
        {
            return Builders<MongoDatabase.Domain.Interview.AggregatesModel.Candidate>.Filter.In("_id", ids);
        }

        private FilterDefinition<MongoDatabase.Domain.JobMatching.AggregatesModel.Candidate> ItemWithListOfJobMatching(List<string> ids)
        {
            return Builders<MongoDatabase.Domain.JobMatching.AggregatesModel.Candidate>.Filter.In("_id", ids);
        }

        private FilterDefinition<MongoDatabase.Domain.Offer.AggregatesModel.Candidate> ItemWithListOfOffer(List<string> ids)
        {
            return Builders<MongoDatabase.Domain.Offer.AggregatesModel.Candidate>.Filter.In("_id", ids);
        }

        private FilterDefinition<MongoDatabase.Domain.Schedule.AggregatesModel.Candidate> ItemWithListOfSchedule(List<string> ids)
        {
            return Builders<MongoDatabase.Domain.Schedule.AggregatesModel.Candidate>.Filter.In("_id", ids);
        }
    }
}
