namespace Infrastructure.AzureStorage.Settings
{
	public class AzureStorageSetting
	{
		public string StorageConnectionString { get; set; }
		public string DomainStorageFile { get; set; }
		public string SecretApiKey { get; set; }
		public string ContainerName { get; set; }
        public string ProfileImageContainerName { get; set; }
    }
}
