using Microsoft.Extensions.Configuration;
using MigrateSqlDbToMongoDbApplication.Common.Services;
using MongoDatabase.DbContext;
using MongoDatabaseHrToolv1.DbContext;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateDomainModel = MongoDatabase.Domain.Candidate.AggregatesModel;

namespace MigrateSqlDbToMongoDbApplication.Services
{
    public class MigrateAttachmentService
    {
        //1.Get candidate list with attachment name contain 'https://hr-staging.orientsoftware.net'
        //2.Re-upload for these candidates: check if file is not exited then doesn't save path into to attachment

        private HrToolv1DbContext _hrToolDbContext;
        private CandidateDbContext _candidateDbContext;
        private IConfiguration _configuration;
        private UploadFileFromLink uploadFileFromLink;
        private string organizationalUnitId;
        private string cvAttachmentFolderName;
        private string oldHrtoolStoragePath;
        private string userId;

        public MigrateAttachmentService(IConfiguration configuration,
            CandidateDbContext candidateDbContext)
        {
            _configuration = configuration;
            _candidateDbContext = candidateDbContext;

            var azureStoregeConnectionString = configuration.GetSection("AzureStorage:StorageConnectionString")?.Value;
            uploadFileFromLink = new UploadFileFromLink(azureStoregeConnectionString);
            cvAttachmentFolderName = configuration.GetSection("AzureStorage:CvAttachmentContainerName")?.Value;
            oldHrtoolStoragePath = configuration.GetSection("OldHrtoolStoragePath")?.Value;
            organizationalUnitId = configuration.GetSection("CompanySetting:Id")?.Value;
            userId = configuration.GetSection("AdminUser:Id")?.Value;
        }

        public async Task ExecuteAsync()
        {
            //We use this migrate when upload wrong path for attachment
            await MigrateAttachmentToCandidateService();
        }

        private async Task MigrateAttachmentToCandidateService()
        {
            Console.WriteLine("Migrate [attachment] to [Candidate service] => Starting...");

            var applications = _candidateDbContext.Applications
                .Where(w => w.Attachments.Any(att => att.Path.Contains("https://hr-staging.orientsoftware.net")))
                .ToList();
            int count = 0;
            foreach (var application in applications)
            {
                var attachments = await GetAttachmentsCandidateDomain(application.CandidateId, application.Id, application.Attachments.ToList());
                var filter = Builders<CandidateDomainModel.Application>.Filter.Eq(x => x.Id, application.Id);
                var update = Builders<CandidateDomainModel.Application>.Update.Set(x => x.Attachments, attachments);
                await _candidateDbContext.ApplicationCollection.UpdateOneAsync(filter, update);
                count++;
                Console.Write($"\r {count}/{applications.Count}");
            }
            Console.WriteLine($"\n Migrate [attachment] to [Candidate service] => DONE: inserted attachment for {applications.Count} applications. \n");

        }

        private async Task<IList<CandidateDomainModel.File>> GetAttachmentsCandidateDomain(string candidateId, string applicationId, List<CandidateDomainModel.File> attachments)
        {
            var results = new List<CandidateDomainModel.File>();
            foreach (var attachment in attachments)
            {
                var contentType = MimeMapping.MimeUtility.GetMimeMapping(attachment.Name);
                var newPath = $"{organizationalUnitId}/{candidateId}/{applicationId}/{attachment.Name}";

                var path = await uploadFileFromLink.GetAttachmentPathAsync(
                    new Common.Services.Model.AttachmentFileModel
                    {
                        Name = attachment.Name,
                        Path = attachment.Path.Replace("https://hr-staging.orientsoftware.net", "https://hr.orientsoftware.net"),
                    }, newPath, cvAttachmentFolderName);

                if (!string.IsNullOrEmpty(path))
                {
                    var newAttachment = new CandidateDomainModel.File
                    {
                        Id = attachment.Id.ToString(),
                        Name = attachment.Name,
                        Path = path
                    };
                    results.Add(newAttachment);
                }
            }

            return results;
        }

        private class AttachmentInfo
        {
            public int OldApplicationId { get; set; }
            public MongoDB.Bson.ObjectId NewCandidateId { get; set; }
            public MongoDB.Bson.ObjectId NewApplicationId { get; set; }
            public string ContainerFolder { get; set; }
            public string OldStoragePath { get; set; }
        }

    }
}
