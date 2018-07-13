using Infrastructure.AzureStorage.Model;
using Infrastructure.AzureStorage.UploadFileToAzureStorage;
using MigrateSqlDbToMongoDbApplication.Common.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MigrateSqlDbToMongoDbApplication.Common.Services
{
    public class UploadFileFromLink
    {
		private readonly string _connectionString;
		public UploadFileFromLink(string connectionString)
		{
			_connectionString = connectionString;
		}

		private UploadFileToAzureStorage _uploadFileToAzureStorage = new UploadFileToAzureStorage();
		public async Task<string> GetProfilePathAsync(AttachmentFileModel attachment, string newPath, string containerFolder)
		{
			using (var client = new HttpClient())
			{

				if (!string.IsNullOrEmpty(attachment.Path))
				{
					var attachmentUrl = new Uri(attachment.Path);

					try
					{
						HttpResponseMessage result = client.GetAsync(attachmentUrl).Result;
						if (result.IsSuccessStatusCode)
						{
							var contentData = await result.Content.ReadAsByteArrayAsync();
							using (var stream = new MemoryStream(contentData))
							{
								var contentType = MimeMapping.MimeUtility.GetMimeMapping(attachment.Name);

								var fileAzureStorageModel = new FileAzureStorageModel()
								{
									ContainerName = containerFolder,
									ContentType = contentType,
									FileName = attachment.Name,
									Stream = stream,
									StorageConnectionString = _connectionString,
									FileUrl = newPath
								};

								attachment.Path = await _uploadFileToAzureStorage.UploadFileAsync(fileAzureStorageModel);
							}
						}
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}

				return attachment.Path;
			}
		}
	}
}
