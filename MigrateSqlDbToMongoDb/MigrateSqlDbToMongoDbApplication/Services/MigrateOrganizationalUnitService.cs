using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;
using InterviewDomainModel = MongoDatabase.Domain.Interview.AggregatesModel;
using JobDomainModel = MongoDatabase.Domain.Job.AggregatesModel;
using JobMatchingDomainModel = MongoDatabase.Domain.JobMatching.AggregatesModel;
using OfferDomainModel = MongoDatabase.Domain.Offer.AggregatesModel;
using TemplateDomainModel = MongoDatabase.Domain.Template.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateOrganizationalUnitService
    {
        private HrToolv1DbContext _hrToolDbContext;
        private CandidateDbContext _candidateDbContext;
        private InterviewDbContext _interviewDbContext;
        private JobDbContext _jobDbContext;
        private JobMatchingDbContext _jobMatchingDbContext;
        private OfferDbContext _offerDbContext;
        private TemplateDbContext _templateDbContext;
        private string organizationalUnitId;
        private string organizationalUnitName;

        public MigrateOrganizationalUnitService(
            IConfiguration configuration,
            HrToolv1DbContext hrToolDbContext,
            CandidateDbContext candidateDbContext,
            InterviewDbContext interviewDbContext,
            JobDbContext jobDbContext,
            JobMatchingDbContext jobMatchingDbContext,
            OfferDbContext offerDbContext,
            TemplateDbContext templateDbContext)
        {
            _hrToolDbContext = hrToolDbContext;
            _candidateDbContext = candidateDbContext;
            _interviewDbContext = interviewDbContext;
            _jobDbContext = jobDbContext;
            _jobMatchingDbContext = jobMatchingDbContext;
            _offerDbContext = offerDbContext;
            _templateDbContext = templateDbContext;
            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            organizationalUnitName = configuration.GetSection("CompanySetting:Name")?.Value;
        }

        public async Task ExecuteAsync()
        {
            await MigrateOrganizationalUnitToCandidateService();
            await MigrateOrganizationalUnitToInterviewService();
            await MigrateOrganizationalUnitToJobService();
            await MigrateOrganizationalUnitToJobMatchingService();
            await MigrateOrganizationalUnitToOfferService();
            await MigrateOrganizationalUnitToTemplateService();
            await MigratePipelineStageToCandidateService();
        }

        private async Task MigrateOrganizationalUnitToCandidateService()
        {
            Console.WriteLine("Migrate [organizationalUnit] to [Candidate service] => Starting...");
            try
            {
                var hasOrganizationalUnitExisted = _candidateDbContext.OrganizationalUnits.Any(a => a.Id == organizationalUnitId);
                if (!hasOrganizationalUnitExisted)
                {
                    var organizationalUnit = new CandidateDomainModel.OrganizationalUnit
                    {
                        Id = organizationalUnitId,
                        Name = organizationalUnitName
                    };
                    await _candidateDbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
                    Console.WriteLine($"Migrate [organizationalUnit] to [Candidate service] => DONE: inserted {1} organizational unit. \n");
                }
                else
                {
                    Console.WriteLine($"Migrate [organizationalUnit] to [Candidate service] => DONE: data existed. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task MigrateOrganizationalUnitToInterviewService()
        {
            try
            {
                Console.WriteLine("Migrate [organizationalUnit] to [Interview service] => Starting...");
                var hasOrganizationalUnitExisted = _interviewDbContext.OrganizationalUnits.Any(a => a.Id == organizationalUnitId);
                if (!hasOrganizationalUnitExisted)
                {
                    var organizationalUnit = new InterviewDomainModel.OrganizationalUnit
                    {
                        Id = organizationalUnitId,
                        Name = organizationalUnitName
                    };
                    await _interviewDbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
                    Console.WriteLine($"Migrate [organizationalUnit] to [Interview service] => DONE: inserted {1} organizational unit. \n");
                }
                else
                {
                    Console.WriteLine($"Migrate [organizationalUnit] to [Interview service] => DONE: data existed. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task MigrateOrganizationalUnitToJobService()
        {
            try
            {
                Console.WriteLine("Migrate [organizationalUnit] to [Job service] => Starting...");
                var hasOrganizationalUnitExisted = _jobDbContext.OrganizationalUnits.Any(a => a.Id == organizationalUnitId);
                if (!hasOrganizationalUnitExisted)
                {
                    var organizationalUnit = new JobDomainModel.OrganizationalUnit
                    {
                        Id = organizationalUnitId,
                        Name = organizationalUnitName
                    };
                    await _jobDbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
                    Console.WriteLine($"Migrate [organizationalUnit] to [Job service] => DONE: inserted {1} organizational unit. \n");
                }
                else
                {
                    Console.WriteLine($"Migrate [organizationalUnit] to [Job service] => DONE: data existed. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task MigrateOrganizationalUnitToJobMatchingService()
        {
            try
            {
                Console.WriteLine("Migrate [organizationalUnit] to [Job Matching service] => Starting...");
                var hasOrganizationalUnitExisted = _jobMatchingDbContext.OrganizationalUnits.Any(a => a.Id == organizationalUnitId);
                if (!hasOrganizationalUnitExisted)
                {
                    var organizationalUnit = new JobMatchingDomainModel.OrganizationalUnit
                    {
                        Id = organizationalUnitId,
                        Name = organizationalUnitName
                    };
                    await _jobMatchingDbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
                    Console.WriteLine($"Migrate [organizationalUnit] to [Job Matching service] => DONE: inserted {1} organizational unit. \n");
                }
                else
                {
                    Console.WriteLine($"Migrate [organizationalUnit] to [Job Matching service] => DONE: data existed. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task MigrateOrganizationalUnitToOfferService()
        {
            try
            {
                Console.WriteLine("Migrate [organizationalUnit] to [Offer service] => Starting...");
                var hasOrganizationalUnitExisted = _offerDbContext.OrganizationalUnits.Any(a => a.Id == organizationalUnitId);
                if (!hasOrganizationalUnitExisted)
                {
                    var organizationalUnit = new OfferDomainModel.OrganizationalUnit
                    {
                        Id = organizationalUnitId,
                        Name = organizationalUnitName
                    };
                    await _offerDbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
                    Console.WriteLine($"Migrate [organizationalUnit] to [Offer service] => DONE: inserted {1} organizational unit. \n");
                }
                else
                {
                    Console.WriteLine($"Migrate [organizationalUnit] to [Offer service] => DONE: data existed. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private async Task MigrateOrganizationalUnitToTemplateService()
        {
            try
            {
                Console.WriteLine("Migrate [organizationalUnit] to [Template service] => Starting...");
                var hasOrganizationalUnitExisted = _templateDbContext.OrganizationalUnits.Any(a => a.Id == organizationalUnitId);
                if (!hasOrganizationalUnitExisted)
                {
                    var organizationalUnit = new TemplateDomainModel.OrganizationalUnit
                    {
                        Id = organizationalUnitId,
                        Name = organizationalUnitName
                    };
                    await _templateDbContext.OrganizationalUnitCollection.InsertOneAsync(organizationalUnit);
                    Console.WriteLine($"Migrate [organizationalUnit] to [Template service] => DONE: inserted {1} organizational unit. \n");
                }
                else
                {
                    Console.WriteLine($"Migrate [organizationalUnit] to [Template service] => DONE: data existed. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task MigratePipelineStageToCandidateService()
        {
            try
            {
                Console.WriteLine("Migrate [pipeline] to [Candidate service] => Starting...");
                var pipeline = _candidateDbContext.Pipelines.FirstOrDefault(x => x.OrganizationalUnitId == organizationalUnitId);
                if (pipeline == null)
                {
                    var newPipelineStages = new List<CandidateDomainModel.PipelineStage>
                {
                        new CandidateDomainModel.PipelineStage {
                            StageType = CandidateDomainModel.StageType.Sourced,
                            Name = "Sourced",
                            Id = ObjectId.GenerateNewId().ToString(),
                            Order = 1
                        },
                        new CandidateDomainModel.PipelineStage {
                            StageType =CandidateDomainModel. StageType.New,
                            Name = "Applied",
                            Id = ObjectId.GenerateNewId().ToString(),
                            Order = 2
                        },
                        new CandidateDomainModel.PipelineStage {
                            StageType =CandidateDomainModel. StageType.Lead,
                            Name = "In Review",
                            Id = ObjectId.GenerateNewId().ToString(),
                            Order = 3
                        },
                        new CandidateDomainModel.PipelineStage {
                            StageType =CandidateDomainModel. StageType.Interviewing,
                            Name = "Phone Interview",
                            Id = ObjectId.GenerateNewId().ToString(),
                            Order = 4
                        },
                        new CandidateDomainModel.PipelineStage {
                            StageType =CandidateDomainModel. StageType.Interviewing,
                            Name = "On-site Interview",
                            Id = ObjectId.GenerateNewId().ToString(),
                            Order = 5,
                            DefaultIconColor = "#b76ab7",
                            DefaultIconPath = "sprite-position",
                        },
                        new CandidateDomainModel.PipelineStage {
                            StageType = CandidateDomainModel.StageType.Offered,
                            Id = ObjectId.GenerateNewId().ToString(),
                            Name = "Offer",
                            Order = 6
                        },
                        new CandidateDomainModel.PipelineStage {
                            StageType = CandidateDomainModel.StageType.Hired,
                            Id = ObjectId.GenerateNewId().ToString(),
                            Name = "Hired",
                            Order = 7
                        }
                    };

                    var newPipeline = new CandidateDomainModel.Pipeline
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        OrganizationalUnitId = organizationalUnitId,
                        Stages = newPipelineStages
                    };

                    await _candidateDbContext.PipelineCollection.InsertOneAsync(newPipeline);
                    Console.WriteLine($"Migrate [pipeline] to [Candidate service] => DONE: inserted {1} pipeline. \n");
                }
                else
                {
                    Console.WriteLine("Migrate [pipeline] to [Candidate service] => DONE: data existed. \n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
