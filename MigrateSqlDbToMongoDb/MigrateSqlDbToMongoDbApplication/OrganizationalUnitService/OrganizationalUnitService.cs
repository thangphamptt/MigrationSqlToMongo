using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using CandidateOrganizationalUnit = MongoDatabase.Domain.Candidate.AggregatesModel;
using InterviewOrganizationalUnit = MongoDatabase.Domain.Interview.AggregatesModel;
using JobOrganizationalUnit = MongoDatabase.Domain.Job.AggregatesModel;
using JobMatchingOrganizationalUnit = MongoDatabase.Domain.JobMatching.AggregatesModel;
using OfferOrganizationalUnit = MongoDatabase.Domain.Offer.AggregatesModel;
using TemplateOrganizationalUnit = MongoDatabase.Domain.Template.AggregatesModel;

using CandidateOrganizationalUnitRepository = Candidate.Persistance.Repositories;
using InterviewOrganizationalUnitRepository = Interview.Persistance.Repositories;
using JobOrganizationalUnitRepository = MongoDatabase.Repositories.Job;
using JobMatchingOrganizationalUnitRepository = MongoDatabase.Repositories.JobMatching;
using OfferOrganizationalUnitRepository = MongoDatabase.Repositories.Offer;
using TemplateOrganizationalUnitRepository = MongoDatabase.Repositories.Template;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.OrganizationalUnitService
{
    public class OrganizationalUnitService
    {
        private readonly CandidateOrganizationalUnitRepository.OrganizationalUnitRepository _candidateOrganizationalUnitRepository;
        private readonly InterviewOrganizationalUnitRepository.OrganizationalUnitRepository _interviewOrganizationalUnitRepository;
        private readonly JobOrganizationalUnitRepository.OrganizationalUnitRepository  _jobOrganizationalUnitRepository;
        private readonly JobMatchingOrganizationalUnitRepository.OrganizationalUnitRepository _jobMatchingOrganizationalUnitRepository;
        private readonly OfferOrganizationalUnitRepository.OrganizationalUnitRepository _offerOrganizationalUnitRepository;
        private readonly TemplateOrganizationalUnitRepository.OrganizationalUnitRepository _templateOrganizationalUnitRepository;

        public OrganizationalUnitService(IConfiguration configuration)
        {
            _candidateOrganizationalUnitRepository = new CandidateOrganizationalUnitRepository.OrganizationalUnitRepository(configuration);
            _interviewOrganizationalUnitRepository = new InterviewOrganizationalUnitRepository.OrganizationalUnitRepository(configuration);
            _jobOrganizationalUnitRepository = new JobOrganizationalUnitRepository.OrganizationalUnitRepository(configuration);
            _jobMatchingOrganizationalUnitRepository = new JobMatchingOrganizationalUnitRepository.OrganizationalUnitRepository(configuration);
            _offerOrganizationalUnitRepository = new OfferOrganizationalUnitRepository.OrganizationalUnitRepository(configuration);
            _templateOrganizationalUnitRepository = new TemplateOrganizationalUnitRepository.OrganizationalUnitRepository(configuration);
        }

        public async Task<List<string>> AddOrganizationalUnit(string id, string companyName)
        {
            var addedEntities = new List<string>();
            var candidateOrganizationalUnit = await _candidateOrganizationalUnitRepository.GetOrganizationalUnitByIdAsync(id);
            if (candidateOrganizationalUnit == null)
            {
                var data = new CandidateOrganizationalUnit.OrganizationalUnit
                {
                    Id = id,
                    Name = companyName
                };
                await _candidateOrganizationalUnitRepository.CreateOrganizationalUnitAsync(data);
                addedEntities.Add("CandidateOrganizationalUnit");
            }

            var interviewOrganizationalUnit = await _interviewOrganizationalUnitRepository.GetOrganizationalUnitByIdAsync(id);
            if (interviewOrganizationalUnit == null)
            {
                var data = new InterviewOrganizationalUnit.OrganizationalUnit
                {
                    Id = id,
                    Name = companyName
                };
                await _interviewOrganizationalUnitRepository.CreateOrganizationalUnitAsync(data);
                addedEntities.Add("InterviewOrganizationalUnit");
            }

            var jobOrganizationalUnit = await _jobOrganizationalUnitRepository.GetAsync(id);
            if (jobOrganizationalUnit == null)
            {
                var data = new JobOrganizationalUnit.OrganizationalUnit
                {
                    Id = id,
                    Name = companyName
                };
                await _jobOrganizationalUnitRepository.CreateAsync(data);
                addedEntities.Add("JobOrganizationalUnit");
            }

            var jobMatchingOrganizationalUnit = await _jobMatchingOrganizationalUnitRepository.GetOrganizationalUnitByIdAsync(id);
            if (jobMatchingOrganizationalUnit == null)
            {
                var data = new JobMatchingOrganizationalUnit.OrganizationalUnit
                {
                    Id = id,
                    Name = companyName
                };
                await _jobMatchingOrganizationalUnitRepository.CreateOrganizationalUnitAsync(data);
                addedEntities.Add("JobMatchingOrganizationalUnit");
            }

            var offerOrganizationalUnit = await _offerOrganizationalUnitRepository.GetOrganizationalUnitByIdAsync(id);
            if (offerOrganizationalUnit == null)
            {
                var data = new OfferOrganizationalUnit.OrganizationalUnit
                {
                    Id = id,
                    Name = companyName
                };
                await _offerOrganizationalUnitRepository.CreateOrganizationalUnitAsync(data);
                addedEntities.Add("OfferOrganizationalUnit");
            }

            var templateOrganizationalUnit = await _templateOrganizationalUnitRepository.GetOrganizationalUnitByIdAsync(id);
            if (templateOrganizationalUnit == null)
            {
                var data = new TemplateOrganizationalUnit.OrganizationalUnit
                {
                    Id = id,
                    Name = companyName
                };
                await _templateOrganizationalUnitRepository.CreateOrganizationalUnitAsync(data);
                addedEntities.Add("TemplateOrganizationalUnit");
            }
            return addedEntities;
        }
    }
}
