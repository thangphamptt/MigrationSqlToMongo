using Infrastructure.AzureStorage.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace Infrastructure.AzureStorage.UploadFileToAzureStorage
{
	public class UploadFileToAzureStorage
	{
		public async Task<string> UploadFileAsync(FileAzureStorageModel fileAzureStorageModel)
		{
			CloudStorageAccount _storageAccount = CloudStorageAccount.Parse(fileAzureStorageModel.StorageConnectionString);
			CloudBlobClient _blobClient = _storageAccount.CreateCloudBlobClient();

			var container = _blobClient.GetContainerReference(fileAzureStorageModel.ContainerName);
			await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

			var blockBlob = container.GetBlockBlobReference(fileAzureStorageModel.FileUrl);
			blockBlob.Properties.ContentType = fileAzureStorageModel.ContentType;

			using (var stream = fileAzureStorageModel.Stream)
			{
			   await blockBlob.UploadFromStreamAsync(fileAzureStorageModel.Stream);
			}

			return blockBlob.Name;
		}
	}
}
