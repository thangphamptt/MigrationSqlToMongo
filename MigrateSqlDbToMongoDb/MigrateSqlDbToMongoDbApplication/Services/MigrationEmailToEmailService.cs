using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Common.Services;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System;
using MongoDatabase.Domain.Candidate.AggregatesModel;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrationEmailToEmailService
	{
		private readonly HrToolv1DbContext hrToolDbContext;
		private readonly EmailDbContext emailDbContext;
		private readonly CandidateDbContext candidateDbContext;
		private readonly string emailAttachmentContainName;
		private readonly string oldHrtoolStoragePath;
		private readonly string organizationalUnitId;
		private readonly string userId;
		private readonly UploadFileFromLink uploadFileFromLink;

		public MigrationEmailToEmailService(IConfiguration configuration)
		{
			hrToolDbContext = new HrToolv1DbContext(configuration);
			candidateDbContext = new CandidateDbContext(configuration);
			emailDbContext = new EmailDbContext(configuration);
			uploadFileFromLink = new UploadFileFromLink(configuration.GetSection("AzureStorage:StorageConnectionString")?.Value);
			emailAttachmentContainName = configuration.GetSection("AzureStorage:EmailAttachmentContainName")?.Value;
			oldHrtoolStoragePath = configuration.GetSection("OldHrtoolStoragePath")?.Value;
			organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
			userId = configuration.GetSection("AdminUser:Id")?.Value;
		}
		
		public async Task<int> ExecuteAsync()
		{
			var totalEmails = 0;
			var interiewEmails = await hrToolDbContext.EmailTrackingCollection.AsQueryable().Where(x => x.TypeOfEmail == "Scheduling Email").ToListAsync();
			foreach(var email in interiewEmails)
			{
				if (!emailDbContext.Emails.Any(x => x.Id == email.Id.ToString()))
				{
					await emailDbContext.EmailCollection.InsertOneAsync(InterviewEmail(email));
					totalEmails++;
				}
			}
			var offerEmails = await hrToolDbContext.EmailTrackingCollection.AsQueryable().Where(x => x.TypeOfEmail == "Offer Email").ToListAsync();
			foreach (var email in offerEmails)
			{
				if (!emailDbContext.Emails.Any(x => x.Id == email.Id.ToString()))
				{
					await emailDbContext.EmailCollection.InsertOneAsync(await OfferMail(email));
					totalEmails++;
				}
			}

			var thankyouEmails = await hrToolDbContext.EmailTrackingCollection.AsQueryable().Where(x => x.TypeOfEmail == "Thank You Email").ToListAsync();
			foreach (var email in thankyouEmails)
			{
				if (!emailDbContext.Emails.Any(x => x.Id == email.Id.ToString()))
				{
					await emailDbContext.EmailCollection.InsertOneAsync(ThankyouEmail(email));
					totalEmails++;
				}
			}
			return totalEmails;
		}

		private MongoDatabase.Domain.Email.AggregatesModel.InterviewEmail InterviewEmail(MongoDatabaseHrToolv1.Model.EmailTracking email)
		{
			return new MongoDatabase.Domain.Email.AggregatesModel.InterviewEmail
			{
				Id = email.Id.ToString(),
				Body = email.Body,
				Recipients = new List<string> { email.To },
				Sender = email.From,
				SentDate = email.SendingTime is DateTime?(DateTime) email.SendingTime: DateTime.Now,
				Subject = email.Subject,
				//Insert Interview
				InterviewId = ""
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
			var offer = hrToolDbContext.ContractCodes.Where(x => x.JobApplicationId == email.JobApplicationId)
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
			var attachments = hrToolDbContext.EmailTrackingAttachments.Where(x => x.EmailTrackingId == info.EmailTrackingId).Select(x => x);
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
			public MongoDB.Bson.ObjectId NewOfferId { get; set; }
			public int OldOfferId { get; set; }
			public string ContainerFolder { get; set; }
			public string OldStoragePath { get; set; }
		}

	}
}
