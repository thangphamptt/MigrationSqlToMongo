using System.IO;

namespace Infrastructure.AzureStorage.Model
{
    public class FileAzureStorageModel
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string ContainerName { get; set; }

        public Stream Stream { get; set; }

        public string StorageConnectionString { get; set; }

		public string FileUrl { get; set; }
	}
}
