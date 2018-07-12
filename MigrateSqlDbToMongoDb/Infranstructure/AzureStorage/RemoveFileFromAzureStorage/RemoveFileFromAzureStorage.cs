using Infrastructure.AzureStorage.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace Infrastructure.AzureStorage.RemoveFileFromAzureStorage
{
    public class RemoveFileFromAzureStorage
    {
        public async Task RemoveFileAsync(FileAzureStorageModel fileAzureStorageModel)
        {
            CloudStorageAccount _storageAccount = CloudStorageAccount.Parse(fileAzureStorageModel.StorageConnectionString);
            CloudBlobClient _blobClient = _storageAccount.CreateCloudBlobClient();

            var container = _blobClient.GetContainerReference(fileAzureStorageModel.ContainerName);
            await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileAzureStorageModel.FileUrl);

            if (await blockBlob.ExistsAsync())
            {
                await blockBlob.DeleteAsync();
            }
        }
    }
}
