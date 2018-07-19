using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfferDomainModel = MongoDatabase.Domain.Offer.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateOfferService
    {
        private HrToolv1DbContext _hrToolDbContext;
        private OfferDbContext _offerDbContext;

        private string organizationalUnitId;
        private string userId;
        private List<MongoDatabaseHrToolv1.Model.ContractCode> offerData;

        public MigrateOfferService(IConfiguration configuration,
            HrToolv1DbContext hrToolDbContext,
            OfferDbContext offerDbContext)
        {
            _hrToolDbContext = hrToolDbContext;
            _offerDbContext = offerDbContext;

            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            userId = configuration.GetSection("AdminUser:Id")?.Value;
            offerData = _hrToolDbContext.ContractCodes.ToList()
                .OrderByDescending(x => x.Id)
                .ThenByDescending(x => x.ExternalId)
                .Distinct(new ContractCodeSearchModelComparer()).ToList();
        }

        public async Task ExecuteAsync()
        {
            await MigrateOfferToOfferService();
        }

        private async Task MigrateOfferToOfferService()
        {
            Console.WriteLine("Migrate [offer] to [Offer service] => Starting...");

            var offerIdDestination = _offerDbContext.Offers.Select(s => s.Id).ToList();
            var offerSource = offerData.Where(w => !offerIdDestination.Contains(w.Id.ToString())).ToList();

            if (offerSource != null && offerSource.Count > 0)
            {
                int count = 0;
                foreach (var offer in offerSource)
                {
                    var jobApplication = _hrToolDbContext.JobApplications
                           .FirstOrDefault(w => w.ExternalId == offer.JobApplicationId);
                    var job = _hrToolDbContext.Jobs
                        .FirstOrDefault(w => w.ExternalId == offer.JobId);
                    var position = _hrToolDbContext.Positions
                       .FirstOrDefault(w => w.ExternalId == (int)jobApplication.PositionId);
                    var currencyVnd = _offerDbContext.Currencies.FirstOrDefault(f => f.Code == "VND");
                    var title = !string.IsNullOrEmpty(job.JobTitle) ? job.JobTitle : GetPositionName(job.PositionId);

                    var expirationDate = offer.ValidTo is DateTime ? (DateTime)offer.WorkingStartDate : DateTime.Now;
                    var data = new OfferDomainModel.Offer
                    {
                        Id = offer.Id.ToString(),
                        ApplicationId = jobApplication.Id.ToString(),
                        CandidateId = jobApplication.CandidateId.ToString(),
                        CreatedByUserId = userId,
                        CreatedDate = expirationDate.AddMonths(-1),
                        CurrencyId = currencyVnd?.Id,
                        JobId = job.Id.ToString(),
                        OrganizationalUnitId = organizationalUnitId,
                        Position = title,
                        ReportTo = offer.ReportTo,
                        Salary = Convert.ToDecimal(Helper.Decrypt(offer.SalaryOffer, true)),
                        Status = GetStatus(offer.IsAcceptSigning),
                        ExpirationDate = expirationDate,
                        SentDate = offer.SendingDate is DateTime ? (DateTime)offer.SendingDate : new DateTime?(),
                        StartDate = expirationDate.AddMonths(-1),
                        SentByUserId = offer.SendingDate is DateTime ? userId : string.Empty,
                        IsUpdate = false
                    };

                    await _offerDbContext.OfferCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{offerSource.Count}");
                }
                Console.WriteLine($"\n Migrate [offer] to [Offer service] => DONE: inserted {offerSource.Count} applications. \n");

            }
            else
            {
                Console.WriteLine($"Migrate [offer] to [Offer service] => DONE: data exsited. \n");
            }
        }

        private string GetPositionName(int positionId)
        {
            return _hrToolDbContext.Positions?.FirstOrDefault(f => f.ExternalId == positionId)?.PositionName;
        }

        private bool? GetStatus(object IsAcceptSigning)
        {
            if (string.IsNullOrEmpty(IsAcceptSigning.ToString()))
            {
                return null;
            }

            return Convert.ToBoolean(IsAcceptSigning);
        }

        public class ContractCodeSearchModelComparer : IEqualityComparer<MongoDatabaseHrToolv1.Model.ContractCode>
        {
            public bool Equals(MongoDatabaseHrToolv1.Model.ContractCode obj1, MongoDatabaseHrToolv1.Model.ContractCode obj2)
            {
                return obj1.JobApplicationId == obj2.JobApplicationId;
            }

            public int GetHashCode(MongoDatabaseHrToolv1.Model.ContractCode obj)
            {
                return obj.JobApplicationId.GetHashCode();
            }
        }
    }
}
