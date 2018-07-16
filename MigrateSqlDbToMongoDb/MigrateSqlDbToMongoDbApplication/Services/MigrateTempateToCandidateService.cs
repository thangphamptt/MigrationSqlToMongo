using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;
using System.Linq;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;
using TemplateDomainModel = MongoDatabase.Domain.Template.AggregatesModel;
using HrToolDomainModel = MongoDatabaseHrToolv1.Model;
using System;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateTemplateToCandidateService
    {
        private HrToolv1DbContext hrToolDbContext;

        public async Task<int> Execute(IConfiguration configuration)
        {
            hrToolDbContext = new HrToolv1DbContext(configuration);
            var candidateDbContext = new CandidateDbContext(configuration);

            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var userId = configuration.GetSection("AdminUser:Id")?.Value;
            var dataInserted = 0;
            var listTemplate = new List<CandidateDomainModel.Template>();

            try
            {
                var letterTemplates = hrToolDbContext.LetterTemplates.ToList();
                foreach (var letterTemplate in letterTemplates)
                {
                    bool hasOfferExisted = candidateDbContext.Templates.OfType<CandidateDomainModel.ThankYouEmailTemplate>()
                        .Any(w => w.Id == letterTemplate.Id.ToString());
                    bool isThankYouTemplate = IsThankYouTemplate(letterTemplate.Type);
                    if (!hasOfferExisted && isThankYouTemplate)
                    {
                        var template = new CandidateDomainModel.ThankYouEmailTemplate()
                        {
                            Id = letterTemplate.Id.ToString(),
                            Attachments = new List<CandidateDomainModel.Attachment>(),
                            Body = letterTemplate.Detail,
                            Name = letterTemplate.Type, //old version use type as name for letter template
                            OrganizationalUnitId = organizationalUnitId,
                            Subject = letterTemplate.Subject,
                            Type = TemplateDomainModel.EmailTemplateType.ThankYou.ToString(),
                        };

                        listTemplate.Add(template);
                        await candidateDbContext.TemplateCollection.InsertOneAsync(template);
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

        private bool IsThankYouTemplate(string type)
        {
            return type.ToLower().Contains("thank");           
        }        
    }
}
