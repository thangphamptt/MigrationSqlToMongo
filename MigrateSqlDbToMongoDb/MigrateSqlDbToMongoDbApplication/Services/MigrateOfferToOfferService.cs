using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;
using System.Linq;
using OfferDomainModel = MongoDatabase.Domain.Offer.AggregatesModel;
using HrToolDomainModel = MongoDatabaseHrToolv1.Model;
using System;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateOfferToOfferService
    {
        private HrToolv1DbContext hrToolDbContext;

        public async Task<int> Execute(IConfiguration configuration)
        {
            hrToolDbContext = new HrToolv1DbContext(configuration);
            var offerDbContext = new OfferDbContext(configuration);

            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var userId = configuration.GetSection("AdminUser:Id")?.Value;
            var dataInserted = 0;
            var listOffer = new List<OfferDomainModel.Offer>();

            try
            {
                var contractCodes = hrToolDbContext.ContractCodes.ToList();
                
                foreach (var contractCode in contractCodes)
                {
                    bool hasOfferExisted = offerDbContext.Offers.Any(w => w.Id == contractCode.Id.ToString());
                    if (!hasOfferExisted)
                    {
                        var jobApplication = hrToolDbContext.JobApplications
                            .FirstOrDefault(w => w.ExternalId == contractCode.JobApplicationId);
                        var job = hrToolDbContext.Jobs
                            .FirstOrDefault(w => w.ExternalId == contractCode.JobId);
                        var position = hrToolDbContext.Positions
                           .FirstOrDefault(w => w.ExternalId == (int)jobApplication.PositionId);
                        var currencyVnd = offerDbContext.Currencies.FirstOrDefault(f => f.Code == "VND");
                        var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);

                        var offer = new OfferDomainModel.Offer
                        {
                            Id = contractCode.Id.ToString(),
                            ApplicationId = jobApplication.Id.ToString(),
                            CandidateId = jobApplication.CandidateId.ToString(),
                            CreatedByUserId = userId,
                            CreatedDate = DateTime.Now,
                            CurrencyId = currencyVnd.Id,
                            JobId = job.Id.ToString(),
                            OrganizationalUnitId = organizationalUnitId,
                            Position = title,
                            ReportTo = contractCode.ReportTo,
                            Salary = Convert.ToDecimal(Helper.Decrypt(contractCode.SalaryOffer, true)),
                            Status = GetStatus(contractCode.IsAcceptSigning)
                        };
                        listOffer.Add(offer);
                        //Migrate job to Candidate service
                        //await offerDbContext.OfferCollection.InsertOneAsync(offer);
                        dataInserted++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
          
            return dataInserted;
        }

        private string GetPositionName(int positionId)
        {
            return hrToolDbContext.Positions?.FirstOrDefault(f => f.ExternalId == positionId)?.PositionName;
        }

        private bool? GetStatus(object IsAcceptSigning)
        {
            if (string.IsNullOrEmpty(IsAcceptSigning.ToString()))
            {
                return null;
            }

            return Convert.ToBoolean(IsAcceptSigning);
        }
    }
}
