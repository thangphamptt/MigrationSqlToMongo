using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateDomainModel = MongoDatabase.Domain.Template.AggregatesModel;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;
using InterviewDomainModel = MongoDatabase.Domain.Interview.AggregatesModel;


namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateTemplateService
    {
        private HrToolv1DbContext _hrToolDbContext;
        private TemplateDbContext _templateDbContext;
        private CandidateDbContext _candidateDbContext;
        private InterviewDbContext _interviewDbContext;

        private string organizationalUnitId;
        private string userId;
        private List<MongoDatabaseHrToolv1.Model.LetterTemplate> templateData;

        public MigrateTemplateService(IConfiguration configuration,
            HrToolv1DbContext hrToolDbContext,
            TemplateDbContext templateDbContext,
            CandidateDbContext candidateDbContext,
            InterviewDbContext interviewDbContext)
        {
            _hrToolDbContext = hrToolDbContext;
            _templateDbContext = templateDbContext;
            _candidateDbContext = candidateDbContext;
            _interviewDbContext = interviewDbContext;

            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            userId = configuration.GetSection("AdminUser:Id")?.Value;
            templateData = _hrToolDbContext.LetterTemplates.ToList();
        }

        public async Task ExecuteAsync()
        {
            await MigrateTemplateToTemplateService();
            await MigrateTemplateToCandidateService();
            await MigrateTemplateToInterviewService();
        }

        private async Task MigrateTemplateToTemplateService()
        {
            Console.WriteLine("Migrate [template] to [Template service] => Starting...");

            var templateIdsDestination = _templateDbContext.Templates.Select(s => s.Id).ToList();
            var templateSource = templateData.Where(w => !templateIdsDestination.Contains(w.Id.ToString())).ToList();
            if (templateSource != null && templateSource.Count > 0)
            {
                int count = 0;
                foreach (var template in templateSource)
                {
                    var emailTemplateType = GetEmailTemplateType(template.Type);
                    var emailTemplateSubType = GetEmailTemplateSubType(emailTemplateType);

                    var data = new TemplateDomainModel.EmailTemplate()
                    {
                        Id = template.Id.ToString(),
                        Attachments = new List<TemplateDomainModel.Attachment>(),
                        Body = template.Detail,
                        CreatedDate = DateTime.Now,
                        CreatedByUserId = userId,
                        Name = template.Type, //old version use type as name for letter template
                        OrganizationalUnitId = organizationalUnitId,
                        Status = TemplateDomainModel.TemplateStatus.Draft,
                        Subject = template.Subject,
                        Type = TemplateDomainModel.EmailTemplateType.General,
                        SubType = emailTemplateSubType
                    };

                    await _templateDbContext.TemplateCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{templateSource.Count}");
                }
                Console.WriteLine($"\n Migrate [template] to [Template service] => DONE: inserted {templateSource.Count} templates. \n");

            }
            else
            {
                Console.WriteLine($"Migrate [template] to [Template service] => DONE: data exsited. \n");
            }
        }

        private async Task MigrateTemplateToCandidateService()
        {
            Console.WriteLine("Migrate [template] to [Candidate service] => Starting...");

            var templateIdsDestination = _templateDbContext.Templates.Select(s => s.Id).ToList();
            var templateSource = templateData.Where(w => !templateIdsDestination.Contains(w.Id.ToString())).ToList();
            if (templateSource != null && templateSource.Count > 0)
            {
                int count = 0;
                foreach (var template in templateSource)
                {
                    var emailTemplateType = GetEmailTemplateType(template.Type);
                    var emailTemplateSubType = GetEmailTemplateSubType(emailTemplateType);

                    var data = new CandidateDomainModel.ThankYouEmailTemplate()
                    {
                        Id = template.Id.ToString(),
                        Attachments = new List<CandidateDomainModel.Attachment>(),
                        Body = template.Detail,
                        Name = template.Type, //old version use type as name for letter template
                        OrganizationalUnitId = organizationalUnitId,
                        Subject = template.Subject,
                        Type = TemplateDomainModel.EmailTemplateType.ThankYou.ToString(),
                    };

                    await _candidateDbContext.TemplateCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{templateSource.Count}");
                }
                Console.WriteLine($"\n Migrate [template] to [Candidate service] => DONE: inserted {templateSource.Count} templates. \n");

            }
            else
            {
                Console.WriteLine($"Migrate [template] to [Candidate service] => DONE: data exsited. \n");
            }
        }

        private async Task MigrateTemplateToInterviewService()
        {
            Console.WriteLine("Migrate [template] to [Interview service] => Starting...");

            var templateIdsDestination = _interviewDbContext.Templates.Select(s => s.Id).ToList();
            var templateSource = templateData.Where(w => !templateIdsDestination.Contains(w.Id.ToString())).ToList();
            if (templateSource != null && templateSource.Count > 0)
            {
                int count = 0;
                foreach (var template in templateSource)
                {
                    var emailTemplateType = GetEmailTemplateType(template.Type);
                    var emailTemplateSubType = GetEmailTemplateSubType(emailTemplateType);

                    var data = new InterviewDomainModel.InterviewEmailTemplate()
                    {
                        Id = template.Id.ToString(),
                        Attachments = new List<InterviewDomainModel.Attachment>(),
                        Body = template.Detail,
                        Name = template.Type, //old version use type as name for letter template
                        OrganizationalUnitId = organizationalUnitId,
                        Subject = template.Subject,
                        Type = TemplateDomainModel.InterviewType.Onsite.ToString()
                    };

                    await _interviewDbContext.TemplateCollection.InsertOneAsync(data);

                    count++;
                    Console.Write($"\r {count}/{templateSource.Count}");
                }
                Console.WriteLine($"\n Migrate [template] to [Interview service] => DONE: inserted {templateSource.Count} templates. \n");

            }
            else
            {
                Console.WriteLine($"Migrate [template] to [Interview service] => DONE: data exsited. \n");
            }
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
