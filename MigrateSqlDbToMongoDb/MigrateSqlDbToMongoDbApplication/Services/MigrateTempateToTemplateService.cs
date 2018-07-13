using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;
using System.Linq;
using TemplateDomainModel = MongoDatabase.Domain.Template.AggregatesModel;
using HrToolDomainModel = MongoDatabaseHrToolv1.Model;
using System;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateTemplateToTemplateService
    {
        private HrToolv1DbContext hrToolDbContext;

        public async Task<int> Execute(IConfiguration configuration)
        {
            hrToolDbContext = new HrToolv1DbContext(configuration);
            var templateDbContext = new TemplateDbContext(configuration);

            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var userId = configuration.GetSection("AdminUser:Id")?.Value;
            var dataInserted = 0;
            var listTemplate = new List<TemplateDomainModel.Template>();

            try
            {
                var letterTemplates = hrToolDbContext.LetterTemplates.ToList();
                foreach (var letterTemplate in letterTemplates)
                {
                    bool hasOfferExisted = templateDbContext.Templates.Any(w => w.Id == letterTemplate.Id.ToString());
                    if (!hasOfferExisted)
                    {
                        var emailTemplateType = GetEmailTemplateType(letterTemplate.Type);
                        var emailTemplateSubType = GetEmailTemplateSubType(emailTemplateType);

                        var template = new TemplateDomainModel.EmailTemplate()
                        {
                            Id = letterTemplate.Id.ToString(),
                            Attachments = null,
                            Body = letterTemplate.Detail,
                            CreatedDate = DateTime.Now,
                            CreatedByUserId = userId,
                            Name = letterTemplate.Type, //old version use type as name for letter template
                            OrganizationalUnitId = organizationalUnitId,
                            Status = TemplateDomainModel.TemplateStatus.Draft,
                            Subject = letterTemplate.Subject,
                            Type = TemplateDomainModel.EmailTemplateType.General,
                            SubType = emailTemplateSubType
                        };

                        listTemplate.Add(template);
                        //Migrate job to Candidate service
                        //await templateDbContext.TemplateCollection.InsertOneAsync(template);
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

        private TemplateDomainModel.EmailTemplateType GetEmailTemplateType(string type)
        {
            if (type.ToLower().Contains("interview"))
            {
                return TemplateDomainModel.EmailTemplateType.Interview;
            }
            else if (type.ToLower().Contains("offer"))
            {
                return TemplateDomainModel.EmailTemplateType.Offer;
            }
            else if (type.ToLower().Contains("thank"))
            {
                return TemplateDomainModel.EmailTemplateType.ThankYou;
            }

            return TemplateDomainModel.EmailTemplateType.General;
        }

        private string GetEmailTemplateSubType(TemplateDomainModel.EmailTemplateType emailTemplateType)
        {
            if (emailTemplateType == TemplateDomainModel.EmailTemplateType.Interview)
            {
                return TemplateDomainModel.InterviewType.Onsite.ToString();
            }
            return string.Empty;
        }
    }
}
