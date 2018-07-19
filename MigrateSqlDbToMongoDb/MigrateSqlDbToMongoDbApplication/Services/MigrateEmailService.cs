using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Common.Services;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateEmailService
    {
        private HrToolv1DbContext _hrToolDbContext;
        private EmailDbContext _emailDbContext;
        private CandidateDbContext _candidateDbContext;

        private List<MongoDatabaseHrToolv1.Model.EmailTracking> emailData;
        private readonly string emailAttachmentContainName;
        private readonly string oldHrtoolStoragePath;
        private string organizationalUnitId;
        private string userId;
        private UploadFileFromLink uploadFileFromLink;

        public MigrateEmailService(IConfiguration configuration,
            HrToolv1DbContext hrToolDbContext,
            EmailDbContext emailDbContext,
            CandidateDbContext candidateDbContext)
        {
            _hrToolDbContext = hrToolDbContext;
            _emailDbContext = emailDbContext;
            _candidateDbContext = candidateDbContext;

            uploadFileFromLink = new UploadFileFromLink(configuration.GetSection("AzureStorage:StorageConnectionString")?.Value);
            emailAttachmentContainName = configuration.GetSection("AzureStorage:EmailAttachmentContainName")?.Value;
            oldHrtoolStoragePath = configuration.GetSection("OldHrtoolStoragePath")?.Value;
            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            userId = configuration.GetSection("AdminUser:Id")?.Value;

            emailData = hrToolDbContext.EmailTrackings
                .Where(x => x.TypeOfEmail == "Scheduling Email"
                || x.TypeOfEmail == "Offer Email"
                || x.TypeOfEmail == "Thank You Email")
                .ToList();
        }

        public async Task ExecuteAsync()
        {
            await MigrateEmailToEmailService();
        }

        private async Task MigrateEmailToEmailService()
        {
            Console.WriteLine("Migrate [email] to [Email service] => Starting...");        

            var emailIdsDestination = _emailDbContext.Emails.Select(s => s.Id).ToList();
            var emailSource = emailData.Where(w => !emailIdsDestination.Contains(w.Id.ToString())).ToList();
            if (emailSource != null && emailSource.Count > 0)
            {
                int count = 0;
                var interiewEmails = emailSource.Where(x => x.TypeOfEmail == "Scheduling Email").ToList();
                foreach (var email in interiewEmails)
                {
                    await _emailDbContext.EmailCollection.InsertOneAsync(InterviewEmail(email));

                    count++;
                    Console.Write($"\r {count}/{emailSource.Count}");
                }

                var offerEmails = emailSource.Where(x => x.TypeOfEmail == "Offer Email").ToList();
                foreach (var email in offerEmails)
                {
                    await _emailDbContext.EmailCollection.InsertOneAsync(await OfferMail(email));

                    count++;
                    Console.Write($"\r {count}/{emailSource.Count}");
                }

                var thankyouEmails = emailSource.Where(x => x.TypeOfEmail == "Thank You Email").ToList();
                foreach (var email in thankyouEmails)
                {
                    await _emailDbContext.EmailCollection.InsertOneAsync(ThankyouEmail(email));

                    count++;
                    Console.Write($"\r {count}/{emailSource.Count}");
                }
                Console.WriteLine($"\n Migrate [email] to [Email service] => DONE: inserted {emailSource.Count} applications. \n");

            }
            else
            {
                Console.WriteLine($"Migrate [email] to [Email service] => DONE: data exsited. \n");
            }
        }

        private MongoDatabase.Domain.Email.AggregatesModel.InterviewEmail InterviewEmail(MongoDatabaseHrToolv1.Model.EmailTracking email)
        {
            var interview = _hrToolDbContext.Interviews.FirstOrDefault(x => x.JobApplicationId == email.JobApplicationId);
            return new MongoDatabase.Domain.Email.AggregatesModel.InterviewEmail
            {
                Id = email.Id.ToString(),
                Body = email.Body,
                Recipients = new List<string> { email.To },
                Sender = email.From,
                SentDate = email.SendingTime is DateTime ? (DateTime)email.SendingTime : DateTime.Now,
                Subject = email.Subject,
                InterviewId = interview.Id.ToString()
            };
        }

        private MongoDatabase.Domain.Email.AggregatesModel.ThankyouEmail ThankyouEmail(MongoDatabaseHrToolv1.Model.EmailTracking email)
        {
            return new MongoDatabase.Domain.Email.AggregatesModel.ThankyouEmail
            {
                Id = email.Id.ToString(),
                Body = email.Body,
                Recipients = new List<string> { email.To },
                Sender = email.From,
                SentDate = email.SendingTime is DateTime ? (DateTime)email.SendingTime : DateTime.Now,
                Subject = email.Subject
            };
        }

        private async Task<MongoDatabase.Domain.Email.AggregatesModel.OfferEmail> OfferMail(MongoDatabaseHrToolv1.Model.EmailTracking email)
        {
            var offer = _hrToolDbContext.ContractCodes.Where(x => x.JobApplicationId == email.JobApplicationId)
                .OrderByDescending(x => x.ExternalId).FirstOrDefault();

            var newAttachments = await GetAttachments(new AttachmentInfo
            {
                ContainerFolder = emailAttachmentContainName,
                EmailTrackingId = email.ExternalId,
                NewOfferId = offer.Id,
                OldOfferId = offer.ExternalId,
                OldStoragePath = oldHrtoolStoragePath
            });

            return new MongoDatabase.Domain.Email.AggregatesModel.OfferEmail
            {
                Id = email.Id.ToString(),
                Body = email.Body,
                Recipients = new List<string> { email.To },
                Sender = email.From,
                SentDate = email.SendingTime is DateTime ? (DateTime)email.SendingTime : DateTime.Now,
                Subject = email.Subject,
                Attachments = newAttachments,
                OfferId = offer.Id.ToString()
            };
        }

        private async Task<IList<MongoDatabase.Domain.Email.AggregatesModel.Attachment>> GetAttachments(AttachmentInfo info)
        {
            var results = new List<MongoDatabase.Domain.Email.AggregatesModel.Attachment>();
            var attachments = _hrToolDbContext.EmailTrackingAttachments.Where(x => x.EmailTrackingId == info.EmailTrackingId).Select(x => x).ToList();
            foreach (var attachment in attachments)
            {
                var contentType = MimeMapping.MimeUtility.GetMimeMapping(attachment.Filename);
                var newLink = $"OfferEmail/{info.OldOfferId}/{info.NewOfferId.ToString()}/{attachment.Filename}";
                var path = await uploadFileFromLink.GetAttachmentPathAsync(
                    new Common.Services.Model.AttachmentFileModel
                    {
                        Name = attachment.Filename,
                        Path = info.OldStoragePath + attachment.Path
                    }, newLink, info.ContainerFolder);
                var newAttachment = new MongoDatabase.Domain.Email.AggregatesModel.Attachment
                {
                    Id = attachment.Id.ToString(),
                    Name = attachment.Filename,
                    Path = path
                };
                results.Add(newAttachment);
            }
            return results;
        }

        private class AttachmentInfo
        {
            public int EmailTrackingId { get; set; }
            public ObjectId NewOfferId { get; set; }
            public int OldOfferId { get; set; }
            public string ContainerFolder { get; set; }
            public string OldStoragePath { get; set; }
        }
    }
}
