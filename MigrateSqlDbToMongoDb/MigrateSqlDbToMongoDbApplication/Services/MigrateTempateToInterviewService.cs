using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Threading.Tasks;
using System.Linq;
using InterviewDomainModel = MongoDatabase.Domain.Interview.AggregatesModel;
using TemplateDomainModel = MongoDatabase.Domain.Template.AggregatesModel;
using HrToolDomainModel = MongoDatabaseHrToolv1.Model;
using System;
using System.Collections.Generic;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateTemplateToInterviewService
    {
        private HrToolv1DbContext hrToolDbContext;

        public async Task<int> Execute(IConfiguration configuration)
        {
            hrToolDbContext = new HrToolv1DbContext(configuration);
            var interviewDbContext = new InterviewDbContext(configuration);

            var organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            var userId = configuration.GetSection("AdminUser:Id")?.Value;
            var dataInserted = 0;
            var listTemplate = new List<InterviewDomainModel.Template>();

            try
            {
                var letterTemplates = hrToolDbContext.LetterTemplates.ToList();
                foreach (var letterTemplate in letterTemplates)
                {
                    bool hasOfferExisted = interviewDbContext.Templates.OfType<InterviewDomainModel.InterviewEmailTemplate>()
                        .Any(w => w.Id == letterTemplate.Id.ToString());
                    if (!hasOfferExisted)
                    {
                        var template = new InterviewDomainModel.InterviewEmailTemplate()
                        {
                            Id = letterTemplate.Id.ToString(),
                            Attachments = new List<InterviewDomainModel.Attachment>(),
                            Body = letterTemplate.Detail,
                            Name = letterTemplate.Type, //old version use type as name for letter template
                            OrganizationalUnitId = organizationalUnitId,
                            Subject = letterTemplate.Subject,
                            Type = TemplateDomainModel.InterviewType.Onsite.ToString()                            
                        };

                        listTemplate.Add(template);
                        await interviewDbContext.TemplateCollection.InsertOneAsync(template);
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
    }
}
